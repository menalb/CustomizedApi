using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace CustomApi.Api.Validation
{
    public abstract class CompositeValidator<T> : AbstractValidator<T>
    {
        private List<IValidator> otherValidators = new List<IValidator>();

        protected void RegisterValidatorImplementation<TImplementation>(IValidator<TImplementation> validator)
        {
            otherValidators.Add(validator);
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var mainErrors = base.Validate(context).Errors;
            var errorsFromOtherValidators = otherValidators.SelectMany(x => x.Validate(context).Errors);
            var combinedErrors = mainErrors.Concat(errorsFromOtherValidators);

            return new ValidationResult(combinedErrors);
        }
    }
}