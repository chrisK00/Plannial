﻿using MediatR;
using Plannial.Data.Interfaces;
using Plannial.Data.Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Core.Commands
{
    public static class Register
    {
        public record Command(string Email, string Password) : IRequest;

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new AppUser { Email = request.Email, UserName = request.Email };

                await _userRepository.AddUserAsync(user, request.Password);
            }
        }
    }
}
