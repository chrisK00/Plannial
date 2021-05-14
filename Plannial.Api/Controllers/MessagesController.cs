using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands;
using Plannial.Core.Extensions;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Requests;
using Plannial.Core.Models.Responses;
using Plannial.Core.Queries;

namespace Plannial.Api.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> AddMessage([FromBody]AddMessageRequest addMessageRequest, CancellationToken cancellationToken)
        {
            var message = await _mediator.Send(
                new AddMessage.Command(User.GetUserId(), addMessageRequest.RecipientId, addMessageRequest.Content),cancellationToken);
    
            return CreatedAtRoute(nameof(GetMessages), new MessageParams { FilterBy = "Outbox" }, message);
        }

        [HttpGet(Name = nameof(GetMessages))]
        public async Task<ActionResult<IEnumerable<MessageResponse>>> GetMessages([FromQuery]MessageParams messageParams, CancellationToken cancellationToken)
        {
            var messages = await _mediator.Send(
                new GetMessages.Query(User.GetUserId(), messageParams), cancellationToken);

            return Ok(messages);
        }

    }
}
