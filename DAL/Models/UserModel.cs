using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebantErp.DAL.Models
{
    public class UserModel
    {
        [Key]
        public int? Id { get; set; }

        [Required, Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Required, Column("last_name")]
        public string LastName { get; set; } = null!;

        [Required, Column("phone")]
        public string Phone { get; set; } = null!;

        public UserRoleEnum Role { get; set; } = UserRoleEnum.User;

        [Required, Column("email")]
        public string Email { get; set; } = null!;

        [Required, Column("password")]
        public string Password { get; set; } = null!;

        [Required, Column("salt")]
        public string Salt { get; set; } = null!;

        [Required, Column("gender")]
        public GenderEnum Gender { get; set; }

        [Required, Column("status")]
        public UserStatusEnum Status { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
