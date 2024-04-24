namespace Trader.TradeService.Domain.Order;

public class OrderChannelViewModel
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public int ChannelId { get; set; }
    public string ChannelName { get; set; } = null!;

    public virtual DateTime? CreatedDate { get; set; } = null!;
    public virtual string CreatedBy { get; set; } = null!;
    public virtual DateTime? LastModifiedDate { get; set; }
    public virtual string? LastModifiedBy { get; set; }
    public virtual DateTime? ValidFor { get; set; }
}