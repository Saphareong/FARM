using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace BusinessObj.Models
{
    public class Subject
    {
        [Key]
        [MaxLength(10)]
        [Required(ErrorMessage = "Nhập mã môn")]
        [RegularExpression(@"^([A-Z]{3,}[0-9]{3,})*$", ErrorMessage = "Mã môn phải bao gồm ít nhất 3 chữ in hoa và 3 số.")]
        [Display(Name = "Mã môn")]
        public string SubjectID { get; set; }

        [MaxLength(70)]
        [Required(ErrorMessage = "Nhập tên môn học")]
        [RegularExpression(@"^([a-zA-Z\s]{2,})*$", ErrorMessage = "Tên môn học chỉ bao gồm chữ cái.")]
        [Display(Name = "Tên môn học")]
        public string SubjectName { get; set; }

        [Range(1, 35, ErrorMessage = "Số slot giới hạn từ 1 đến 35")]
        [Required(ErrorMessage = "Nhập số slot")]
        [Display(Name = "Số slot")]
        public int SlotRequire { get; set; }

        [MaxLength(10)]
        [Display(Name = "Mã chuyên ngành")]
        public string MajorCode { get; set; }

        [ForeignKey("MajorCode")]
        public virtual Major Major { get; set; }

        public virtual ICollection<FinalExam> FinalExams { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
        public virtual ICollection<Class> Classes { get; set; }

    }
}
