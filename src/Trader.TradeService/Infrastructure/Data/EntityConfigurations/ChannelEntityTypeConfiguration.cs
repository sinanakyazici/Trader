using BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader.TradeService.Domain.Channel;

namespace Trader.TradeService.Infrastructure.Data.EntityConfigurations;

public class ChannelEntityTypeConfiguration : EntityTypeConfiguration<Channel, int>
{
    public override void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable("channel");

        builder.Property(x => x.Name).HasColumnName("name").IsRequired();

        base.Configure(builder);
    }
}