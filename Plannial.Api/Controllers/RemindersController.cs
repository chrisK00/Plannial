using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands.AddCommands;
using Plannial.Core.Extensions;
using Plannial.Core.Models.Params;
using Plannial.Core.Models.Requests;
using Plannial.Core.Models.Responses;
using Plannial.Core.Queries;

namespace Plannial.Api.Controllers
{
    public class RemindersController : BaseApiController
    {
        private readonly IMediator _mediator;

        public RemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ReminderResponse>> AddReminder(AddReminderRequest addReminderRequest, CancellationToken cancellationToken)
        {
            var reminder = await _mediator.Send(
                new AddReminder.Command(User.GetUserId(), addReminderRequest.Name, addReminderRequest.Description, addReminderRequest.DueDate,
                addReminderRequest.Priority), cancellationToken);
            return reminder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReminderResponse>>> GetReminders(ReminderParams reminderParams, CancellationToken cancellationToken)
        {
            var reminders = await _mediator.Send(new GetReminders.Query(User.GetUserId(), reminderParams), cancellationToken);
            return Ok(reminders);
        }
    }
}
