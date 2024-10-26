using ASPAPIWithToken.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPAPIWithToken.Data
{
    public class AspApiWithDBContext : DbContext
    {
        public AspApiWithDBContext(DbContextOptions<AspApiWithDBContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<PhoneNumber> PhoneNumbers { get; set; } = default!;
    }
}
