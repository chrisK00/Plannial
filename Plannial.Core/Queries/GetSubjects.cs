using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Queries
{
    public static class GetSubjects
    {
        public record Query(string UserId) : IRequest<IEnumerable<SubjectResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<SubjectResponse>>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;

            public Handler(ISubjectRepository subjectRepository, ILogger<Handler> logger, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<IEnumerable<SubjectResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Getting subjects");
                var subjects = await _subjectRepository.GetSubjectsAsync(request.UserId, cancellationToken);
                return _mapper.Map<IEnumerable<SubjectResponse>>(subjects);
            }
        }
    }
}
