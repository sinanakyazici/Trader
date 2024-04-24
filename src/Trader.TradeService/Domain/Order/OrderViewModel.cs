using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.Order;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int DayOfMonth { get; set; }
    public decimal Amount { get; set; }
    public int OrderStatusId { get; set; }
    public string OrderStatusName { get; set; } = null!;

    public virtual DateTime? CreatedDate { get; set; } = null!;
    public virtual string CreatedBy { get; set; } = null!;
    public virtual DateTime? LastModifiedDate { get; set; }
    public virtual string? LastModifiedBy { get; set; }
    public virtual DateTime? ValidFor { get; set; }
}