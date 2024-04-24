using BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader.TradeService.Domain.Order;

namespace Trader.TradeService.Infrastructure.Data.EntityConfigurations;

public class OrderStatusEntityTypeConfiguration : EntityTypeConfiguration<OrderStatus, int>
{
    public override void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable("order_status");

        builder.Property(x => x.Name).HasColumnName("name").IsRequired();

        base.Configure(builder);
    }
}