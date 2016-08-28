using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Owin;
using Stateless1;
using WebApi1.Model;

namespace WebApi1
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {

            var builder = new ContainerBuilder();
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            builder.RegisterWebApiFilterProvider(config);

            //Register the repo that our code will use to abstract the end code one level from the actor
            builder.RegisterType<SomeRepo>().As<ISomeRepo>();

            //Register the actor.
            builder.Register((e) => ServiceProxy.Create<IStateless1>(new Uri("fabric:/Application3/Stateless1")))
                .As<IStateless1>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            
            appBuilder.UseWebApi(config);
        }
    }
}
