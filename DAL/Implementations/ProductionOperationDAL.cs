using DebantErp.DAL.Interfaces;
using DebantErp.DAL.Models;

namespace DebantErp.DAL.Implementations
{
    public class ProductionOperationDAL : IProductionOperationDAL
    {
        private readonly DbHelper _dbHelper;

        public ProductionOperationDAL(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<ProductionOperationModel>> Get()
        {
            var result = await _dbHelper.QueryAsync<ProductionOperationModel>(
                "SELECT * FROM production_operations",
                new { }
            );
            return result.ToList();
        }

        public async Task<ProductionOperationModel> Get(int id)
        {
            var result = await _dbHelper.QueryAsync<ProductionOperationModel>(
                "SELECT * FROM production_operations WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new ProductionOperationModel();
        }

        public async Task<int> Create(ProductionOperationModel model)
        {
            string sql = "INSERT INTO production_operations ( name ) VALUES (@name) RETURNING id";
            return await _dbHelper.ExecuteScalarAsync(sql, model);
        }

        public Task<int> Update(ProductionOperationModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
