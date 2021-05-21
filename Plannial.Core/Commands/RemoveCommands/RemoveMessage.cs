using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;

namespace Plannial.Core.Commands.RemoveCommands
{
    public static class RemoveMessage
    {
        public record Command(int MessageId, string UserId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMessageRepository _messageRepository;
            private readonly ILogger<Handler> _logger;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IMessageRepository messageRepository, ILogger<Handler> logger, IUnitOfWork unitOfWork)
            {
                _messageRepository = messageRepository;
                _logger = logger;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var message = await _messageRepository.GetMessage(request.MessageId, request.UserId);

                if (message == null)
                {
                    _logger.LogWarning($"User tried to access message: {request.MessageId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                if (message.SenderId == request.UserId)
                {
                    message.SenderDeleted = true;
                }
                else
                {
                    message.RecipientDeleted = true;
                }

                if (message.RecipientDeleted && message.SenderDeleted)
                {
                    _messageRepository.RemoveMessage(message);
                }

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError($"Failed to update message {message.Id} with the incoming request {request}");
                    throw new DbUpdateException("Failed to save changes");
                }

                return Unit.Value;
            }
        }

    }
}
