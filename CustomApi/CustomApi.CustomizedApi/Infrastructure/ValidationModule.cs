using Autofac;
using CustomApi.CustomizedApi.Validation;
using CustomizedApi.Api.Controllers;
using FluentValidation;
using FluentValidation.WebApi;
using System.Linq;
using System.Reflection;

namespace CustomizedApi.Infrastructure
{
    public class ValidationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(LocationController)))
                .Where(type => type.Name.EndsWith("Validator"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<FluentValidationModelValidatorProvider>().As<System.Web.Http.Validation.ModelValidatorProvider>();

            builder.RegisterType<AutoFacValidatorFactory>().As<IValidatorFactory>().SingleInstance();

            base.Load(builder);
        }
    }
}