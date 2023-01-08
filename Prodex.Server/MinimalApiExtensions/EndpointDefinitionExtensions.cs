using FluentValidation.Results;
using Prodex.Shared.Forms;
using Prodex.Shared.Validation;

namespace Prodex.Server.MinimalApiExtensions;

public static class EndpointDefinitionExtensions
{
    public static void AddEndpointDefinitions(this IServiceCollection services)
    {
        var defs = typeof(EndpointDefinitionExtensions).Assembly.ExportedTypes
            .Where(p => p.IsAssignableTo(typeof(IEndpointDefinition)) && p.IsClass)
            .ToList();

        foreach (var def in defs)
        {
            services.AddScoped(typeof(IEndpointDefinition), def);
        }
    }

    public static void UseEndpointDefinitions(this WebApplication app, string apiPrefix = null)
    {
        using var services = app.Services.CreateScope();
        var defs = services.ServiceProvider.GetServices<IEndpointDefinition>();
        RouteGroupBuilder group = null;

        if (apiPrefix != null)
        {
            group = app.MapGroup(apiPrefix).AddEndpointFilter<ValidationFilter>();
        }

        foreach (var def in defs)
        {
            if (group != null)
                def.DefineEndpoints(group.MapGroup(def.GroupName));
            else
                def.DefineEndpoints(app.MapGroup(def.GroupName));
        }
    }
}


public class ValidationFilter : IEndpointFilter
{
    public ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var errors = new ValidationErrors(context.Arguments.OfType<FormBaseModel>().SelectMany(p => p.Validate(null).Errors));

        return errors.HasErrors ?
            ValueTask.FromResult((object)Results.BadRequest(errors)) : 
            next(context);
    }
}
