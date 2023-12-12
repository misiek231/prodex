using System.Reflection;

namespace Prodex.Client.DiHelpers
{
    public static class DIExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            typeof(DIExtensions)
                .Assembly
                .GetTypes()
                .Where(p => p.GetCustomAttribute<RegisterScopedAttribute>() != null)
                .ToList()
                .ForEach(p => services.AddScoped(p));

            typeof(DIExtensions)
               .Assembly
               .GetTypes()
               .Where(p => p.GetCustomAttribute<RegisterSingletonAttribute>() != null)
               .ToList()
               .ForEach(p => services.AddSingleton(p));
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterScopedAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterSingletonAttribute : Attribute
    { }
}
