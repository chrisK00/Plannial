using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
using Plannial.Core.Commands.AddCommands;
using Plannial.Core.Commands.RemoveCommands;
using Plannial.Core.Extensions;
using Plannial.Core.Queries;
using Plannial.Core.Requests;
using Plannial.Data.Models.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<ActionResult<ExamDetailResponse>> UpdateExam(int id, UpdateExamRequest updateExamRequest, CancellationToken cancellationToken)
        {
            var exam = await _mediator.Send(new UpdateExam.Command(
                   id, updateExamRequest.Name, updateExamRequest.Description, updateExamRequest.DueDate, User.GetUserId()), cancellationToken);

            return exam;
        }

        [HttpPost]
        public async Task<ActionResult<ExamDetailResponse>> AddExam(AddExamRequest addExamRequest, CancellationToken cancellationToken)
        {
            var exam = await _mediator.Send(new AddExam.Command(
                addExamRequest.Name, addExamRequest.Description, addExamRequest.DueDate, addExamRequest.SubjectId, User.GetUserId()),
                cancellationToken);

            return CreatedAtRoute(nameof(GetExam), new { exam.Id }, exam);
        }

        [HttpGet("{id}", Name = nameof(GetExam))]
        public async Task<ActionResult<ExamDetailResponse>> GetExam(int id)
        {
            var exam = await _mediator.Send(new GetExam.Query(User.GetUserId(), id));
            return exam;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamListResponse>>> GetExams([FromQuery] int? subjectId, CancellationToken cancellationToken)
        {
            var exams = await _mediator.Send(
                new GetExams.Query(subjectId, User.GetUserId()), cancellationToken);
            return Ok(exams);
        }
    }
}
