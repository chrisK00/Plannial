using System;
using System.Collections.Generic;
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
    public static class AddHomework
    {
        public record Command(string Name, string Description, DateTime DueDate, int subjectId, Guid UserId) : IRequest<HomeworkResponse>;

        public class Handler : IRequestHandler<Command, HomeworkResponse>
        {
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ISubjectRepository _subjectRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork, ISubjectRepository subjectRepository, ILogger<Handler> logger)
            {
                _mapper = mapper;
                _unitOfWork = unitOfWork;
                _subjectRepository = subjectRepository;
                _logger = logger;
            }

            public async Task<HomeworkResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var homework = new Homework
                {
                    Name = request.Name,
                    Description = request.Description,
                    DueDate = request.DueDate,
                    UserId = request.UserId
                };

                var subject = await _subjectRepository.GetSubjectByIdAsync(request.subjectId, request.UserId, cancellationToken);

                if (subject == null)
                {
                    _logger.LogWarning($"User tried to access subject; {request.subjectId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Adding homework {request} to subject: {subject.Id}");
                subject.Homeworks.Add(homework);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to add exam");
                    throw new DbUpdateException("Failed to add exam");
                }
                return _mapper.Map<HomeworkResponse>(homework);
            }
        }
    }
}
