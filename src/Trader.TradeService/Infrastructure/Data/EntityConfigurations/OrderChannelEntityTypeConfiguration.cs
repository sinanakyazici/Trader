using BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Infrastructure.Data.EntityConfigurations;

public class OrderChannelEntityTypeConfiguration : AuditEntityTypeConfiguration<OrderChannel, Guid>
{
    public override void Configure(EntityTypeBuilder<OrderChannel> builder)
    {
        builder.ToTable("order_channel");
        builder.Property(ct => ct.OrderId).HasColumnName("order_id");
        builder.Property(ct => ct.ChannelId).HasColumnName("channel_id");

        base.Configure(builder);
    }
}