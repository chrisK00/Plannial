using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
using Plannial.Core.Commands.RemoveCommands;
using Plannial.Core.Extensions;
using Plannial.Core.Models.Requests;
using Plannial.Core.Models.Responses;

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
        public async Task<IActionResult> RemoveHomework(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoveHomework.Command(User.GetUserId(), id), cancellationToken);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<ActionResult<HomeworkResponse>> UpdateHomework(int id, UpdateHomeworkRequest updateHomeworkRequest, CancellationToken cancellationToken)
        {
            var homework = await _mediator.Send(new UpdateHomework.Command(
                   id, updateHomeworkRequest.Name, updateHomeworkRequest.Description, updateHomeworkRequest.DueDate, User.GetUserId()), cancellationToken);

            return homework;
        }
    }
}
