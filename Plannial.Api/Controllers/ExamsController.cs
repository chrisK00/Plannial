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

    public class ExamsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ExamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveExam(int id)
        {
            await _mediator.Send(new RemoveExam.Command(id, User.GetUserId()));
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExamResponse>> UpdateExam(int id, UpdateExamRequest updateExamRequest, CancellationToken cancellationToken)
        {
            var exam = await _mediator.Send(new UpdateExam.Command(
                   id, updateExamRequest.Name, updateExamRequest.Description, updateExamRequest.DueDate, User.GetUserId()), cancellationToken);

            return exam;
        }
    }
}
