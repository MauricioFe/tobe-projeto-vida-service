using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UserApi.Data
{
    public class UserDbConfig
    {

        public static void ApplyMigrations(IApplicationBuilder app)
        {
            ApplyMigrations(app.ApplicationServices.GetRequiredService<UserDbContext>());
        }
        public static void ApplyMigrations(UserDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
