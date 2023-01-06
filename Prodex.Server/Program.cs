using FluentMigrator.Runner;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prodex.Bussines.Handlers.Processes;
using Prodex.Data;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Processes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Todo: Register handlers in better way
builder.Services.AddMediatR(typeof(Program).Assembly, typeof(FilterModel).Assembly, typeof(CreateHandler).Assembly);

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(o => o.AddSqlServer()
                           .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
                           .ScanIn(typeof(DataContext).Assembly).For.Migrations());

builder.Services.AddEndpointDefinitions();

var app = builder.Build();

using (var services = app.Services.CreateScope())
{
    services.ServiceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpointDefinitions("api");

app.MapFallbackToFile("index.html");
app.Run();
