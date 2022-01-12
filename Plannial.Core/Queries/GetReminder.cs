using AutoMapper;
using MediatR;
using Plannial.Data.Interfaces;
using Plannial.Data.Models.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Plannial.Core.Queries
{
    public static class GetReminder
    {
        public record Query(string UserId, int ReminderId) : IRequest<ReminderResponse>;

        public class Handler : IRequestHandler<Query, ReminderResponse>
        {
            private readonly IReminderRepository _reminderRepository;
            private readonly IMapper _mapper;

            public Handler(IReminderRepository reminderRepository, IMapper mapper)
            {
                _reminderRepository = reminderRepository;
                _mapper = mapper;
            }
            public async Task<ReminderResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var reminder = await _reminderRepository.GetReminderAsync(request.UserId, request.ReminderId, cancellationToken);

                return _mapper.Map<ReminderResponse>(reminder);
            }
        }
    }
}
