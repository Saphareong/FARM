using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace BusinessObj.Models
{
    public class RoomDetail
    {
        [Key]
        [Required]
        public int RoomDetailID { get; set; }

        [Required]
        public int RoomID { get; set; }

        public int TimeTableID { get; set; }

        [Required]
        public int SlotCurrentDay { get; set; }

        [Required]
        public DateTime DateBusy { get; set; }

        [Required]
        public int SlotTotal { get; set; }

        [Required]
        public string AttendanceAction { get; set; }

        public DateTime TakeAttendanceTime { get; set; }

        [ForeignKey("RoomID")]
        public virtual Room Room { get; set; }

        [ForeignKey("TimeTableID")]
        public virtual TimeTable TimeTable { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

    }
}
