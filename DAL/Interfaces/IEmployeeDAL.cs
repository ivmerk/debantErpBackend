using DebantErp.DAL.Models;

namespace DebantErp.DAL.Interfaces
{
    public interface IEmployeeDAL
    {
        Task<List<EmployeeModel>> GetEmployees();
        Task<EmployeeModel> GetEmployee(int id);
        Task<int> CreateEmployee(EmployeeModel model);
        Task<int> UpdateEmployee(EmployeeModel model);
        Task<int> DeleteEmployee(int id);
    }
}
