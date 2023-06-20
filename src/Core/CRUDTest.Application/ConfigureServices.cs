using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CRUDTest.Application
{
    public static class ConfigureServices
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
    }
}
}
