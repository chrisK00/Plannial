using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Plannial.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
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

              //configure token for signalR
              opt.Events = new JwtBearerEvents
              {
                  OnMessageReceived = context =>
                  {
                      var accessToken = context.Request.Query["access_token"];
                      var path = context.HttpContext.Request.Path;

                      //check if we have a token in the query string and the path is towards our hubs
                      if (!string.IsNullOrWhiteSpace(accessToken) && path.StartsWithSegments("/hubs"))
                      {
                          context.Token = accessToken;
                      }

                      return Task.CompletedTask;
                  }
              };
          });
        }
    }
}
