using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
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

        /// <summary>
        /// Adds a new exam to an existing subject
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /Exam
        ///     {
        ///         "name":"Math test",
        ///         "description":"not so fun",
        ///         "dueDate":"2020-02-30"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="updateExamRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A newly created exam</returns>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="403">If the user is not the owner of the item </response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{id}")]
        public async Task<ActionResult<ExamResponse>> UpdateExam(int id, UpdateExamRequest updateExamRequest, CancellationToken cancellationToken)
        {
            var exam = await _mediator.Send(new UpdateExam.Command(
                   id, updateExamRequest.Name, updateExamRequest.Description, updateExamRequest.DueDate, User.GetUserId()), cancellationToken);

            return exam;
        }
    }
}
