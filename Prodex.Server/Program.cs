using Prodex.Server;

var builder = WebApplication.CreateBuilder(args);

Configuration.InitializeServices(builder);

var app = builder.Build();

Configuration.Initialize(app);

app.Run();

