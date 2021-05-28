﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Commands.AddCommands
{
    public static class AddHomework
    {
        public record Command(string Name, string Description, DateTime DueDate, int SubjectId, string UserId) : IRequest<HomeworkDetailResponse>;

        public class Handler : IRequestHandler<Command, HomeworkDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ISubjectRepository _subjectRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IMapper mapper,IUnitOfWork unitOfWork, ISubjectRepository subjectRepository, ILogger<Handler> logger)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _subjectRepository = subjectRepository;
                _logger = logger;
            }

            public async Task<HomeworkDetailResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!await _subjectRepository.SubjectExistsAsync(request.SubjectId, request.UserId, cancellationToken))
                {
                    _logger.LogWarning($"User tried to access subject; {request.SubjectId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Adding homework {request} to subject: {request.SubjectId}");
                var homework = new Homework
                {
                    Name = request.Name,
                    Description = request.Description,
                    DueDate = request.DueDate,
                    UserId = request.UserId,
                    SubjectId = request.SubjectId
                };

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to add exam");
                    throw new DbUpdateException("Failed to add exam");
                }

                return _mapper.Map<HomeworkDetailResponse>(homework);
            }
        }
    }
}
