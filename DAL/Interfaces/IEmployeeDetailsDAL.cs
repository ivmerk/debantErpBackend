using DebantErp.DAL.Models;

namespace DebantErp.DAL.Interfaces
{
    public interface IEmployeeDetailsDAL
    {
        Task<EmployeeDetailsModel> GetEmployeeDetails(int id);
        Task<int> CreateEmployeeDetails(EmployeeDetailsModel model);
        Task<int> UpdateEmployeeDetails(EmployeeDetailsModel model);
    }
}
