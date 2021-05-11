using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Commands
{
    public static class AddSubject
    {
        public record Command(string Name, string Description, string UserId) : IRequest<SubjectResponse>;

        public class Handler : IRequestHandler<Command, SubjectResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISubjectRepository _subjectRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(IMapper mapper, ISubjectRepository subjectRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _mapper = mapper;
                _subjectRepository = subjectRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<SubjectResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Creating new subject: {request}");

                var subject = new Subject
                {
                    UserId = request.UserId,
                    Description = request.Description,
                    Name = request.Name
                };

                await _subjectRepository.AddSubjectAsync(subject, cancellationToken);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError("Failed to add subject");
                    throw new DbUpdateException("Failed to create subject");
                }

                return _mapper.Map<SubjectResponse>(subject);
            }
        }
    }
}
