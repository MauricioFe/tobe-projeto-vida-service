using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<long>, IdentityRole<long>, long>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    }
}
