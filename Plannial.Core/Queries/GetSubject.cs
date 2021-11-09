using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Queries
{
    public static class GetSubject
    {
        public record Query(string UserId, int Subjectid) : IRequest<SubjectDetailResponse>;

        public class Handler : IRequestHandler<Query, SubjectDetailResponse>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly IMapper _mapper;

            public Handler(ISubjectRepository subjectRepository, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _mapper = mapper;
            }

            public async Task<SubjectDetailResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var subject = await _subjectRepository.GetSubjectByIdAsync(request.Subjectid, request.UserId, cancellationToken);
                return _mapper.Map<SubjectDetailResponse>(subject);
            }
        }
    }
}
