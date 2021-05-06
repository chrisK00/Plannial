using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plannial.Core.Helpers;
using Plannial.Core.Interfaces;
using Plannial.Core.Queries;
using Plannial.Core.Repositories;

namespace Plannial.Core.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)                                              
        {
            services.AddMediatR(typeof(GetSubjects).Assembly);
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //register all app services here
            return services;
        }
    }
}
