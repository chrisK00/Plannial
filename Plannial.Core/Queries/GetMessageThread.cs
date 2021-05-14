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
        public record Query(string UserId,string OtherUserId) : IRequest<IEnumerable<MessageResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<MessageResponse>>
        {
            private readonly IMessageRepository _messageRepository;

            public Handler(IMessageRepository messageRepository)
            {
                _messageRepository = messageRepository;
            }

            public Task<IEnumerable<MessageResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var messages = _messageRepository.GetMessageThreadAsync(request.UserId, request.OtherUserId, cancellationToken);
                return messages;
            }
        }
    }
}
