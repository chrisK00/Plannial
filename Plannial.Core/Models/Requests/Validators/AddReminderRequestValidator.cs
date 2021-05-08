using FluentValidation;

namespace Plannial.Core.Models.Requests.Validators
{
    public class AddReminderRequestValidator : AbstractValidator<AddReminderRequest>
    {
        public AddReminderRequestValidator()
        {
            RuleFor(x => x.Name).IsInEnum();
        }
    }
}
