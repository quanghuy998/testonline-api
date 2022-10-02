using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestOnlineProject.Domain.Aggregates.TestAggregate;
using TestOnlineProject.Domain.SeedWork;
using TestOnlineProject.Infrastructure.Database;
using TestOnlineProject.Infrastructure.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TestOnlineProject.Domain.Aggregates.CandidateAggregate;
using TestOnlineProject.Domain.Aggregates.SubmissionAggregate;

namespace TestOnlineProject.Infrastructure.Domain
{
    public static class DomainScExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("appConnectionString"));
            });
            services.AddScoped<IUnitOfWork>(sp => new UnitOfWork(sp.GetRequiredService<AppDbContext>()));
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICandidateTestRepository, CandidateTestRepository>();

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();
                InitializeData.CreateInitializeData(db);
            }

            return services;
        }
    }
}
