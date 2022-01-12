using FluentValidation;

namespace Plannial.Core.Requests.Validators
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
