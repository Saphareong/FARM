using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace BusinessObj.Models
{
    public class Attendance
    {
        [Key]
        [Required]
        public int AttendanceID { get; set; }

        public string AccountId { get; set; }

        [Required]
        public int SlotTotal { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        public int RoomDetailID { get; set; }

        public int UserClassID { get; set; }

        [ForeignKey("UserClassID")]
        public virtual UserClass UserClass { get; set; }

        [ForeignKey("RoomDetailID")]
        public virtual RoomDetail RoomDetail { get; set; }

        [ForeignKey("AccountId")]
        public virtual User User { get; set; }

    }
}
