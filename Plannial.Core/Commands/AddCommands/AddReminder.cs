using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plannial.Core.Helpers;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Responses;
using Plannial.Core.Repositories;

namespace Plannial.Core.Commands.AddCommands
{
    public static class AddReminder
    {
        public record Command(string UserId, string Name, string Description, DateTime? DueDate, Priority Priority)
            : IRequest<ReminderResponse>();

        public class Handler : IRequestHandler<Command, ReminderResponse>
        {
            private readonly IReminderRepository _reminderRepository;
            private readonly IMapper _mapper;
            private readonly ILogger<Handler> _logger;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IReminderRepository reminderRepository, IMapper mapper, ILogger<Handler> logger, IUnitOfWork unitOfWork)
            {
                _reminderRepository = reminderRepository;
                _mapper = mapper;
                _logger = logger;
                _unitOfWork = unitOfWork;
            }

            public async Task<ReminderResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Incoming add reminder request => {request}");
                var reminder = new Reminder
                {
                    UserId = request.UserId,
                    Description = request.Description,
                    DueDate = request.DueDate,
                    Name = request.Name,
                    Priority = request.Priority
                };

                await _reminderRepository.AddReminderAsync(reminder, cancellationToken);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to add reminder");
                    throw new DbUpdateException("Failed to add reminder");
                }

                return _mapper.Map<ReminderResponse>(reminder);
            }
        }
    }
}
