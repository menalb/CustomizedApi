using Autofac;
using Autofac.Integration.WebApi;
using CustomApi.CustomizedApi.Models;
using CustomizedApi.Api.Filters;
using CustomizedApi.Api.Formatters;
using FluentValidation.WebApi;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CustomApi.CustomizedApi.Infrastructure
{
    public static class ConsfigurationExtensions
    {
        public static void ConfigureFluentValidator(this HttpConfiguration config)
        {
            FluentValidationModelValidatorProvider.Configure(config);
        }

        public static void ConfigureFormatters(this HttpConfiguration config)
        {
            config.Formatters.Add(new CustomFormatter<Location>(new MediaTypeHeaderValue("application/custom-location-type")));
            config.Formatters.Add(new CustomFormatter<CustomProductModel>(new MediaTypeHeaderValue("application/custom-productmodel-type")));
        }

        public static void ConfigureFilters(this HttpConfiguration config)
        {
            config.Filters.Add(new ModelStateFilterAttribute());
        }

        public static void ConfigureWebApi(this HttpConfiguration config, IContainer container)
        {
            config.ConfigureFormatters();
            config.ConfigureFilters();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.ConfigureFluentValidator();
        }
    }
}