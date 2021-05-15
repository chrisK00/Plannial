using FluentValidation;

namespace Plannial.Core.Models.Requests.Validators
{
    public class AddSubjectGradeRequestValidator : AbstractValidator<AddSubjectGradeRequest>
    {
        public AddSubjectGradeRequestValidator()
        {
            RuleFor(x => x.Grade).MaximumLength(1);
        }
    }
}
