﻿namespace BuildingBlocks.Core.Domain;

public class AuditAggregateRoot<TId> : AggregateRoot<TId>, IAuditEntity
{
    public virtual DateTime? CreatedDate { get; set; } = null!;
    public virtual string CreatedBy { get; set; } = null!;
    public virtual DateTime? LastModifiedDate { get; set; }
    public virtual string? LastModifiedBy { get; set; }
    public virtual DateTime? ValidFor { get; set; }
}