using BuildingBlocks.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildingBlocks.Core.Domain;

public class AuditEntityTypeConfiguration<T, TId> : EntityTypeConfiguration<T, TId> where T : AuditEntity<TId>
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(x => x.CreatedDate).HasColumnName("created_date").IsRequired();
        builder.Property(x => x.LastModifiedBy).HasColumnName("last_modified_by");
        builder.Property(x => x.LastModifiedDate).HasColumnName("last_modified_date");
        builder.Property(x => x.ValidFor).HasColumnName("valid_for");

        // auto ignore invalid entities
        builder.HasQueryFilter(x => !x.ValidFor.HasValue || (x.ValidFor.HasValue && x.ValidFor.Value >= DateExtensions.DateTimeNow));
        base.Configure(builder);
    }
}