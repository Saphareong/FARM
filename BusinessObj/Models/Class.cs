using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace BusinessObj.Models
{
    public class Class
    {
        [Key]
        [MaxLength(10)]
        [Display(Name = "Mã lớp")]
        public string ClassID { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Nhập tên lớp")]
        //[RegularExpression(@"^[A-Z0-9\s]*$", ErrorMessage = "Tên lớp chỉ bao gồm chữ hoa và số.")]
        [Display(Name = "Tên lớp")]
        public string Name { get; set; }

        [MaxLength(25)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [MaxLength(10)]
        [Display(Name = "Mã môn")]
        public string SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public virtual Subject Subject { get; set; }
    }
}
