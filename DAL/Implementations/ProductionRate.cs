using DebantErp.DAL.Interfaces;
using DebantErp.DAL.Models;

namespace DebantErp.DAL.Implementations
{
    public class ProductionRateDAL : IProductionRateDAL
    {
        private readonly DbHelper _dbHelper;

        public ProductionRateDAL(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<ProductionRateModel>> Get()
        {
            var result = await _dbHelper.QueryAsync<ProductionRateModel>(
                "SELECT * FROM production_rates",
                new { }
            );
            return result.ToList();
        }

        public async Task<ProductionRateModel> Get(int id)
        {
            var result = await _dbHelper.QueryAsync<ProductionRateModel>(
                "SELECT * FROM production_rates WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new ProductionRateModel();
        }

        public async Task<List<ProductionRateModel>> GetByProductionOperationId(
            int productionOperationId
        )
        {
            var result = await _dbHelper.QueryAsync<ProductionRateModel>(
                "SELECT * FROM production_rates WHERE production_operation_id = @productionOperationId",
                new { productionOperationId }
            );
            return result.ToList();
        }

        public async Task<int> Create(ProductionRateModel model)
        {
            string sql =
                "INSERT INTO production_rates (production_operation_id, operation_timeframe, rate) VALUES (@productionOperationId, @operationTimeframe, @rate) RETURNING id";
            return await _dbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> Update(ProductionRateModel model)
        {
            string sql =
                "UPDATE production_rates SET production_operation_id = @productionOperationId, operation_timeframe = @operationTimeframe, rate = @rate, updated_at = CAST(@UpdatedAt AS timestamp with time zone) WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<int> Delete(int id)
        {
            string sql = "UPDATE production_rates SET is_actual = false WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, new { id });
        }
    }
}
