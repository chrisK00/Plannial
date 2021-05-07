using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;
using Plannial.Core.Responses;

namespace Plannial.Core.Queries
{
    public static class GetSubjects
    {
        public record Query(string UserId) : IRequest<IEnumerable<SubjectResponse>>;

        public class Handle : IRequestHandler<Query, IEnumerable<SubjectResponse>>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly ILogger<Handle> _logger;

            public Handle(ISubjectRepository subjectRepository, ILogger<Handle> logger, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _logger = logger;
            }

            async Task<IEnumerable<SubjectResponse>> IRequestHandler<Query, IEnumerable<SubjectResponse>>.Handle(Query request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Getting subjects");
                return await _subjectRepository.GetSubjectResponsesAsync(request.UserId, cancellationToken);
            }
        }
    }
}
