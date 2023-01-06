using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Prodex.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(o => o.AddSqlServer()
                           .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
                           .ScanIn(typeof(DataContext).Assembly).For.Migrations());

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
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
