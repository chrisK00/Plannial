using MediatR;
using Microsoft.AspNetCore.Identity;
using Plannial.Core.Services;
using Plannial.Data.Interfaces;
using Plannial.Data.Models.Entities;
using Plannial.Data.Models.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Core.Commands
{
    public static class Login
    {
        public record Command(string Email, string Password) : IRequest<UserResponse>;

        public class Handler : IRequestHandler<Command, UserResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenService _tokenService;
            private readonly SignInManager<AppUser> _signInManager;

            public Handler(IUserRepository userRepository, ITokenService tokenService, SignInManager<AppUser> signInManager)
            {
                _userRepository = userRepository;
                _tokenService = tokenService;
                _signInManager = signInManager;
            }

            public async Task<UserResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email);

                _ = user ?? throw new InvalidOperationException("Failed to login");

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded)
                {
                    throw new UnauthorizedAccessException("Invalid username or password");
                }

                var token = _tokenService.CreateToken(user);
                return new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    Token = token
                };
            }
        }
    }
}
