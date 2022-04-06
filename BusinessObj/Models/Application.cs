using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace BusinessObj.Models
{
    public class Application
    {
        [Key]
        [Required]
        [Display(Name = "Mã đơn")]
        public int ApplicationID { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "Nhập nội dung đơn")]
        [RegularExpression(@"^.{10,}$", ErrorMessage = "Nội dung đơn quá ngắn.")]
        [Display(Name = "Nội dung đơn")]
        public string ApplicationContent { get; set; }

        [MaxLength(25)]
        [Display(Name = "Trạng thái")]
        public string ApplicationStatus { get; set; }

        [MaxLength(10)]
        [Display(Name = "Loại đơn")]
        public string ApplicationTypeID { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreateDay { get; set; }

        [MaxLength(10)]
        public string AccountId { get; set; }

        [ForeignKey("ApplicationTypeID")]
        public virtual ApplicationType ApplicationType { get; set; }

        [ForeignKey("AccountId")]
        public virtual User User { get; set; }

    }
}
