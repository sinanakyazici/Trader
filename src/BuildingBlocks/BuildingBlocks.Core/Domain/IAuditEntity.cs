namespace BuildingBlocks.Core.Domain;

public interface IAuditEntity
{
    DateTime? CreatedDate { get; set; }
    string CreatedBy { get; set; }
    DateTime? LastModifiedDate { get; set; }
    string? LastModifiedBy { get; set; }
    DateTime? ValidFor { get; set; }
}