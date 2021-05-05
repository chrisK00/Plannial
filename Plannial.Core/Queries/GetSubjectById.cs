using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;
using Plannial.Core.Responses;

namespace Plannial.Core.Queries
{
    public static class GetSubjectById
    {
        public record Query(int id) : IRequest<SubjectResponse>;

        public class Handle : IRequestHandler<Query, SubjectResponse>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly ILogger<Handle> _logger;

            public Handle(ISubjectRepository subjectRepository, ILogger<Handle> logger)
            {
                _subjectRepository = subjectRepository;
                _logger = logger;
            }

            Task<SubjectResponse> IRequestHandler<Query, SubjectResponse>.Handle(Query request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Getting subject: {request.id}");
                var subject = _subjectRepository.GetSubjectResponseById(request.id);
                return subject;
            }
        }
    }
}
