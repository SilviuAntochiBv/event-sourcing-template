using System;

using EventBasedServiceTemplate.Domain.Contracts.Persistence;
using EventBasedServiceTemplate.Domain.Example.AggregateRoots;
using EventBasedServiceTemplate.Persistence;
using EventBasedServiceTemplate.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Consul.AspNetCore;
using System.Text.Json;
using Consul;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIRegistrationHelper
    {
        public static void RegisterPersistenceComponents(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<AppDbContext>(contextCreationOptions =>
            {
                contextCreationOptions.UseNpgsql(configuration.GetConnectionString("EventStoreDb"), npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly("EventBasedServiceTemplate.Persistence");
                });
            });

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IAggregateQueryRepository<ExampleAggregate, Guid>, AggregateQueryRepository<ExampleAggregate, Guid>>();
            serviceCollection.AddScoped<IEventStoreRepository, EventStoreRepository>();
            serviceCollection.AddScoped<IAggregateStoreRepository, AggregateStoreRepository>();

            serviceCollection.AddConsul(consulClientConfiguration =>
            {
                consulClientConfiguration.Address = new Uri(configuration.GetConnectionString("ConsulClient"));
            });
            serviceCollection.AddSingleton<IRuntimeConfigurationStore, RuntimeConfigurationStore>(provider =>
                new RuntimeConfigurationStore(
                    provider.GetRequiredService<IConsulClient>(),
                    configuration["AppConfigurationPrefix"],
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
        }
    }
}
