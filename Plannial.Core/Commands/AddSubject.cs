using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Plannial.Core.Entities;
using Plannial.Core.Interfaces;
using Plannial.Core.Responses;

namespace Plannial.Core.Commands
{
    public static class AddSubject
    {
        public record Command(string Name, string Description, string UserId) : IRequest<SubjectResponse>;

        class Handler : IRequestHandler<Command, SubjectResponse>
        {
            private readonly IMapper _mapper;
            private readonly ISubjectRepository _subjectRepository;

            public Handler(IMapper mapper, ISubjectRepository subjectRepository)
            {
                _mapper = mapper;
                _subjectRepository = subjectRepository;
            }

            async Task<SubjectResponse> IRequestHandler<Command, SubjectResponse>.Handle(Command request, CancellationToken cancellationToken)
            {
                var subject = new Subject { UserId = request.UserId, Description = request.Description, Name = request.Name };
                //add subjecttorepo
                await _subjectRepository.AddSubjectAsync(subject);
                return _mapper.Map<SubjectResponse>(subject);
            }
        }
    }
}
