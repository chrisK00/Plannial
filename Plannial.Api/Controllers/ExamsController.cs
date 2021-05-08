using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
using Plannial.Core.Extensions;

namespace Plannial.Api.Controllers
{

    public class ExamsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ExamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{examId}")]
        public async Task<IActionResult> RemoveExam(int examId)
        {
            await _mediator.Send(new RemoveExam.Command(examId, User.GetUserId()));
            return NoContent();
        }
    }
}
