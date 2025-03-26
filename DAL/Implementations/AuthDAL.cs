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
                "SELECT * FROM users WHERE email = @email",
                new { email }
            );
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<UserModel> GetUser(int id)
        {
            var result = await _dbHelper.QueryAsync<UserModel>(
                "SELECT id, first_name AS FirstName, last_name AS LastName, phone, role, email, status FROM users WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<int> CreateUser(UserModel model)
        {
            string sql =
                "INSERT INTO users ( first_name, last_name, phone, role, email, password, salt, status) VALUES (@firstName, @lastName, @phone, @role, @email, @password,  @salt, @status)";
            return await _dbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> UpdateUser(UserModel model)
        {
            string sql =
                "UPDATE users SET first_name = @firstName, last_name = @LastName, phone = @phone, role = @role, email = @email, status = @status, updated_at = CAST(@UpdatedAt AS timestamp with time zone) WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, model);
        }
    }
}
