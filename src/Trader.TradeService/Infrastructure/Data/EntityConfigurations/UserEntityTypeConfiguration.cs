using BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trader.TradeService.Domain.User;

namespace Trader.TradeService.Infrastructure.Data.EntityConfigurations;

public class UserEntityTypeConfiguration : AuditAggregateRootEntityTypeConfiguration<User, Guid>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.Property(ct => ct.Username).HasColumnName("username").HasMaxLength(100).IsRequired();
        builder.Property(ct => ct.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(ct => ct.Surname).HasColumnName("surname").HasMaxLength(100).IsRequired();
        builder.Property(ct => ct.GsmPhone).HasColumnName("gsm_phone").HasMaxLength(100).IsRequired();
        builder.Property(ct => ct.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
        base.Configure(builder);
    }
}