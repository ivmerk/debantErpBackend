using Dapper;
using DebantErp.DAL.Models;
using Npgsql;

public class MockProductionOperation
{
    private readonly string _connectionString;

    public MockProductionOperation(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InsertAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        var operationCount = await connection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM production_operations"
        );
        if (operationCount > 0)
            return;

        var operations = new List<ProductionOperationModel>
        {
            new ProductionOperationModel
            {
                Name = "Производство",
                IsActual = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
        };
    }
}
