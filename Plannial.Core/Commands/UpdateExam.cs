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
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Commands
{
    public static class UpdateExam
    {
        public record Command(int ExamId, string Name, string Description, DateTime DueDate, string UserId) : IRequest<ExamResponse>;

        public class Handler : IRequestHandler<Command, ExamResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly ISubjectRepository _subjectRepository;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger, ISubjectRepository subjectRepository, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _subjectRepository = subjectRepository;
                _mapper = mapper;
            }

            public async Task<ExamResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var exam = await _subjectRepository.GetExamAsync(request.ExamId, request.UserId, cancellationToken);

                if (exam == null)
                {
                    _logger.LogWarning($"User tried to access exam: {request.ExamId}");
                    throw new UnauthorizedAccessException("You dont own this item");
                }

                _logger.LogInformation($"Updating exam {exam.Id} with the incoming request {request}");
                exam.Description = request.Description;
                exam.DueDate = request.DueDate;
                exam.Name = request.Name;

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogWarning($"No changes to exam {exam.Id}");
                }

                return _mapper.Map<ExamResponse>(exam);
            }
        }
    }
}
