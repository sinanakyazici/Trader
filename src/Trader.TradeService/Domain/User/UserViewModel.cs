namespace Trader.TradeService.Domain.User;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string GsmPhone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public virtual DateTime? CreatedDate { get; set; } = null!;
    public virtual string CreatedBy { get; set; } = null!;
    public virtual DateTime? LastModifiedDate { get; set; }
    public virtual string? LastModifiedBy { get; set; }
    public virtual DateTime? ValidFor { get; set; }
}