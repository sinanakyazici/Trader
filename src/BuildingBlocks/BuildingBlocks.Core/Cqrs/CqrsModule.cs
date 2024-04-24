using Autofac;
using BuildingBlocks.Core.Cqrs.Behaviors;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace BuildingBlocks.Core.Cqrs;

public class CqrsModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var entryAssembly = Assembly.GetEntryAssembly()!;
        
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
 
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(IValidator<>));
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(INotificationHandler<>));

        builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(TxBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        builder.Register<ServiceFactory>(ctx =>
        {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });
    }
}