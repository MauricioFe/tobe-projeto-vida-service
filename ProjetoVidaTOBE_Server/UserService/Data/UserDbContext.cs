using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUserToBe, IdentityRole<int>, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    }
}
