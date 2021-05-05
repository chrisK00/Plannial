using FluentValidation;
using Plannial.Core.Requests;

namespace Plannial.Core.Validators
{
    public class AddReminderRequestValidator : AbstractValidator<AddReminderRequest>
    {
        public AddReminderRequestValidator()
        {
            RuleFor(x => x.Name).IsInEnum();
        }
    }
}
