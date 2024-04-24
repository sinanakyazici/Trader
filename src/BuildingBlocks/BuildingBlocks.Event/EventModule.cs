using Autofac;
using BuildingBlocks.Core.Event;

namespace BuildingBlocks.Event;

public class EventModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EventBus>().As<IEventBus>().InstancePerLifetimeScope();
        builder.RegisterType<DomainEventService>().As<IDomainEventService>().InstancePerLifetimeScope();
        builder.RegisterType<IntegrationEventService>().As<IIntegrationEventService>().InstancePerLifetimeScope();
    }
}