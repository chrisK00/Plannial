using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
using Plannial.Core.Models.Requests;
using Plannial.Core.Models.Responses;

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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            await _mediator.Send(new Register.Command(registerRequest.Email, registerRequest.Password));
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _mediator.Send(new Login.Command(loginRequest.Email, loginRequest.Password));
            return user;
        }

    }
}
