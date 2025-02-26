using System.ComponentModel.DataAnnotations;

namespace DebantErp.DAL.Models
{
    public class UserModel
    {
        [Key]
        public int? Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public UserRoleEnum Role { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public UserStatusEnum Status { get; set; }
    }
}
