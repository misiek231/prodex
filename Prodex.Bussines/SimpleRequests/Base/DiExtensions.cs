using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Prodex.Data.Interfaces;
using Prodex.Shared.Pagination;
using Riok.Mapperly.Abstractions;
using System.Reflection;

namespace Prodex.Bussines.SimpleRequests.Base;

public static class DiExtensions
{
    public static IServiceCollection RegisterSimpleRequests(this IServiceCollection services)
    {
        var configs = typeof(DiExtensions).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IConfigurator)))
            .Select(p => Activator.CreateInstance(p) as IConfigurator);

        var con = new SimpleRequestConfig(services);

        foreach (var config in configs)
        {
            config.Configure(con);
        }

        typeof(DiExtensions).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.GetInterfaces().Any(p => p.Name.Contains("IFilter")))
            .ToList()
            .ForEach(p => services.AddScoped(p.GetInterfaces().Single(), p));

        typeof(DiExtensions).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.GetCustomAttribute<MapperAttribute>() != null)
            .SelectMany(impl => impl.GetInterfaces().Select(inter => (inter, impl)))
            .ToList()
            .ForEach(p => services.AddScoped(p.inter, p.impl));

        return services;
    }
}

public class SimpleRequestConfig
{

    private readonly IServiceCollection Services;

    public SimpleRequestConfig(IServiceCollection services)
    {
        Services = services;
    }

    public SimpleRequestConfig AddListConfig<TEntity, TFilter, TListItemModel>() where TEntity : class
    {
        Services.AddScoped<IRequestHandler<SimpleGetList.Request<TEntity, TFilter, TListItemModel>, Pagination<TListItemModel>>,
            SimpleGetList.Handler<TEntity, TFilter, TListItemModel>>();

        return this;
    }

    public SimpleRequestConfig AddCreateConfig<TEntity, TForm>() where TEntity : class
    {
        Services.AddScoped<IRequestHandler<SimpleCreate.Request<TEntity, TForm>, object>,
            SimpleCreate.Handler<TEntity, TForm>>();

        return this;
    }

    public SimpleRequestConfig AddUpdateConfig<TEntity, TForm>() where TEntity : class
    {
        Services.AddScoped<IRequestHandler<SimpleUpdate.Request<TEntity, TForm>, object>,
            SimpleUpdate.Handler<TEntity, TForm>>();

        return this;
    }

    public SimpleRequestConfig AddDetailsConfig<TEntity, TDetails>() where TEntity : class, IEntity
    {
        Services.AddScoped<IRequestHandler<SimpleGetDetails.Request<TEntity, TDetails>, TDetails>,
            SimpleGetDetails.Handler<TEntity, TDetails>>();

        return this;
    }
}

public interface IConfigurator
{
    void Configure(SimpleRequestConfig config);
}





