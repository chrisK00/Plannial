using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Plannial.Core.Interfaces;
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
            private readonly IMapper _mapper;

            public Handler(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper)
            {
                _messageRepository = messageRepository;
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<MessageResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var messages = await _messageRepository.GetMessagesAsync(request.UserId, request.MessageParams, cancellationToken);
                var messageResponses = _mapper.Map<IEnumerable<MessageResponse>>(messages);

                foreach (var message in messageResponses)
                {
                    var sender =  await _userRepository.GetUserAsync(message.SenderId);
                    var recipient =  await _userRepository.GetUserAsync(message.RecipientId);

                    message.SenderUsername = recipient.UserName;
                    message.RecipientUsername = sender.UserName;
                }

                return messageResponses;
            }
        }
    }
}
