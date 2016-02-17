using Autofac;
using Autofac.Integration.WebApi;
using CustomApi.CustomizedApi.Infrastructure;
using CustomizedApi.Api.Controllers;
using CustomizedApi.Infrastructure;
using FakeESB;
using Owin;
using System.Reflection;
using System.Web.Http;

namespace CustomApi.CustomizedApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            var container = GetConfiguredContainer(config);
            config.ConfigureWebApi(container);

            appBuilder
                .UseAutofacMiddleware(container)
                .UseAutofacWebApi(config)
                .UseWebApi(config);
        }       

        private static IContainer GetConfiguredContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(LocationController)));
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterModule<ValidationModule>();

            builder.RegisterType<FakeESB.FakeESB>().As<IFakeESB>();

            return builder.Build();
        }
    }    
}