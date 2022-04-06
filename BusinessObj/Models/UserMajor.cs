using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace BusinessObj.Models
{
    public class UserMajor
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Nhập mã tài khoản")]
        [RegularExpression(@"^[A-Z0-9\s]*$", ErrorMessage = "Mã tài khoản chỉ bao gồm chữ in hoa và số.")]
        [Display(Name = "Mã tài khoản")]
        public string AccountId { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "Nhập mã chuyên ngành")]
        [RegularExpression(@"^[A-Z0-9\s]*$", ErrorMessage = "Mã chuyên ngành chỉ bao gồm chữ in hoa và số.")]
        [Display(Name = "Mã chuyên ngành")]
        public string MajorCode { get; set; }

        [ForeignKey("AccountId")]
        public virtual User User { get; set; }

        [ForeignKey("MajorCode")]
        public virtual Major Major { get; set; }


    }
}
