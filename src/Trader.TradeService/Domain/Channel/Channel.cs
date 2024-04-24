using BuildingBlocks.Core.Domain;

namespace Trader.TradeService.Domain.Channel;

public class Channel : Enumeration
{
    // order created
    public static readonly Channel Sms = new(1, nameof(Sms).ToLowerInvariant());
    // order done
    public static readonly Channel Email = new(2, nameof(Email).ToLowerInvariant());
    // cancel order
    public static readonly Channel PushNotification = new(3, nameof(PushNotification).ToLowerInvariant());

    public Channel(int id, string name) : base(id, name)
    {
    }
}