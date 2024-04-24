using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.User;

public class User : AuditAggregateRoot<Guid>
{
    public string Username { get; }
    public string Name { get; }
    public string Surname { get; }
    public string GsmPhone { get; }
    public string Email { get; }

    public User(string username, string name, string surname, string gsmPhone, string email)
    {
        Username = username;
        Name = name;
        Surname = surname;
        GsmPhone = gsmPhone;
        Email = email;
    }
}