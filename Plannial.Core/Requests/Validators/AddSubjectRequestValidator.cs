using FluentValidation;

namespace Plannial.Core.Requests.Validators
{
    public class AddSubjectRequestValidator : AbstractValidator<AddSubjectRequest>
    {
        public AddSubjectRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
