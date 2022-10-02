using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestOnlineProject.Infrastructure.CQRS;

namespace TestOnlineProject.Application
{
    public static class CqrsScExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCqrs(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
