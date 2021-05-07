using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
using Plannial.Core.Extensions;
using Plannial.Core.Queries;
using Plannial.Core.Requests;
using Plannial.Core.Responses;

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
    }
}
