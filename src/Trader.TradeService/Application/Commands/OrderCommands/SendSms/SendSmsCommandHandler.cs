using BuildingBlocks.Core.Cqrs.Commands;
using MediatR;
using Trader.TradeService.Domain.Channel;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.Order.Exceptions;

namespace Trader.TradeService.Application.Commands.OrderCommands.SendSms;

public class SendSmsCommandHandler : ICommandHandler<SendSmsCommand>
{
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly ILogger<SendSmsCommandHandler> _logger;

    public SendSmsCommandHandler(IOrderCommandRepository orderCommandRepository, ILogger<SendSmsCommandHandler> logger)
    {
        _orderCommandRepository = orderCommandRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(SendSmsCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderCommandRepository.GetAsync(request.OrderId);
        if (order == null)
        {
            throw new NotFoundOrderException("order not found");
        }

        var orderNotification = order.OrderNotifications.First(x => x.ChannelId == Channel.Sms.Id);
        try
        {
            SendSms(request.GsmPhone, request.Text);
            orderNotification.Complete();
        }
        catch (Exception e)
        {
            orderNotification.Fail();
            _logger.LogError(e, e.Message);
        }

        return Unit.Value;
    }

    private void SendSms(string gsmPhone, string text)
    {
        _logger.LogInformation("SEND SMS: {0} - {1}", gsmPhone, text);
    }
}