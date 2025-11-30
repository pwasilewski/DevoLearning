namespace Nihdi.DevoLearning.Presentation.Features.Persons.Create.Validators
{
    using FluentValidation;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Create.Models;
    using Nihdi.DevoLearning.Presentation.Resources;

    public class PersonCreateModelValidator : AbstractValidator<PersonCreateModel>
    {
        public PersonCreateModelValidator()
        {
            RuleFor(_ => _.FirstName)
                .NotEmpty().WithMessage(ValidationMessage.Required)
                .MaximumLength(100);

            RuleFor(_ => _.LastName)
                .NotEmpty().WithMessage(ValidationMessage.Required)
                .MaximumLength(100);

            RuleFor(_ => _.GenderId)
                .NotNull().WithMessage(ValidationMessage.Required);

            RuleFor(_ => _.CivilStateId)
                .NotNull().WithMessage(ValidationMessage.Required);

            // Birth date should not be in the future
            RuleFor(_ => _.BirthDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessage.DateNotInFuture)
                .When(_ => _.BirthDate.HasValue);

            // Deceased date should not be in the future
            RuleFor(_ => _.DeceasedDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage(ValidationMessage.Required)
                .When(_ => _.DeceasedDate.HasValue);

            RuleFor(_ => _.Email)
                .EmailAddress().WithMessage(ValidationMessage.InvalidEmail)
                .When(_ => !string.IsNullOrEmpty(_.Email));

            // Only validate Mobile length when Mobile has a value
            RuleFor(_ => _.Mobile)
                .Length(7, 20).WithMessage(ValidationMessage.Phone_Length)
                .When(_ => !string.IsNullOrEmpty(_.Mobile));

            // When both dates are provided, DeceasedDate must be after BirthDate
            When(_ => _.BirthDate.HasValue && _.DeceasedDate.HasValue, () =>
            {
                RuleFor(_ => _.DeceasedDate)
                    .GreaterThan(_ => _.BirthDate).WithMessage(ValidationMessage.DeceaseDate_AfterBirthDate);
            });
        }
    }
}
