using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Queries
{
    public static class GetHomework
    {
        public record Query(string UserId, int HomeworkId) : IRequest<HomeworkDetailResponse>;

        public class Handler : IRequestHandler<Query, HomeworkDetailResponse>
        {
            private readonly IMapper _mapper;
            private readonly IHomeworkRepository _homeworkRepository;

            public Handler(IMapper mapper, IHomeworkRepository homeworkRepository)
            {
                _mapper = mapper;
                _homeworkRepository = homeworkRepository;
            }

            public async Task<HomeworkDetailResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var homework = await _homeworkRepository.GetHomeworkAsync(request.HomeworkId, request.UserId, cancellationToken);
                return _mapper.Map<HomeworkDetailResponse>(homework);
            }
        }
    }
}
