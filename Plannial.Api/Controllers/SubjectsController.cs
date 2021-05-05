using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Queries;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectResponse>> GetSubjectById(int id)
        {
            var subject = await _mediator.Send(new GetSubjectById.Query(id));
            return subject;
        }
    }
}
