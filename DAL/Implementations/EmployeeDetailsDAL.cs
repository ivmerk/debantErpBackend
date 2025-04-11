using DebantErp.DAL.Models;

namespace DebantErp.DAL
{
    public class EmployeeDetailsDAL : IEmployeeDetailsDAL
    {
        public Task<List<EmployeeDetailsModel>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeDetailsModel> Get(int id)
        {
            var result = await DbHelper.QueryAsync<EmployeeDetailsModel>(
                "SELECT * FROM employee_details WHERE id = @id",
                new { id }
            );
            return result.FirstOrDefault() ?? new EmployeeDetailsModel();
        }

        public async Task<EmployeeDetailsModel> GetByEmployeeId(int employeeId)
        {
            var result = await DbHelper.QueryAsync<EmployeeDetailsModel>(
                "SELECT * FROM employee_details WHERE employee_id = @employeeId",
                new { employeeId }
            );
            return result.FirstOrDefault() ?? new EmployeeDetailsModel();
        }

        public async Task<int> Create(EmployeeDetailsModel model)
        {
            string sql =
                "INSERT INTO employee_details (tax_code, address, email, phone, birth_date, gender, picture, employee_id) VALUES (@tax_code, @address, @email, @phone, @birth_date, @gender, @picture, @employeeId) RETURNING id";
            return await DbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> Update(EmployeeDetailsModel model)
        {
            string sql =
                "UPDATE employee_details SET tax_code = @tax_code, address = @address, email = @email, phone = @phone, birth_date = @birth_date, gender = @gender, picture = @picture, updated_at = CAST(@UpdatedAt AS timestamp with time zone) WHERE id = @id";
            return await DbHelper.ExecuteAsync(sql, model);
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
