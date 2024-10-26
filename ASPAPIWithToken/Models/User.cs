using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAPIWithToken.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailConfirmed { get; set; }

        [ForeignKey(nameof(PhoneNumber.UserId))]
        public List<PhoneNumber>? PhoneNumbers { get; }

    }
}
