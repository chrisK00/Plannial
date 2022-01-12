using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plannial.Core.Queries;
using Plannial.Core.Services;

namespace Plannial.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddMediatR(typeof(GetSubjects).Assembly);
        }
    }
}