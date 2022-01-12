using AutoMapper;
using MediatR;
using Plannial.Data.Interfaces;
using Plannial.Data.Models.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Core.Queries
{
    public static class GetHomeworks
    {
        public record Query(int? SubjectId, string UserId) : IRequest<IEnumerable<HomeworkListResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<HomeworkListResponse>>
        {
            private readonly IHomeworkRepository _homeworkRepository;
            private readonly IMapper _mapper;

            public Handler(IHomeworkRepository homeworkRepository, IMapper mapper)
            {
                _homeworkRepository = homeworkRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<HomeworkListResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var homeworks = await _homeworkRepository.GetHomeworksAsync(request.UserId, request.SubjectId, cancellationToken);

                return _mapper.Map<IEnumerable<HomeworkListResponse>>(homeworks);
            }
        }
    }
}
