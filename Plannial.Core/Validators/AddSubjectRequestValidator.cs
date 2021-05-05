using FluentValidation;
using Plannial.Core.Requests;

namespace Plannial.Core.Validators
{
    public class AddSubjectRequestValidator : AbstractValidator<AddSubjectRequest>
    {
        public AddSubjectRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
