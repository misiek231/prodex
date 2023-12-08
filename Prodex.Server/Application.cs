namespace Prodex.Server;

public class Application
{
    public static WebApplication CreateApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Configuration.InitializeServices(builder);

        var app = builder.Build();

        Configuration.Initialize(app);

        return app;
    }
}
