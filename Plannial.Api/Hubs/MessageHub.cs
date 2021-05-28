using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Plannial.Core.Commands.AddCommands;
using Plannial.Core.Extensions;
using Plannial.Core.Models.Requests;
using Plannial.Core.Queries;

namespace Plannial.Api.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMediator _mediator;

        public MessageHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            var otherUserEmail = Context.GetHttpContext().Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUserId(), otherUserEmail);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var messages = await _mediator.Send(new GetMessageThread.Query(Context.User.GetUserId(), otherUserEmail));

            await Clients.Group(groupName).SendAsync("MessageThread", messages);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task AddMessage(AddMessageRequest addMessageRequest)
        {
            var message = await _mediator.Send(
              new AddMessage.Command(Context.User.GetUserId(), addMessageRequest.RecipientEmail, addMessageRequest.Content));
            var groupName = GetGroupName(Context.User.GetUserId(), addMessageRequest.RecipientEmail);

            await Clients.Group(groupName).SendAsync("NewMessage", message);
        }

        private static string GetGroupName(string userId, string otherUserId)
        {
            //evaluates the values of each char in the strings.
            var userIdIsLessThanOtherUserId = string.CompareOrdinal(userId, otherUserId) < 0;
            return userIdIsLessThanOtherUserId ? $"{userId}-{otherUserId}" : $"{otherUserId}-{userId}"; 
        }
    }
}
