using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands.AddCommands;
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

        /// <summary>
        /// Adds a new subject
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Subject
        ///     {
        ///         "name":"Math",
        ///         "description:"not so fun"
        ///     }
        ///     
        ///     POST /Subject
        ///     {
        ///         "name":"Math"
        ///     }
        /// </remarks>
        /// <param name="addSubjectRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A newly created subject</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">In case of validation errors or if the database was not able to add the item</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubjectResponse>> AddSubject(AddSubjectRequest addSubjectRequest, CancellationToken cancellationToken)
        {
            var subject = await _mediator.Send(new AddSubject.Command(
                addSubjectRequest.Name, addSubjectRequest.Description, User.GetUserId()), cancellationToken);
            return CreatedAtRoute(nameof(GetSubjects), subject);
        }

        /// <summary>
        /// Edits the subject's grade property
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Subject/grade
        ///     {
        ///         grade:""   
        ///     }
        ///     
        ///     POST /Subject/grade
        ///     {
        ///         grade:"A"   
        ///     }
        /// </remarks>
        /// <param name="addSubjectGradeRequest"></param>
        /// <param name="subjectId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="204">Set the subject's grade successfully</response>
        /// <response code="404">Could not find the specified subject</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{subjectId}/grade")]
        public async Task<ActionResult> AddSubjectGrade(AddSubjectGradeRequest addSubjectGradeRequest, int subjectId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddSubjectGrade.Command(User.GetUserId(), subjectId, addSubjectGradeRequest.Grade), cancellationToken);
            return NoContent();
        }
    }
}
