using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Queries
{
    public static class GetMessageThread
    {
        public record Query(string UserId, string OtherUserId) : IRequest<IEnumerable<MessageResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<MessageResponse>>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IUserRepository _userRepository;

            public Handler(IMessageRepository messageRepository, IUserRepository userRepository)
            {
                _messageRepository = messageRepository;
                _userRepository = userRepository;
            }

            public async Task<IEnumerable<MessageResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var messages = await _messageRepository.GetMessageThreadAsync(request.UserId, request.OtherUserId, cancellationToken);
                var user = await _userRepository.GetUserAsync(request.UserId);
                var otherUser = await _userRepository.GetUserAsync(request.OtherUserId);

                foreach (var message in messages)
                {
                    message.SenderUsername = message.SenderId == request.UserId ? user.UserName : otherUser.UserName;
                    message.RecipientUsername = message.RecipientId == request.UserId ? user.UserName : otherUser.UserName;
                }
                return messages;
            }
        }
    }
}
