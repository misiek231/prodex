using FluentMigrator.Runner;
using Prodex.Server;
using Prodex.Server.MinimalApiExtensions;

var builder = WebApplication.CreateBuilder(args);

Configuration.InitializeServices(builder);

var app = builder.Build();

Configuration.Initialize(app);

app.Run();

