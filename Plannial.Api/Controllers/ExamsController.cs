﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Requests;
using Plannial.Core.Responses;

namespace Plannial.Api.Controllers
{
    [Route("api/subjects/{subjectId}/[controller]")]
    public class ExamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ExamResponse>> AddExam(AddExamRequest addExamRequest, int subjectId, CancellationToken cancellationToken)
        {
            var exam = new ExamResponse { Id = 5 };
        //    var exam = await _mediator.Send(new AddExam.Command(
          //      addExamRequest.Name, addExamRequest.Description, addExamRequest.DueDate,subjectId), cancellationToken);
            return Created($"subjects/{subjectId}", exam);
        }
    }
}
