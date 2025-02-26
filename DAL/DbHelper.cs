using System;
using Dapper;
using DebantErp.DAL.Models;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DebantErp.DAL
{
    public class DbHelper
    {
        public static string ConnectionString =
            "Server=localhost;Port=5432;Database=debanterp;Username=admin;Password=test;";
        private readonly ILogger<DbHelper> _logger;

        public DbHelper(ILogger<DbHelper> logger)
        {
            _logger = logger;
        }

        public async Task<int> ExecuteScalarAsync(string sql, object model)
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
                _logger.LogError(ex, "Ошибка при выполнении SQL: {Sql}", sql);
                return -1;
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
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
                _logger.LogError(ex, "Ошибка при выполнении SQL: {Sql}", sql);
                return new List<T>();
            }
        }
    }
}
