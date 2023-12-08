using FluentMigrator.Runner;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Prodex.Bussines.Services;
using Prodex.Bussines.SimpleRequests.Base;
using Prodex.Bussines.Sitemap;
using Prodex.Data;
using Prodex.Processes;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Users;
using System.Reflection;
using System.Text;

namespace Prodex.Server
{
    public static class Configuration
    {
        public static void InitializeServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataContext>(options =>
                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Todo: Register handlers in better way
            builder.Services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                    cfg.RegisterServicesFromAssembly(typeof(Shared.Models.ProductTemplates.FilterModel).Assembly);
                    cfg.RegisterServicesFromAssembly(typeof(GetSitemap).Assembly);
                    cfg.RegisterServicesFromAssembly(typeof(AfterStepExecutedRequest).Assembly);
                }
            );

            builder.Services.RegisterSimpleRequests();

            builder.Services.AddScoped<PasswordHasher>();
            builder.Services.AddScoped<ProcessBuilderService>();
            builder.Services.AddSingleton<ApiSelectCacheService>();

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(o => o.AddSqlServer()
                                       .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
                                       .ScanIn(typeof(DataContext).Assembly).For.Migrations());

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddAuthorization(p =>
            {
                p.AddPolicy(UserType.Admin.ToString(), q => q.RequireRole(UserType.Admin.ToString()));
                p.AddPolicy(UserType.Worker.ToString(), q => q.RequireRole(UserType.Worker.ToString()));
            });

            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddEndpointDefinitions();
        }
        public static void Initialize(WebApplication app)
        {
            using (var services = app.Services.CreateScope())
            {
                services.ServiceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();

            app.UseEndpointDefinitions("api");

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);
        }
    }
}
