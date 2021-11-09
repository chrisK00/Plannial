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
            private readonly IMessageReadRepository _messageRepository;

            public Handler(IMessageReadRepository messageRepository)
            {
                _messageRepository = messageRepository;
            }

            public async Task<IEnumerable<MessageResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var messages = await _messageRepository.GetMessagesAsync(request.UserId, request.MessageParams, cancellationToken);
                return messages;
            }
        }
    }
}
