using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Plannial.Core.Interfaces;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Responses;

namespace Plannial.Core.Queries
{
    public static class GetReminders
    {
        public record Query(string UserId, ReminderParams ReminderParams) : IRequest<IEnumerable<ReminderResponse>>();


        public class Handler : IRequestHandler<Query, IEnumerable<ReminderResponse>>
        {
            private readonly IReminderRepository _reminderRepository;

            public Handler(IReminderRepository reminderRepository)
            {
                _reminderRepository = reminderRepository;
            }

            public async Task<IEnumerable<ReminderResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _reminderRepository.GetReminderResponsesAsync(request.UserId, request.ReminderParams, cancellationToken);
            }
        }
    }
}
