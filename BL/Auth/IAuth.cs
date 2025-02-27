using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(RegisterUserDto model);
        Task<int> Authentificate(string email, string password, bool rememberMe);
        Task<UserRdo> GetUser(int id);
        Task ValidateEmail(string email);
    }
}
