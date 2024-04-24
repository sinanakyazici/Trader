using BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Infrastructure.Data.EntityConfigurations;

public class OrderEntityTypeConfiguration : AuditAggregateRootEntityTypeConfiguration<Order, Guid>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("order");
        builder.Property(ct => ct.UserId).HasColumnName("user_id");
        builder.Property(ct => ct.DayOfMonth).HasColumnName("day_of_month");
        builder.Property(ct => ct.Amount).HasColumnName("amount");
        builder.Property(ct => ct.OrderStatus).HasColumnName("order_status_id").HasConversion(ct => ct.Id, ct => Enumeration.Parse<OrderStatus>(ct));

        builder.HasMany(x => x.OrderChannels).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.OrderNotifications).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        base.Configure(builder);
    }
}