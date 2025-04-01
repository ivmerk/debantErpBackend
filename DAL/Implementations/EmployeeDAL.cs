using DebantErp.DAL.Interfaces;
using DebantErp.DAL.Models;

namespace DebantErp.DAL.Implementations
{
    public class EmployeeDAL : IEmployeeDAL
    {
        private readonly DbHelper _dbHelper;

        public EmployeeDAL(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            var result = await _dbHelper.QueryAsync<EmployeeModel>(
                "SELECT * FROM employees",
                new { }
            );
            return result.ToList();
        }

        public async Task<EmployeeModel> GetEmployee(int id)
        {
            var result = await _dbHelper.QueryAsync<EmployeeModel>(
                "SELECT * FROM employees WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new EmployeeModel();
        }

        public async Task<int> CreateEmployee(EmployeeModel model)
        {
            string sql =
                "INSERT INTO employees ( first_name, middle_name, last_name) VALUES (@firstName,@middleName, @lastName) RETURNING id";
            return await _dbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> UpdateEmployee(EmployeeModel model)
        {
            string sql =
                "UPDATE employees SET first_name = @firstName, middle_name = @middleName, last_name = @lastName, updated_at = CAST(@UpdatedAt AS timestamp with time zone) WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<int> DeleteEmployee(int id)
        {
            string sql = "UPDATE employees SET is_actual = false WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, new { id });
        }
    }
}
