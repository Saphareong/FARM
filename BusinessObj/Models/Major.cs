using System.ComponentModel.DataAnnotations;
#nullable disable
namespace BusinessObj.Models
{
    public class Major
    {
        [Key]
        [MaxLength(10)]
        [Required(ErrorMessage = "Nhập mã chuyên ngành")]
        [RegularExpression(@"^([A-Z]{2,})*$", ErrorMessage = "Mã chuyên ngành bao gồm ít nhất 2 chữ in hoa.")]
        [Display(Name = "Mã chuyên ngành")]
        public string MajorCode { get; set; }

        [MaxLength(70)]
        [Required(ErrorMessage = "Nhập tên chuyên ngành")]
        [RegularExpression(@"^([a-zA-Z\s]{2,})*$", ErrorMessage = "Tên chuyên ngành chỉ bao gồm chữ cái.")]
        [Display(Name = "Tên chuyên ngành")]
        public string MajorName { get; set; }

        public virtual ICollection<UserMajor> UserMajors { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
