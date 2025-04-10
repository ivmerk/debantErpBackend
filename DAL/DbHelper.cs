using Dapper;
using Npgsql;

namespace DebantErp.DAL
{
    public static class DbHelper
    {
        public static string ConnectionString =
            "Server=localhost;Port=5432;Database=debanterp;Username=admin;Password=test;";

        private static ILogger? _logger;

        public static void SetLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public static async Task<int> ExecuteAsync(string sql, object model)
        {
            try
            {
                using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
                {
                    await connection.OpenAsync();
                    return await connection.ExecuteAsync(sql, model);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Ошибка при выполнении SQL: {Sql}", sql);
                return -1;
            }
        }

        public static async Task<int> ExecuteScalarAsync(string sql, object model)
        {
            try
            {
                using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
                {
                    await connection.OpenAsync();
                    return await connection.ExecuteAsync(sql, model);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Ошибка при выполнении SQL: {Sql}", sql);
                return -1;
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            try
            {
                using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
                {
                    await connection.OpenAsync();
                    return await connection.QueryAsync<T>(sql, model);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Ошибка при выполнении SQL: {Sql}", sql);
                return new List<T>();
            }
        }
    }
}
