using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Plannial.Data.Interfaces;
using Plannial.Data.Models.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Core.Commands
{
    public static class UpdateExam
    {
        public record Command(int ExamId, string Name, string Description, DateTime DueDate, string UserId) : IRequest<ExamDetailResponse>;

        public class Handler : IRequestHandler<Command, ExamDetailResponse>
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

            public async Task<ExamDetailResponse> Handle(Command request, CancellationToken cancellationToken)
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
                    _logger.LogWarning($"No changes to exam {exam.Id}");
                }

                return _mapper.Map<ExamDetailResponse>(exam);
            }
        }
    }
}
