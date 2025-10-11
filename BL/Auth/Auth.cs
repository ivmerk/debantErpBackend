using DebantErp.DAL;
using DebantErp.DAL.Models;
using DebantErp.Dtos;
using DebantErp.Rdos;
using System.ComponentModel.DataAnnotations;

namespace DebantErp.BL.Auth;
public class Auth : IAuth
{
    private readonly IAuthDAL _authDAL;

    private readonly IEncrypt _encrypt;

    public Auth(IAuthDAL authDAL, IEncrypt encrypt)
    {
        _authDAL = authDAL;
        _encrypt = encrypt;
    }

    public async Task<int> CreateUser(UserModel user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
            user.Salt = Guid.NewGuid().ToString();
            user.Status = UserStatusEnum.NeedToApprove;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
        
        user.Password = _encrypt.HashPassword(user.Password ?? "", user.Salt);

        System.Console.WriteLine(user);
        var id = await _authDAL.Create(user);
        return id;
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

    public async Task<int> UpdateUser(int id, UpdateUserDto model)
    {
        var user = await GetUser(id);
        var updatedUser = new UserModel
        {};
          if (!string.IsNullOrEmpty(model.FirstName))
          {
              updatedUser.FirstName = model.FirstName;} else {updatedUser.FirstName = user.FirstName;}

          if (!string.IsNullOrEmpty(model.LastName)){
              updatedUser.LastName = model.LastName;} else {
              updatedUser.LastName = user.LastName;
          }
          if (!string.IsNullOrEmpty(model.Phone)){
              updatedUser.Phone = model.Phone;} else {
              updatedUser.Phone = user.Phone;
          }
          if (model.Role != null && Enum.IsDefined(typeof(UserRoleEnum), model.Role.Value)){
              updatedUser.Role = model.Role.Value;} else {
              updatedUser.Role = user.Role;
          }
          if (!string.IsNullOrEmpty(model.Email)){
              updatedUser.Email = model.Email;} else {
              updatedUser.Email = user.Email;
          }
          if (model.Status != null && Enum.IsDefined(typeof(UserStatusEnum), model.Status.Value)){
              updatedUser.Status = model.Status.Value;} else {
              updatedUser.Status = user.Status;
          }

        return await _authDAL.Update(updatedUser);
    }

    public async Task <ValidationResult?> ValidateEmail(string email)
    {
        var user = await _authDAL.Get(email);
        if (user.Id != null)
        {
            return new ValidationResult("Пользователь с таким email уже существует.");
        }
        return null;
    }
}