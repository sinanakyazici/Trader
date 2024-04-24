using BuildingBlocks.Data.Dapper;
using Dapper;
using Npgsql;
using Trader.TradeService.Domain.User;

namespace Trader.TradeService.Infrastructure.Data.QueryRepos;

public class UserQueryRepository : DapperRepository, IUserQueryRepository
{
    public UserQueryRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<UserViewModel?> GetUser(Guid userId)
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@userId", userId);

        var sql = string.Format(
            "SELECT " +
            "\"{0}\".\"id\" AS {1}, " +
            "\"{0}\".\"username\" AS {2}, " +
            "\"{0}\".\"name\" AS {3}, " +
            "\"{0}\".\"surname\" AS {4}, " +
            "\"{0}\".\"gsm_phone\" AS {5}, " +
            "\"{0}\".\"email\" AS {6}, " +
            "\"{0}\".\"created_date\" AS {7}, " +
            "\"{0}\".\"created_by\" AS {8}, " +
            "\"{0}\".\"last_modified_date\" AS {9}, " +
            "\"{0}\".\"last_modified_by\" AS {10}, " +
            "\"{0}\".\"valid_for\" AS {11} " +
            "FROM \"user\" \"{0}\" " +
            "WHERE \"{0}\".\"id\" = @userId",
            nameof(User),
            nameof(User.Id),
            nameof(User.Username),
            nameof(User.Name),
            nameof(User.Surname),
            nameof(User.GsmPhone),
            nameof(User.Email),
            nameof(User.CreatedDate),
            nameof(User.CreatedBy),
            nameof(User.LastModifiedDate),
            nameof(User.LastModifiedBy),
            nameof(User.ValidFor)
        );

        var userViewModel = connection.Query<UserViewModel>(sql, parameters).SingleOrDefault();
        return userViewModel;
    }
}