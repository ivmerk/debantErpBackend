using DebantErp.Domain.Base;
using DebantErp.Domain.Enums;

namespace DebantErp.Domain.Users;
public class User : Entity, IAggregateRoot
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public UserRole Role { get; private set; }
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string Salt { get; private set; } = null!;
    public UserStatus Status { get; private set; }

    protected User() { }

    public User(string firstName, string lastName, string email, string phone, UserRole role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Role = role;
        Status = UserStatus.NeedToApprove;
    }

    public void SetPassword(string hash, string salt)
    {
        PasswordHash = hash;
        Salt = salt;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = UserStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(string firstName, string lastName, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        UpdatedAt = DateTime.UtcNow;
    }
}


