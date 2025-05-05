using DebantErp.DAL;
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
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            model.Password = _encrypt.HashPassword(dto.Password ?? "", model.Salt);

            System.Console.WriteLine(model);
            return await _authDAL.Create(model);
        }

        public async Task<int> Authentificate(string email, string password, bool rememberMe)
        {
            var user = await _authDAL.Get(email);
            if (user.Id != null)
            {
                return user.Id ?? -1;
            }
            throw new AuthorizationException();
        }

        public async Task<UserRdo> GetUser(int id)
        {
            var user = await _authDAL.Get(id);
            var userRdo = new UserRdo
            {
                Id = user.Id ?? -1,
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Phone = user.Phone ?? "",
                Role = user.Role,
                Email = user.Email ?? "",
                Status = user.Status,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };
            return userRdo;
        }

        public async Task<int> UpdateUser(int id, UpdateUserDto dto)
        {
            var user = await GetUser(id);
            var updatedUser = new UserModel
            {};
              if (!string.IsNullOrEmpty(dto.FirstName))
              {
                  updatedUser.FirstName = dto.FirstName;} else {updatedUser.FirstName = user.FirstName;}

              if (!string.IsNullOrEmpty(dto.LastName)){
                  updatedUser.LastName = dto.LastName;} else {
                  updatedUser.LastName = user.LastName;
              }
              if (!string.IsNullOrEmpty(dto.Phone)){
                  updatedUser.Phone = dto.Phone;} else {
                  updatedUser.Phone = user.Phone;
              }
              if (dto.Role != null && Enum.IsDefined(typeof(UserRoleEnum), dto.Role.Value)){
                  updatedUser.Role = dto.Role.Value;} else {
                  updatedUser.Role = user.Role;
              }
              if (!string.IsNullOrEmpty(dto.Email)){
                  updatedUser.Email = dto.Email;} else {
                  updatedUser.Email = user.Email;
              }
              if (dto.Status != null && Enum.IsDefined(typeof(UserStatusEnum), dto.Status.Value)){
                  updatedUser.Status = dto.Status.Value;} else {
                  updatedUser.Status = user.Status;
              }

            return await _authDAL.Update(updatedUser);
        }

        public async Task ValidateEmail(string email)
        {
            var user = await _authDAL.Get(email);
            if (user.Id != null)
            {
                throw new DuplicateEmailException();
            }
        }
    }
}
