using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands.AddCommands;
using Plannial.Core.Commands.RemoveCommands;
using Plannial.Core.Extensions;
using Plannial.Core.Queries;
using Plannial.Core.Requests;
using Plannial.Data.Models.Params;
using Plannial.Data.Models.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
                new AddMessage.Command(User.GetUserId(), addMessageRequest.RecipientEmail, addMessageRequest.Content),cancellationToken);
    
            return CreatedAtRoute(nameof(GetMessages), new MessageParams { FilterBy = "Outbox" }, message);
        }

        [HttpGet(Name = nameof(GetMessages))]
        public async Task<ActionResult<IEnumerable<MessageResponse>>> GetMessages([FromQuery] MessageParams messageParams, CancellationToken cancellationToken)
        {
            var messages = await _mediator.Send(
                new GetMessages.Query(User.GetUserId(), messageParams), cancellationToken);

            return Ok(messages);
        }

        [HttpGet("{email}/thread")]
        public async Task<ActionResult<IEnumerable<MessageResponse>>> GetMessageThread(string email, CancellationToken cancellationToken)
        {
            var messages = await _mediator.Send(
                new GetMessageThread.Query(User.GetUserId(), email), cancellationToken);

            return Ok(messages);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{messageId}")]
        public async Task<ActionResult> RemoveMessage(int messageId)
        {
            await _mediator.Send(new RemoveMessage.Command(messageId, User.GetUserId()));
            return NoContent();
        }

    }
}
