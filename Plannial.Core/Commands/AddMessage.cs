using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Commands
{
    public static class AddMessage
    {
        public record Command(string SenderId, string RecipientId, string Content) : IRequest<MessageResponse>;

        public class Handler : IRequestHandler<Command, MessageResponse>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;

            public Handler(IMessageRepository messageRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger, IMapper mapper)
            {
                _messageRepository = messageRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<MessageResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var message = new Message
                {
                    RecipientId = request.RecipientId,
                    SenderId = request.SenderId,
                    Content = request.Content
                };

                _logger.LogInformation($"Sending messge to {request.RecipientId}");
                await _messageRepository.AddMessageAsync(message, cancellationToken);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to send message");
                    throw new DbUpdateException("Failed to send message");
                }

                return _mapper.Map<MessageResponse>(message);
            }
        }
    }
}
