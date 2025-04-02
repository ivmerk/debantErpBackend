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

        public async Task<List<SpecialityModel>> GetSpecialities()
        {
            var result = await _dbHelper.QueryAsync<SpecialityModel>(
                "SELECT * FROM specialities",
                new { }
            );
            return result.ToList();
        }

        public async Task<SpecialityModel> GetSpeciality(int id)
        {
            var result = await _dbHelper.QueryAsync<SpecialityModel>(
                "SELECT * FROM specialities WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new SpecialityModel();
        }

        public async Task<int> CreateSpeciality(SpecialityModel model)
        {
            string sql = "INSERT INTO specialities (name) VALUES (@name) RETURNING id";
            return await _dbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> DeleteSpeciality(int id)
        {
            string sql = "UPDATE specialities SET is_actual = false WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, new { id });
        }
    }
}
