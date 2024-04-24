using Autofac;
using BuildingBlocks.Core.Domain;
using BuildingBlocks.Data.EfCore;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace BuildingBlocks.Data;

public class DataModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var entryAssembly = Assembly.GetEntryAssembly()!;

        builder.RegisterType<HttpContextAccessor>().AsImplementedInterfaces();
        builder.RegisterType<EfUnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterAssemblyTypes(entryAssembly).AssignableTo<IQueryRepository>().AsImplementedInterfaces();
        builder.RegisterAssemblyTypes(entryAssembly).AsClosedTypesOf(typeof(ICommandRepository<>));
    }
}
