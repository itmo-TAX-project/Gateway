using Application.Models;
using Application.Models.Enums;
using Application.Repositories;
using Npgsql;
using NpgsqlTypes;

namespace Infrastructure.Database.Resositories;

public class UserRepository(NpgsqlDataSource dataSource) : IUserRepository
{
    private readonly NpgsqlDataSource _dataSource = dataSource;

    public async Task<bool> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        const string sql = """
                           insert into users (user_name, user_password, user_role)
                           values (:name, :password, :role)
                           on conflict(user_name) do nothing;
                           """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Text) { Value = user.Name });
        command.Parameters.Add(new NpgsqlParameter("phone", NpgsqlDbType.Text) { Value = user.Password });
        command.Parameters.Add(new NpgsqlParameter("role", "account_role") { Value = user.Role });

        int result = await command.ExecuteNonQueryAsync(cancellationToken);

        // Если пользователь добавлен, возвращает true, в противном случае произошел конфликт первичных ключей
        return result > 0;
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        const string sql = """
                           update users set
                           users_name=:name,
                           users_password=:phone,
                           users_role=:role,
                           """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Text) { Value = user.Name });
        command.Parameters.Add(new NpgsqlParameter("phone", NpgsqlDbType.Text) { Value = user.Password });
        command.Parameters.Add(new NpgsqlParameter("role", "user_role") { Value = user.Role });

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<User?> GetUserAsync(string name, CancellationToken cancellationToken)
    {
        const string sql = """
                           select * from accounts 
                           where (:user = user_name)
                           """;

        await using NpgsqlConnection connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("account_id", NpgsqlDbType.Bigint)
        {
            Value = name,
        });

        NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        await reader.ReadAsync(cancellationToken);
        return new User
        {
            Name = reader.GetString(0),
            Password = reader.GetString(1),
            Role = await reader.GetFieldValueAsync<UserRole>(2, cancellationToken),
        };
    }
}