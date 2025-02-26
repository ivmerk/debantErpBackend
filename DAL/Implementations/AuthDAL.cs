using DebantErp.DAL.Interfaces;
using DebantErp.DAL.Models;

namespace DebantErp.DAL.Implementations
{
    public class AuthDAL : IAuthDAL
    {
        private readonly DbHelper _dbHelper;

        public AuthDAL(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<UserModel> GetUser(string email)
        {
            var result = await _dbHelper.QueryAsync<UserModel>(
                "SELECT * FROM app_user WHERE email = @email",
                new { email }
            );
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<UserModel> GetUser(int id)
        {
            var result = await _dbHelper.QueryAsync<UserModel>(
                "SELECT * FROM app_user WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<int> CreateUser(UserModel model)
        {
            string sql =
                "INSERT INTO app_user ( first_name, last_name, phone, role, email, password, salt, status) VALUES (@firstName, @lastName, @phone, @role, @email, @password,  @salt, @status)";
            return await _dbHelper.ExecuteScalarAsync(sql, model);
        }
    }
}
