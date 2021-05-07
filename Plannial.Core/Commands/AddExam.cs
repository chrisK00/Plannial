using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Plannial.Core.Interfaces;
using Plannial.Core.Responses;

namespace Plannial.Core.Commands
{
    public static class AddExam
    {
        public record Command(string Name, string Description, DateTime DueDate, int subjectId):IRequest<ExamResponse>;

      /*  public class Handler : IRequestHandler<ExamResponse>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IMapper mapper, IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public Task<Unit> Handle(ExamResponse request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }*/
    }
}
