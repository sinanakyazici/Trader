using BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Infrastructure.Data.EntityConfigurations;

public class OrderNotificationStatusEntityTypeConfiguration : EntityTypeConfiguration<OrderNotificationStatus, int>
{
    public override void Configure(EntityTypeBuilder<OrderNotificationStatus> builder)
    {
        builder.ToTable("order_notification_status");

        builder.Property(x => x.Name).HasColumnName("name").IsRequired();

        base.Configure(builder);
    }
}