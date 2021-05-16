using FluentValidation;
using Plannial.Core.Helpers;

namespace Plannial.Core.Models.Requests.Validators
{
    public class AddReminderRequestValidator : AbstractValidator<AddReminderRequest>
    {
        public AddReminderRequestValidator()
        {
            RuleFor(x => x.Priority).IsInEnum();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
