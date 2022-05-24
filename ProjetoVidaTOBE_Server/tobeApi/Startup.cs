using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using tobeApi.Data;
using tobeApi.Data.Repositories.CalendarEvents;
using tobeApi.Data.Repositories.Courses;
using tobeApi.Data.Repositories.CoursesEnrolled;
using tobeApi.Data.Repositories.Institutions;
using tobeApi.Data.Repositories.Schollings;
using tobeApi.Data.Repositories.Skills;
using tobeApi.Data.Repositories.StudentProfiles;
using tobeApi.Data.Repositories.Students;
using tobeApi.Services;

namespace tobeApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "tobeApi", Version = "v1" });
            });
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D4AFC1953ACF1A613D03EE4093483CB01DF130F93060B869CB937FF143B9BD48")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IDataAccess, DataAccess>(_ => new DataAccess(Configuration.GetConnectionString("tobeConn")));
            AddRepositoriesTransient(services);
            AddServicesScopes(services);
        }

        private static void AddRepositoriesTransient(IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ISchollingRepository, SchollingRepository>();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<ICalendarEventRepository, CalendarEventRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICourseEnrolledRepository, CourseEnrolledRepository>();
            services.AddTransient<IStudentProfileRepository, StudentProfileRepository>();
            services.AddTransient<ISkillRepository, SkillRepository>();
        }

        private static void AddServicesScopes(IServiceCollection services)
        {
            services.AddScoped<ContactsService, ContactsService>();
            services.AddScoped<StudentLoginService, StudentLoginService>();
            services.AddScoped<StudentService, StudentService>();
            services.AddScoped<SchollingService, SchollingService>();
            services.AddScoped<InstitutionService, InstitutionService>();
            services.AddScoped<CalendarEventService, CalendarEventService>();
            services.AddScoped<CourseService, CourseService>();
            services.AddScoped<CourseEnrolledService, CourseEnrolledService>();
            services.AddScoped<StudentProfileService, StudentProfileService>();
            services.AddScoped<SkillService, SkillService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "tobeApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
