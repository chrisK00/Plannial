using AutoMapper;
using MediatR;
using Plannial.Data.Interfaces;
using Plannial.Data.Models.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Core.Queries
{
    public static class GetExam
    {
        public record Query(string UserId, int ExamId) : IRequest<ExamDetailResponse>;

        public class Handler : IRequestHandler<Query, ExamDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly IExamRepository _examRepository;

            public Handler(IMapper mapper, IExamRepository examRepository)
            {
                _mapper = mapper;
                _examRepository = examRepository;
            }

            public async Task<ExamDetailResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var exam = await _examRepository.GetExamAsync(request.ExamId, request.UserId, cancellationToken);
                return _mapper.Map<ExamDetailResponse>(exam);
            }
        }
    }
}
