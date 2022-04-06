using System.ComponentModel.DataAnnotations;
#nullable disable
namespace BusinessObj.Models
{
    public class ApplicationType
    {
        [Key]
        [Required]
        [MaxLength(10)]
        public string ApplicationTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ApplicationTypeName { get; set; }

        public virtual ICollection<Application> ApplicationTypes { get; set; }

    }
}
