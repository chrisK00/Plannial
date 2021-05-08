using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
using Plannial.Core.Extensions;
using Plannial.Core.Models.Requests;
using Plannial.Core.Models.Responses;
using Plannial.Core.Queries;

namespace Plannial.Api.Controllers
{
    public class SubjectsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SubjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = nameof(GetSubjects))]
        public async Task<ActionResult<IEnumerable<SubjectResponse>>> GetSubjects(CancellationToken cancellationToken)
        {
            var subjects = await _mediator.Send(new GetSubjects.Query(User.GetUserId()), cancellationToken);
            return Ok(subjects);
        }

        [HttpPost]
        public async Task<ActionResult<SubjectResponse>> AddSubject(AddSubjectRequest addSubjectRequest, CancellationToken cancellationToken)
        {
            var subject = await _mediator.Send(new AddSubject.Command(
                addSubjectRequest.Name, addSubjectRequest.Description, User.GetUserId()), cancellationToken);
            return CreatedAtRoute(nameof(GetSubjects), subject);
        }

        [HttpPost("{subjectId}")]
        public async Task<ActionResult<ExamResponse>> AddExam(AddExamRequest addExamRequest, int subjectId, CancellationToken cancellationToken)
        {
            var exam = await _mediator.Send(new AddExam.Command(
                addExamRequest.Name, addExamRequest.Description, addExamRequest.DueDate, subjectId, User.GetUserId()), cancellationToken);

            return CreatedAtRoute(nameof(GetSubjects), exam);
        }

    }
}
