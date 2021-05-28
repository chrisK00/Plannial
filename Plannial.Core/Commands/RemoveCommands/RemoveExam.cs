﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;

namespace Plannial.Core.Commands.RemoveCommands
{
    public static class RemoveExam
    {
        public record Command(int ExamId, string UserId) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ISubjectRepository _subjectRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IUnitOfWork unitOfWork, ISubjectRepository subjectRepository, ILogger<Handler> logger)
            {
                _unitOfWork = unitOfWork;
                _subjectRepository = subjectRepository;
                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
          
                var exam = await _subjectRepository.GetExamAsync(request.ExamId, request.UserId, cancellationToken);

                if (exam == null)
                {
                    _logger.LogWarning($"User tried to access exam: {request.ExamId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Removing exam {request}");
                _subjectRepository.RemoveExam(exam);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to remove exam");
                    throw new DbUpdateException("Failed to remove exam");
                }

                return Unit.Value;
            }
        }
    }
}
