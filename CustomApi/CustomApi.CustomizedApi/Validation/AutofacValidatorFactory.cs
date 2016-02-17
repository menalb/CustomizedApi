using Autofac;
using FluentValidation;
using System;

namespace CustomApi.CustomizedApi.Validation
{
    public class AutoFacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext _context;

        public AutoFacValidatorFactory(IComponentContext context)
        {
            _context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            object instance;
            if (_context.TryResolve(validatorType, out instance))
            {
                var validator = instance as IValidator;
                return validator;
            }

            return null;
        }
    }
}