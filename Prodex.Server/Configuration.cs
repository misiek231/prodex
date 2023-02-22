using AutoMapper;
using FluentMigrator.Runner;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Prodex.Bussines.Handlers.Processes;
using Prodex.Bussines.Services;
using Prodex.Bussines.Sitemap;
using Prodex.Data;
using Prodex.Server.MinimalApiExtensions;
using Prodex.Shared.Models.Processes;
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
            builder.Services.AddMediatR(typeof(Program).Assembly, typeof(FilterModel).Assembly, typeof(LoginHandler).Assembly, typeof(SitemapHandler).Assembly);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<PasswordHasher>();

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

            builder.Services.AddAuthorization();

            builder.Services.AddEndpointDefinitions();
        }
        public static void Initialize(WebApplication app)
        {
            using (var services = app.Services.CreateScope())
            {
                services.ServiceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
                services.ServiceProvider.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpointDefinitions("api");

            app.MapFallbackToFile("index.html");
        }
    }
}
