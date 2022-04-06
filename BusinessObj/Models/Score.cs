using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace BusinessObj.Models
{
    public class Score
    {
        [Key]
        [Required]
        [Display(Name = "Mã điểm")]
        public int ScoreID { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Nhập sai kiểu dữ liệu")]
        [Required(ErrorMessage = "Nhập điểm quiz 1")]
        [Display(Name = "Điểm quiz 1")]
        public double Quiz1 { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Nhập sai kiểu dữ liệu")]
        [Required(ErrorMessage = "Nhập điểm quiz 2")]
        [Display(Name = "Điểm quiz 2")]
        public double Quiz2 { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Nhập sai kiểu dữ liệu")]
        [Required(ErrorMessage = "Nhập điểm lab 1")]
        [Display(Name = "Điểm lab 1")]
        public double Lab1 { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Nhập sai kiểu dữ liệu")]
        [Required(ErrorMessage = "Nhập điểm lab 2")]
        [Display(Name = "Điểm lab 2")]
        public double Lab2 { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Nhập sai kiểu dữ liệu")]
        [Required(ErrorMessage = "Nhập điểm lab 3")]
        [Display(Name = "Điểm lab 3")]
        public double Lab3 { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Nhập sai kiểu dữ liệu")]
        [Required(ErrorMessage = "Nhập điểm Assignment")]
        [Display(Name = "Điểm assignment")]
        public double Assignment { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Nhập sai kiểu dữ liệu")]
        [Required(ErrorMessage = "Nhập điểm practical exam")]
        [Display(Name = "Điểm practical exam")]
        public double PE { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Nhập sai kiểu dữ liệu")]
        [Required(ErrorMessage = "Nhập điểm final exam")]
        [Display(Name = "Điểm final exam")]
        public double FE { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Nhập sai kiểu dữ liệu")]
        [Required(ErrorMessage = "Nhập tổng điểm")]
        [Display(Name = "Tổng điểm")]
        public double Total { get; set; }

        [MaxLength(25)]
        [Display(Name = "Trạng thái(pass or not)")]
        public string Status { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Nhập mã môn")]
        [RegularExpression(@"^[A-Z0-9\s]*$", ErrorMessage = "Tên môn học chỉ bao gồm chữ in hoa và số.")]
        [Display(Name = "Mã môn")]
        public string SubjectID { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Nhập mã lớp")]
        [RegularExpression(@"^[A-Z0-9\s]*$", ErrorMessage = "Tên lớp học chỉ bao gồm chữ in hoa và số.")]
        [Display(Name = "Mã lớp")]
        public string ClassID { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Nhập mã tài khoản")]
        [RegularExpression(@"^[A-Z0-9\s]*$", ErrorMessage = "Mã tài khoản chỉ bao gồm chữ in hoa và số.")]
        [Display(Name = "Mã tài khoản")]
        public string AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual User User { get; set; }

        [ForeignKey("SubjectID")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("ClassID")]
        public virtual Class Class { get; set; }
    }
}
