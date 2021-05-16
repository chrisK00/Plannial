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

namespace Plannial.Core.Commands.AddCommands
{
    public static class AddExam
    {
        public record Command(string Name, string Description, DateTime DueDate, int SubjectId, string UserId) : IRequest<ExamResponse>;

        public class Handler : IRequestHandler<Command, ExamResponse>
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

            public async Task<ExamResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var exam = new Exam
                {
                    Name = request.Name,
                    Description = request.Description,
                    DueDate = request.DueDate,
                    UserId = request.UserId
                };

                var subject = await _subjectRepository.GetSubjectByIdAsync(request.SubjectId, request.UserId, cancellationToken);

                if (subject == null)
                {
                    _logger.LogWarning($"User tried to access subject: {request.SubjectId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Adding exam {request} to subject: {subject.Id}");
                subject.Exams.Add(exam);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to add exam");
                    throw new DbUpdateException("Failed to add exam");
                }
                return _mapper.Map<ExamResponse>(exam);
            }
        }
    }
}
