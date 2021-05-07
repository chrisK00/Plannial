using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
using Plannial.Core.Requests;
using Plannial.Core.Responses;

namespace Plannial.Api.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _mediator.Send(new Login.Command(loginRequest.Email, loginRequest.Password));
            return user;
        }

    }
}
