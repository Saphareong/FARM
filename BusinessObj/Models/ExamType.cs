using System.ComponentModel.DataAnnotations;
#nullable disable
namespace BusinessObj.Models
{
    public class ExamType
    {
        [Key]
        [Required]
        [MaxLength(10)]
        public string ExamTypeID { get; set; }

        [Required]
        [MaxLength(20)]
        public string ExamTypeName { get; set; }

        public virtual ICollection<FinalExam> ExamTypes { get; set; }
    }
}
