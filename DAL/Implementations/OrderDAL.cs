using DebantErp.DAL.Models;

namespace DebantErp.DAL
{
    public class OrderDAL : IOrderDAL
    {
        public async Task<List<OrderModel>> Get()
        {
            var result = await DbHelper.QueryAsync<OrderModel>("SELECT * FROM orders", new { });
            return result.ToList();
        }

        public async Task<OrderModel> Get(int id)
        {
            var result = await DbHelper.QueryAsync<OrderModel>(
                "SELECT * FROM orders WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new OrderModel();
        }

        public async Task<int> Create(OrderModel model)
        {
            string sql = "INSERT INTO orders (name) VALUES (@name) RETURNING id";
            return await DbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> Update(OrderModel model)
        {
            string sql = "UPDATE orders SET name = @name WHERE id = @id";
            return await DbHelper.ExecuteAsync(sql, model);
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
