using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Event;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitForRabbitMq(this IServiceCollection services, RabbitMqConfig rabbitMqConfig)
    {
        services.AddMassTransit(x =>
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            x.AddConsumers(entryAssembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host: rabbitMqConfig.Hostname, port: (ushort)rabbitMqConfig.Port, rabbitMqConfig.VirtualHost, hostConfig =>
                {
                    hostConfig.Username(rabbitMqConfig.Username);
                    hostConfig.Password(rabbitMqConfig.Password);
                });

                cfg.ConfigureEndpoints(context, new DefaultEndpointNameFormatter(".", string.Empty, true));
            });
        });
        return services;
    }
}