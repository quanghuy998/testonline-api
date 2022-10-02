using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TestOnlineProject.Infrastructure.CQRS.Commands;
using TestOnlineProject.Infrastructure.CQRS.Queries;

namespace TestOnlineProject.Infrastructure.CQRS
{
    public static class CqrsScExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<ICommandBus, CommandBus>();

            return services;
        }
    }
}
