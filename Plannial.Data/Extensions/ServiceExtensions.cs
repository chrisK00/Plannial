using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plannial.Core.Data;
using Plannial.Data.Helpers;
using Plannial.Data.Interfaces;
using Plannial.Data.Models.Entities;
using Plannial.Data.Repositories;

namespace Plannial.Data.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IHomeworkRepository, HomeworkRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageReadRepository, MessageReadRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(config.GetConnectionString("Default")));

            services.ConfigureIdentityServices();
        }

        private static void ConfigureIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(
             options =>
             {
                 options.User.RequireUniqueEmail = true;
                 options.Password.RequireNonAlphanumeric = false;
             }
             ).AddEntityFrameworkStores<DataContext>();
        }
    }
}