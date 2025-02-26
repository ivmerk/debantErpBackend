using DebantErp.Dtos;

namespace DebantErp.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(RegisterUserDto model);
        Task<int> Authentificate(string email, string password, bool rememberMe);
        Task ValidateEmail(string email);
    }
}
