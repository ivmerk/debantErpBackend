using DebantErp.DAL.Interfaces;
using DebantErp.DAL.Models;

namespace DebantErp.DAL.Implementations
{
    public class EmployeeSpecialityDAL : IEmployeeSpecialityDAL
    {
        private readonly DbHelper _dbHelper;

        public EmployeeSpecialityDAL(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<int> CreateEmployeeSpeciality(EmployeeSpecialityModel model)
        {
            string sql =
                "INSERT INTO employee_specialities ( employee_id, speciality_id) VALUES (@EmployeeId, @SpecialityId) RETURNING id";
            return await _dbHelper.ExecuteScalarAsync(sql, model);
        }

        public async Task<int> DeleteEmployeeSpeciality(int id)
        {
            string sql = "UPDATE employee_specialities SET is_actual = false WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, new { id });
        }

        public async Task<int> UpdateEmployeeSpeciality(EmployeeSpecialityModel model)
        {
            string sql =
                "UPDATE employee_specialities SET employee_id = @EmployeeId, speciality_id = @SpecialityId, updated_at = @UpdatedAt CAST(@UpdatedAt AS timestamp with time zone) WHERE id = @id";
            return await _dbHelper.ExecuteAsync(sql, model);
        }
    }
}
