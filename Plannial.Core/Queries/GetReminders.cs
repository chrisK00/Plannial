using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Queries
{
    public static class GetReminders
    {
        public record Query(string UserId, ReminderParams ReminderParams) : IRequest<IEnumerable<ReminderResponse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<ReminderResponse>>
        {
            private readonly IReminderRepository _reminderRepository;
            private readonly IMapper _mapper;

            public Handler(IReminderRepository reminderRepository, IMapper mapper)
            {
                _reminderRepository = reminderRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ReminderResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reminders = await _reminderRepository.GetRemindersAsync(request.UserId, request.ReminderParams, cancellationToken);
                return _mapper.Map<IEnumerable<ReminderResponse>>(reminders);
            }
        }
    }
}
