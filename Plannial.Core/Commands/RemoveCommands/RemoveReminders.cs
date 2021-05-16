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
    public static class RemoveReminders
    {
        public record Command(string UserId, IEnumerable<int> ReminderIds) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IReminderRepository _reminderRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, IReminderRepository reminderRepository, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _reminderRepository = reminderRepository;
                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var reminders = await _reminderRepository.GetRemindersAsync(request.UserId, request.ReminderIds);
               
                if (reminders.Count < 1)
                {
                    _logger.LogWarning($"Could not find any reminders from request {request}");
                    throw new KeyNotFoundException("Could not find any reminders");
                }

                _reminderRepository.RemoveReminders(reminders);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    throw new DbUpdateException("Failed to remove reminders");
                }

                return Unit.Value;
            }
        }
    }
}
