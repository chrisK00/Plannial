using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands.RemoveCommands;
using Plannial.Core.Extensions;

namespace Plannial.Api.Controllers
{
  
    public class HomeworksController : BaseApiController
    {

        private readonly IMediator _mediator;

        public HomeworksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveHomework(int id)
        {
            await _mediator.Send(new RemoveHomework.Command(User.GetUserId(), id));
            return NoContent();
        }
    }
}
