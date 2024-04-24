using BuildingBlocks.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using Trader.TradeService.Infrastructure.Data.EntityConfigurations;

namespace Trader.TradeService.Infrastructure.Data;

public class TradeContext : BaseDbContext
{
    public TradeContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory) 
        : base(configuration, httpContextAccessor, loggerFactory)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Order
        modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderChannelEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderNotificationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderNotificationStatusEntityTypeConfiguration());
        
        // User
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        
        // Channel
        modelBuilder.ApplyConfiguration(new ChannelEntityTypeConfiguration());

    }
}