using DebantErp.DAL.Models;

namespace DebantErp.DAL.Interfaces
{
    public interface IAuthDAL
    {
        Task<UserModel> GetUser(string email);
        Task<UserModel> GetUser(int id);
        Task<int> CreateUser(UserModel user);
    }
}
