using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebantErp.DAL.Models
{
    public class UserModel
    {
        [Key]
        public int? Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Column("last_name")]
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public UserRoleEnum Role { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public UserStatusEnum Status { get; set; }
    }
}
