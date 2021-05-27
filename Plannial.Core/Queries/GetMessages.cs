using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Plannial.Core.Interfaces;
using Plannial.Core.Mappings;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Queries
{
    public static class GetMessages
    {
        public record Query(string UserId, MessageParams MessageParams) : IRequest<IEnumerable<MessageResponse>>;

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
                var messages = await _messageRepository.GetMessagesAsync(request.UserId, request.MessageParams, cancellationToken);
                var messageResponses = new List<MessageResponse>(messages.Count());

                foreach (var message in messages)
                {
                    var sender =  await _userRepository.GetUserAsync(message.SenderId);
                    var recipient =  await _userRepository.GetUserAsync(message.RecipientId);

                    var messageResponse = MessageMapper.MapToMessageResponse(message);
                    messageResponse.SenderUsername = recipient.UserName;
                    messageResponse.RecipientUsername = sender.UserName;

                    messageResponses.Add(messageResponse);
                }

                return messageResponses;
            }
        }
    }
}
