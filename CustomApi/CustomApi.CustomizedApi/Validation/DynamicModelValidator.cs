using CustomApi.Api.Validation;
using CustomApi.CustomizedApi.Models;
using CustomizedApi.Api.Models;
using FluentValidation;

namespace CustomApi.CustomizedApi.Validators
{
    public class ProductModelValidator : CompositeValidator<DynamicProductModel>
    {
        public ProductModelValidator()
        {
            RegisterValidatorImplementation(new CustomDynamicModelValidator());
        }
    }

    public class CustomDynamicModelValidator : AbstractValidator<CustomProductModel>
    {
        public CustomDynamicModelValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand is empty");
        }
    }   
}