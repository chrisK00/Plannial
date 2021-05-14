using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Commands
{
    public static class Register
    {
        public record Command(string Email, string Password) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new AppUser { Email = request.Email, UserName = request.Email };

                await _userRepository.AddUserAsync(user, request.Password);

                return Unit.Value;
            }
        }
    }
}
