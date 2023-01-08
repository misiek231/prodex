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
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterScopedAttribute : Attribute
    { }
}
