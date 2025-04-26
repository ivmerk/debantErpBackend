using DebantErp.DAL.Models;

namespace DebantErp.DAL
{
    public class EmployeeTransitionBetweenSpecialityDAL : IEmployeeTransitionBetweenSpecialityDAL
    {
        public Task<List<EmployeeTransitionBetweenSpecialityModel>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeTransitionBetweenSpecialityModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(EmployeeTransitionBetweenSpecialityModel model)
        {
            string sql =
                "INSERT INTO employee_specialities ( employee_id, speciality_id) VALUES (@EmployeeId, @SpecialityId) RETURNING id";
            return await DbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> Update(EmployeeTransitionBetweenSpecialityModel model)
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
