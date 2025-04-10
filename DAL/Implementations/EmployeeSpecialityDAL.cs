using DebantErp.DAL.Models;

namespace DebantErp.DAL
{
    public class EmployeeSpecialityDAL : IEmployeeSpecialityDAL
    {
        public Task<List<EmployeeSpecialityModel>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeSpecialityModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(EmployeeSpecialityModel model)
        {
            string sql =
                "INSERT INTO employee_specialities ( employee_id, speciality_id) VALUES (@EmployeeId, @SpecialityId) RETURNING id";
            return await DbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> Update(EmployeeSpecialityModel model)
        {
            string sql =
                "UPDATE employee_specialities SET employee_id = @EmployeeId, speciality_id = @SpecialityId, updated_at = @UpdatedAt CAST(@UpdatedAt AS timestamp with time zone) WHERE id = @id";
            return await DbHelper.ExecuteAsync(sql, model);
        }

        public async Task<int> Delete(int id)
        {
            string sql = "UPDATE employee_specialities SET is_actual = false WHERE id = @id";
            return await DbHelper.ExecuteAsync(sql, new { id });
        }
    }
}
