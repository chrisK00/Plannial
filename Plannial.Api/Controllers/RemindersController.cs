using MediatR;
using Microsoft.AspNetCore.Mvc;
using Plannial.Core.Commands.AddCommands;
using Plannial.Core.Commands.RemoveCommands;
using Plannial.Core.Extensions;
using Plannial.Core.Queries;
using Plannial.Core.Requests;
using Plannial.Data.Models.Params;
using Plannial.Data.Models.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<ReminderResponse>>> GetReminders([FromQuery]ReminderParams reminderParams, CancellationToken cancellationToken)
        {
            var reminders = await _mediator.Send(new GetReminders.Query(User.GetUserId(), reminderParams), cancellationToken);
            return Ok(reminders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReminderResponse>> GetReminder(int id, CancellationToken cancellationToken)
        {
            var reminder = await _mediator.Send(new GetReminder.Query(User.GetUserId(), id), cancellationToken);
            return reminder;
        }

        [HttpDelete]
        public async Task<ActionResult<ReminderResponse>> RemoveReminders([FromQuery] IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            await _mediator.Send(new RemoveReminders.Command(User.GetUserId(), ids), cancellationToken);
            return NoContent();
        }
    }
}
