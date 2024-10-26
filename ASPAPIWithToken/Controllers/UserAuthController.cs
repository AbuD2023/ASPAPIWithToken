using ASPAPIWithToken.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ASPAPIWithToken.Service;

namespace ASPAPIWithToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public UserAuthController(ITokenService tokenService, IConfiguration configuration)
        {
           _configuration = configuration;
            _tokenService = tokenService;
        }


        [HttpPost("loginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserAuth userAuth)
        {

            // تحقق من بيانات المستخدم في قاعدة البيانات
            bool isAuthenticated = false;
            int userId = 0;

            if (!string.IsNullOrEmpty(userAuth.Name) && !string.IsNullOrEmpty(userAuth.Password))
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id, Password FROM Users WHERE Name = @Name";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", userAuth.Name);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = reader.GetInt32(0); // استرجاع رقم المستخدم
                                string storedpassword = reader.GetString(1);

                                if (userAuth.Password == storedpassword)
                                {
                                    isAuthenticated = true;
                                }
                            }
                        }
                    }
                }
            }

            if (isAuthenticated)
            {
                var token = _tokenService.GenerateToken(userAuth.Name);
                return Ok(new { UserId = userId, Token = token });
            }

            return Unauthorized();

        }
    }
}
