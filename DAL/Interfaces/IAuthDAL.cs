using DebantErp.DAL.Models;

namespace DebantErp.DAL.Interfaces
{
    public interface IAuthDAL : IBaseDAL<UserModel>
    {
        Task<UserModel> Get(string email);
    }
}
