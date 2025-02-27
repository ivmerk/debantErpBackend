using DebantErp.DAL.Interfaces;
using DebantErp.DAL.Models;
using DebantErp.Dtos;
using DebantErp.Rdos;

namespace DebantErp.BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL _authDAL;

        private readonly IEncrypt _encrypt;

        public Auth(IAuthDAL authDAL, IEncrypt encrypt)
        {
            _authDAL = authDAL;
            _encrypt = encrypt;
        }

        public async Task<int> CreateUser(RegisterUserDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var model = new UserModel
            {
                FirstName = dto.FirstName ?? "",
                LastName = dto.LastName ?? "",
                Phone = dto.Phone ?? "",
                Role = UserRoleEnum.User,
                Email = dto.Email ?? "",
                Salt = Guid.NewGuid().ToString(),
                Status = UserStatusEnum.NeedToApprove,
            };
            model.Password = _encrypt.HashPassword(dto.Password ?? "", model.Salt);

            System.Console.WriteLine(model);
            return await _authDAL.CreateUser(model);
        }

        public async Task<int> Authentificate(string email, string password, bool rememberMe)
        {
            var user = await _authDAL.GetUser(email);
            if (user.Id != null)
            {
                return user.Id ?? -1;
            }
            throw new AuthorizationException();
        }

        public async Task<UserRdo> GetUser(int id)
        {
            var user = await _authDAL.GetUser(id);

            var userRdo = new UserRdo
            {
                Id = user.Id ?? -1,
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Phone = user.Phone ?? "",
                Role = user.Role,
                Email = user.Email ?? "",
                Status = user.Status,
            };
            return userRdo;
        }

        public async Task ValidateEmail(string email)
        {
            var user = await _authDAL.GetUser(email);
            if (user.Id != null)
            {
                throw new DuplicateEmailException();
            }
        }
    }
}
