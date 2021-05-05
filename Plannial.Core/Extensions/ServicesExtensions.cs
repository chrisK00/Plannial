using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plannial.Core.Interfaces;
using Plannial.Core.Queries;
using Plannial.Core.Repositories;

namespace Plannial.Core.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)                                              
        {
            services.AddMediatR(typeof(GetSubjectById).Assembly);
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            //register all app services here
            return services;
        }
    }
}
