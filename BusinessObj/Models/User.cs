using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace BusinessObj.Models
{
    public class User
    {
        [Key]
        [MaxLength(10)]
        [Required(ErrorMessage = "Nhập mã tài khoản")]
        [RegularExpression(@"^([A-Z0-9])*$", ErrorMessage = "Mã tài khoản chỉ cho phép chữ in hoa và số.")]
        [Display(Name = "Mã tài khoản")]
        public string AccountId { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Nhập tên tài khoản")]
        [RegularExpression(@"^([a-zA-Z0-9\s]{2,})*$", ErrorMessage = "Tên tài khoản bao gồm chữ cái và số, ít nhất 2 kí tự.")]
        [Display(Name = "Tên tài khoản")]
        public string AccountName { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [Display(Name = "Mật khẩu tài khoản")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", 
            ErrorMessage = "Mật khẩu không hợp lệ (Phải có ít nhất: 1 ký tự thường, 1 chữ số, 1 ký tự hoa, 1 ký tự đặc biệt và phải có ít nhất 6 kí tự).")]
        public string Password { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Nhập tên người sở hữu")]
        [RegularExpression(@"^([a-zA-Z\s]{2,})*$", ErrorMessage = "Tên chủ tài khoản bao gồm ít nhất 2 chữ cái.")]
        [Display(Name = "Chủ tài khoản")]
        public string Ower { get; set; }

        [MaxLength(10)]
        public string RoleId { get; set; }
        
        [MaxLength(100)]
        [Required(ErrorMessage = "Nhập Email")]
        [Display(Name = "Email tài khoản")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [MaxLength(12)]
        [Required(ErrorMessage = "Nhập số điện thoại tài khoản")]
        [Display(Name = "Số điện thoại tài khoản")]
        [RegularExpression(@"^([0-9]{10,})$", ErrorMessage = "Số điện thoại giới hạn từ 10 đến 12 số.")]
        public string Phone { get; set; }
        
        [MaxLength(25)]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual ICollection<UserMajor> UserMajors { get; set; }
        public virtual ICollection<Score> Scores { get; set; }  
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<TimeTable> TimeTables { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
