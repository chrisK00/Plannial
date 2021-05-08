using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
using Plannial.Core.Extensions;
using Plannial.Core.Requests;
using Plannial.Core.Responses;

namespace Plannial.Api.Controllers
{

    public class ExamsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ExamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{subjectId}")]
        public async Task<ActionResult<ExamResponse>> AddExam(AddExamRequest addExamRequest, int subjectId, CancellationToken cancellationToken)
        {
            var exam = await _mediator.Send(new AddExam.Command(
                addExamRequest.Name, addExamRequest.Description, addExamRequest.DueDate, subjectId, User.GetUserId()), cancellationToken);

            return Created($"subjects/{subjectId}", exam);
        }
    }
}
