using Autofac;
using BuildingBlocks.Data.EfCore;

namespace Trader.TradeService.Infrastructure.Data;

public class TradeContextModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TradeContext>().As<BaseDbContext>().InstancePerLifetimeScope();
    }
}
