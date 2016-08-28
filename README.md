# AutofacServiceFabricExample
Really poorly documented project with Autofac DI working with Service Fabric WebAPI

It sets up Autofac, configures a basic repo and a Reliable Service, before requesting the Repo (which in turn requests the service) via DI in the Controller. 

###Config in Startup.cs

```C#
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
```

###The repo in SomeRepo.cs

```C#
public interface ISomeRepo
{
    Task<string> GetSomething();
}

public class SomeRepo : ISomeRepo
{
    private readonly IStateless1 _service;

    public SomeRepo(IStateless1 service)
    {
        _service = service;
    }

    public async Task<string> GetSomething()
    {
        return await _service.GetHello();
    }
}
```

###Consuming it in the ValuesController.cs

```c#
public class ValuesController : ApiController
{
    private readonly ISomeRepo _someRepo;

    public ValuesController(ISomeRepo someRepo)
    {
        _someRepo = someRepo;
    }
    
    // GET api/values 
    public async Task<IHttpActionResult> Get()
    {
        return Ok(await _someRepo.GetSomething());
    }
}


```
