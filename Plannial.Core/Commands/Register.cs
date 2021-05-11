using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Commands
{
    public static class Register
    {
        public record Command(string Email, string Password) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<AppUser> _userManager;

            public Handler(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new AppUser { Email = request.Email, UserName = request.Email };
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return Unit.Value;
                }

                var sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description);
                }
                throw new InvalidOperationException(sb.ToString());
            }
        }

    }
}
