using System.ComponentModel.DataAnnotations;
#nullable disable
namespace BusinessObj.Models
{
    public class Role
    {
        [Key]
        [Required]
        [MaxLength(10)]
        public string RoleId { get; set; }

        [Required]
        [MaxLength(20)]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
