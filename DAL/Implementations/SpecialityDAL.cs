using DebantErp.DAL.Interfaces;
using DebantErp.DAL.Models;

namespace DebantErp.DAL.Implementations
{
    public class SpecialityDAL : ISpecialityDAL
    {
        private readonly DbHelper _dbHelper;

        public SpecialityDAL(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<SpecialityModel>> Get()
        {
            var result = await _dbHelper.QueryAsync<SpecialityModel>(
                "SELECT * FROM specialities",
                new { }
            );
            return result.ToList();
        }

        public async Task<SpecialityModel> Get(int id)
        {
            var result = await _dbHelper.QueryAsync<SpecialityModel>(
                "SELECT * FROM specialities WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new SpecialityModel();
        }

        public async Task<int> Create(SpecialityModel model)
        {
            string sql = "INSERT INTO specialities (name) VALUES (@name) RETURNING id";
            return await _dbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> Update(SpecialityModel model)
        {
            string sql = "UPDATE specialities SET name = @name WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<int> Delete(int id)
        {
            string sql = "UPDATE specialities SET is_actual = false WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, new { id });
        }
    }
}
