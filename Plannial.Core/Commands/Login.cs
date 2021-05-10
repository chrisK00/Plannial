using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Commands
{
    public static class Login
    {
        public record Command(string Email, string Password) : IRequest<UserResponse>;

        public class Handler : IRequestHandler<Command, UserResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenService _tokenService;

            public Handler(IUserRepository userRepository, ITokenService tokenService)
            {
                _userRepository = userRepository;
                _tokenService = tokenService;
            }

            public async Task<UserResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new InvalidOperationException("Failed to login");
                }

                var token = _tokenService.CreateToken(user);
                return new UserResponse { Id = user.Id, Email = user.Email, Token = token };
            }
        }
    }
}
