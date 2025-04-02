using DebantErp.DAL.Interfaces;
using DebantErp.DAL.Models;

namespace DebantErp.DAL.Implementations
{
    public class EmployeeDetailsDAL : IEmployeeDetailsDAL
    {
        private readonly DbHelper _dbHelper;

        public EmployeeDetailsDAL(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public Task<List<EmployeeDetailsModel>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeDetailsModel> Get(int id)
        {
            var result = await _dbHelper.QueryAsync<EmployeeDetailsModel>(
                "SELECT * FROM employee_details WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new EmployeeDetailsModel();
        }

        public async Task<int> Create(EmployeeDetailsModel model)
        {
            string sql =
                "INSERT INTO employee_details (tax_code, address, email, phone, birth_date, gender, picture, employee_id) VALUES (@tax_code, @address, @email, @phone, @birth_date, @gender, @picture, @employeeId) RETURNING id";
            return await _dbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> Update(EmployeeDetailsModel model)
        {
            string sql =
                "UPDATE employee_details SET tax_code = @tax_code, address = @address, email = @email, phone = @phone, birth_date = @birth_date, gender = @gender, picture = @picture, updated_at = CAST(@UpdatedAt AS timestamp with time zone) WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, model);
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
