namespace Nihdi.DevoLearning.Presentation.Features.Persons.Create.Validators
{
    using FluentValidation;
    using Nihdi.DevoLearning.Presentation.Features.Persons.Create.Models;

    public class PersonCreateModelValidator : AbstractValidator<PersonCreateModel>
    {
        public PersonCreateModelValidator()
        {
            RuleFor(_ => _.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(_ => _.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(_ => _.GenderId)
                .NotNull();

            RuleFor(_ => _.CivilStateId)
                .NotNull();

            // Birth date should not be in the future
            RuleFor(_ => _.BirthDate)
                .LessThanOrEqualTo(DateTime.Today)
                .When(_ => _.BirthDate.HasValue);

            // Deceased date should not be in the future
            RuleFor(_ => _.DeceasedDate)
                .LessThanOrEqualTo(DateTime.Today)
                .When(_ => _.DeceasedDate.HasValue);

            RuleFor(_ => _.Email)
                .EmailAddress()
                .When(_ => !string.IsNullOrEmpty(_.Email));

            // Only validate Mobile length when Mobile has a value
            RuleFor(_ => _.Mobile)
                .Length(7, 20)
                .When(_ => !string.IsNullOrEmpty(_.Mobile));

            // When both dates are provided, DeceasedDate must be after BirthDate
            When(_ => _.BirthDate.HasValue && _.DeceasedDate.HasValue, () =>
            {
                RuleFor(_ => _.DeceasedDate)
                    .GreaterThan(_ => _.BirthDate);
            });
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<PersonCreateModel>.CreateWithOptions((PersonCreateModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
            {
                return Array.Empty<string>();
            }

            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
