using BuildingBlocks.Core.Cqrs.Commands;
using MediatR;
using Trader.TradeService.Domain.Channel;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.Order.Exceptions;

namespace Trader.TradeService.Application.Commands.OrderCommands.SendEmail;

public class SendEmailCommandHandler : ICommandHandler<SendEmailCommand>
{
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly ILogger<SendEmailCommandHandler> _logger;

    public SendEmailCommandHandler(IOrderCommandRepository orderCommandRepository, ILogger<SendEmailCommandHandler> logger)
    {
        _orderCommandRepository = orderCommandRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderCommandRepository.GetAsync(request.OrderId);
        if (order == null)
        {
            throw new NotFoundOrderException("order not found");
        }

        var orderNotification = order.OrderNotifications.First(x => x.ChannelId == Channel.Email.Id);
        try
        {
            SendEmail(request.Email, request.Text);
            orderNotification.Complete();
        }
        catch (Exception e)
        {
            orderNotification.Fail();
            _logger.LogError(e, e.Message);
        }

        return Unit.Value;
    }

    private void SendEmail(string email, string text)
    {
        _logger.LogInformation("SEND EMAIL: {0} - {1}", email, text);
    }
}