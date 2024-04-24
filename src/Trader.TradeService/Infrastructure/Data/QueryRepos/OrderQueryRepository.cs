using BuildingBlocks.Data.Dapper;
using Dapper;
using Npgsql;
using Trader.TradeService.Domain.Channel;
using Trader.TradeService.Domain.Order;
using Trader.TradeService.Domain.User;

namespace Trader.TradeService.Infrastructure.Data.QueryRepos;

public class OrderQueryRepository : DapperRepository, IOrderQueryRepository
{
    public OrderQueryRepository(IConfiguration configuration) : base(configuration)
    {
    }


    public async Task<IEnumerable<OrderChannelViewModel>> GetOrderChannels(Guid orderId)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@orderId", orderId);

        var sql = string.Format(
            "SELECT " +
            "\"{0}\".\"id\" AS {1}, " +
            "\"{0}\".\"order_id\" AS {2}, " +
            "\"{0}\".\"channel_id\" AS {3}, " +
            "\"{4}\".\"name\" AS {5}, " +
            "\"{0}\".\"created_date\" AS {6}, " +
            "\"{0}\".\"created_by\" AS {7}, " +
            "\"{0}\".\"last_modified_date\" AS {8}, " +
            "\"{0}\".\"last_modified_by\" AS {9}, " +
            "\"{0}\".\"valid_for\" AS {10} " +
            "FROM \"order_channel\" \"{0}\" " +
            "INNER JOIN \"channel\" \"{4}\" ON \"{4}\".\"id\" = \"{0}\".\"channel_id\" " +
            "WHERE \"{0}\".\"order_id\" = @orderId",
            nameof(OrderChannel),
            nameof(OrderChannel.Id),
            nameof(OrderChannel.OrderId),
            nameof(OrderChannel.ChannelId),
            nameof(Channel),
            nameof(Channel) + nameof(Channel.Name),
            nameof(User.CreatedDate),
            nameof(User.CreatedBy),
            nameof(User.LastModifiedDate),
            nameof(User.LastModifiedBy),
            nameof(User.ValidFor)
        );

        var orderChannels = await connection.QueryAsync<OrderChannelViewModel>(sql, parameters);
        return orderChannels;
    }

    public async Task<OrderViewModel> GetUserActiveOrder(Guid userId)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@userId", userId);
        parameters.Add("@orderStatus", OrderStatus.Active.Id);

        var sql = string.Format(
            "SELECT " +
            "\"{0}\".\"id\" AS {1}, " +
            "\"{0}\".\"user_id\" AS {2}, " +
            "\"{0}\".\"day_of_month\" AS {3}, " +
            "\"{0}\".\"amount\" AS {4}, " +
            "\"{0}\".\"order_status_id\" AS {5}, " +
            "\"{11}\".\"name\" AS {12}, " +
            "\"{0}\".\"created_date\" AS {6}, " +
            "\"{0}\".\"created_by\" AS {7}, " +
            "\"{0}\".\"last_modified_date\" AS {8}, " +
            "\"{0}\".\"last_modified_by\" AS {9}, " +
            "\"{0}\".\"valid_for\" AS {10} " +
            "FROM \"order\" \"{0}\" " +
            "INNER JOIN \"order_status\" \"{11}\" ON \"{11}\".\"id\" = \"{0}\".\"order_status_id\" " +
            "WHERE \"{0}\".\"user_id\" = @userId and \"{0}\".\"order_status_id\" = @orderStatus",
            nameof(Order),
            nameof(Order.Id),
            nameof(Order.UserId),
            nameof(Order.DayOfMonth),
            nameof(Order.Amount),
            nameof(Order.OrderStatus) + "Id",
            nameof(User.CreatedDate),
            nameof(User.CreatedBy),
            nameof(User.LastModifiedDate),
            nameof(User.LastModifiedBy),
            nameof(User.ValidFor),
            nameof(OrderStatus),
            nameof(OrderStatus) + nameof(OrderStatus.Name)
        );

        var orders = await connection.QueryFirstOrDefaultAsync<OrderViewModel>(sql, parameters);
        return orders;
    }
}