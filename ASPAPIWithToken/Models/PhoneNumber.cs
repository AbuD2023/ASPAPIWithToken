using System.ComponentModel.DataAnnotations.Schema;

namespace ASPAPIWithToken.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Phone { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
    }
}
