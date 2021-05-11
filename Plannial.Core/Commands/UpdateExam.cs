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
        public record Command(int ExamId, string Name, string Description, DateTime DueDate,Guid UserId) : IRequest<ExamResponse>;

        public class Handler : IRequestHandler<Command, ExamResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IExamRepository _examRepository;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, ILogger<Handler> logger, IExamRepository examRepository, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
                _examRepository = examRepository;
                _mapper = mapper;
            }

            public async Task<ExamResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var exam = await _examRepository.GetExamAsync(request.ExamId, request.UserId, cancellationToken);

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
                    _logger.LogError($"Failed to update exam {exam.Id}");
                    throw new DbUpdateException("Failed to update exam");
                }

                return _mapper.Map<ExamResponse>(exam);
            }
        }
    }
}
