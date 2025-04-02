using DebantErp.DAL.Models;

namespace DebantErp.DAL.Interfaces
{
    public interface IEmployeeSpecialityDAL
    {
        Task<int> CreateEmployeeSpeciality(EmployeeSpecialityModel model);
        Task<int> UpdateEmployeeSpeciality(EmployeeSpecialityModel model);
        Task<int> DeleteEmployeeSpeciality(int id);
    }
}
