﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;

namespace Plannial.Core.Commands.RemoveCommands
{
    public static class RemoveHomework
    {
        public record Command(string UserId, int HomeworkId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _subjectRepository = subjectRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var homework = await _subjectRepository.GetHomeworkAsync(request.HomeworkId, request.UserId, cancellationToken);

                if (homework == null)
                {
                    _logger.LogWarning($"User tried to access homework: {request.HomeworkId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Removing homework {request}");
                _subjectRepository.RemoveHomework(homework);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to remove homework");
                    throw new DbUpdateException("Failed to remove homework");
                }

                return Unit.Value;

            }
        }
    }
}
