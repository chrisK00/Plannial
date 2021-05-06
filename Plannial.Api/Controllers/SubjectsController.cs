using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
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
        public async Task<ActionResult<IEnumerable<SubjectResponse>>> GetSubjects()
        {
            //get userid from token
            var subjects = await _mediator.Send(new GetSubjects.Query());
            return Ok(subjects);
        }

        [HttpPost]
        public async Task<ActionResult<SubjectResponse>> AddSubject(AddSubjectRequest addSubjectRequest)
        {
            //get userid from token
            var subject = await _mediator.Send(new AddSubject.Command(addSubjectRequest.Name, addSubjectRequest.Description, ));
        }
    }
}
