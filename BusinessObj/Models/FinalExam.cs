using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace BusinessObj.Models
{
    public class FinalExam
    {
        [Key]
        [Required]
        [Display(Name = "Mã Exam")]
        public int ExamID { get; set; }

        [Required(ErrorMessage = "Nhập ngày thi")]
        [Display(Name = "Ngày thi")]
        public DateTime ExamDate { get; set; }

        [Range(1, 180, ErrorMessage = "Thời gian giới hạn từ 1 đến 180 phút")]
        [Required(ErrorMessage = "Nhập thời gian thi")]
        [Display(Name = "Thời gian thi(minutes)")]
        public int ExamTime { get; set; }

        [MaxLength(4, ErrorMessage = "Mã phòng chỉ gồm 3 chữ số")]
        [Required(ErrorMessage = "Nhập phòng thi")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phòng thi chỉ bao gồm số.")]
        [Display(Name = "Phòng thi")]
        public string RoomID { get; set; }

        [MaxLength(10)]
        [Display(Name = "Mã loại exam")]
        public string ExamTypeID { get; set; }

        [MaxLength(10)]
        [Display(Name = "Mã môn")]
        public string SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("ExamTypeID")]
        public virtual ExamType ExamType { get; set; }
    }
}
