using FluentValidation.Results;
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
        var models = context.Arguments.Where(p => p.GetType().GetInterfaces().Where(p => p.Name.Contains("IValidatable")).Any()).ToList();

        foreach (var p in models)
        {
            var modelType = p.GetType();
            var validatorType = typeof(FluentValidator<>).MakeGenericType(modelType);
            var validator = Activator.CreateInstance(validatorType);

            modelType.GetMethod("Rules").Invoke(p, new object[] { null, validator });

            var result = validatorType.GetMethod("Validate", new Type[] { modelType }).Invoke(validator, new object[] { p }) as ValidationResult;

            if (!result.IsValid)
                return ValueTask.FromResult((object)Results.BadRequest(result.Errors.Select(p => new KeyValuePair<string, string>(p.PropertyName, p.ErrorMessage))));

        }

        return next(context);
    }
}
