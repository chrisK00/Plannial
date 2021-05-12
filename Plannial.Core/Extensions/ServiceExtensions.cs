using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Plannial.Core.Data;
using Plannial.Core.Helpers;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;
using Plannial.Core.Queries;
using Plannial.Core.Repositories;
using Plannial.Core.Services;

namespace Plannial.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddMediatR(typeof(GetSubjects).Assembly);
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<IMessageRepository, MessageRepository>();
            //register all app services here
            return services;
        }

        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<AppUser, IdentityRole>(
             options =>
             {
                 options.User.RequireUniqueEmail = true;
                 options.Password.RequireNonAlphanumeric = false;
             }
             ).AddEntityFrameworkStores<DataContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(opt =>
                 {
                     opt.TokenValidationParameters = new TokenValidationParameters
                     {
                         //validate the created token is correct
                         ValidateIssuerSigningKey = true,
                         //our key to validate against the incoming
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                         ValidateAudience = false,
                         ValidateIssuer = false
                     };
                 });

            return services;
        }
    }
}
