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
        public record Query(string UserId, string OtherUserEmail) : IRequest<IEnumerable<MessageResponse>>;

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
                var otherUser = await _userRepository.GetUserByEmailAsync(request.OtherUserEmail);

                if (otherUser == null)
                {
                    throw new KeyNotFoundException("Could not find user with the specified email");
                }
                var messages = await _messageRepository.GetMessageThreadAsync(request.UserId, otherUser.Id, cancellationToken);
                var user = await _userRepository.GetUserAsync(request.UserId);
                
                if (otherUser == null)
                {
                    throw new KeyNotFoundException("User with this email was not found");
                }

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
