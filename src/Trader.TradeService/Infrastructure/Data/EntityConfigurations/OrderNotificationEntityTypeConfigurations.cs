using BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Infrastructure.Data.EntityConfigurations;

public class OrderNotificationEntityTypeConfiguration : AuditAggregateRootEntityTypeConfiguration<OrderNotification, Guid>
{
    public override void Configure(EntityTypeBuilder<OrderNotification> builder)
    {
        builder.ToTable("order_notification");
        builder.Property(ct => ct.OrderId).HasColumnName("order_id");
        builder.Property(ct => ct.UserId).HasColumnName("user_id");
        builder.Property(ct => ct.ChannelId).HasColumnName("channel_id");
        builder.Property(ct => ct.OrderNotificationStatus).HasColumnName("order_notification_status_id").HasConversion(ct => ct.Id, ct => Enumeration.Parse<OrderNotificationStatus>(ct));
        builder.Property(ct => ct.Text).HasColumnName("text");

        base.Configure(builder);
    }
}