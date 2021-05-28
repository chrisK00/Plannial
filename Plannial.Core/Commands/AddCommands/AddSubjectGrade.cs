using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Entities;

namespace Plannial.Core.Commands.AddCommands
{
    public static class AddSubjectGrade
    {
        public record Command(string UserId, int SubjectId, string Grade) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly ISubjectRepository _subjectRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;

            public Handler(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger)
            {
                _subjectRepository = subjectRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var subject = await _subjectRepository.GetSubjectByIdAsync(request.SubjectId, request.UserId, cancellationToken);
                if (subject == null)
                {
                    throw new KeyNotFoundException("Could not find subject");
                }

                if (!string.IsNullOrWhiteSpace(request.Grade))
                {
                    var grade = await _subjectRepository.GetGradeAsync(request.Grade, cancellationToken);
                    if (grade == null)
                    {
                        grade = new Grade { Value = request.Grade };
                    }

                    subject.Grade = grade;
                }
                else
                {
                    subject.Grade = null;
                }

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError($"Failed to apply grade in request: {request}");
                    throw new DbUpdateException("Failed to edit grade");
                }

                return Unit.Value;
            }
        }

    }
}
