using Prodex.Server;
using Prodex.Server.MinimalApiExtensions;

namespace Prodex.ClientGenerator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Configuration.InitializeServices(builder);

            var app = builder.Build();

            Configuration.Initialize(app);

            using (var services = app.Services.CreateScope())
            {
                var appLifetime = services.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();

                appLifetime.ApplicationStarted.Register(() =>
                {
                    using (var services = app.Services.CreateScope())
                    {
                        var routes = services.ServiceProvider.GetRequiredService<EndpointDataSource>();

                        new ClientGenerator().Generate(routes);
                    }

                    appLifetime.StopApplication();
                });
            }

            app.Run();
        }
    }
}