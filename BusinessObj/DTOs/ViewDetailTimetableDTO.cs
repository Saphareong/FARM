
namespace BusinessObj.DTOs
{
    public class ViewDetailTimetableDTO
    {
        public int RoomDetailId { get; set; }
        public DateTime ThatSlotDate { get; set; }
        public int slotInThatDate { get; set; }
        public int courseSessionNumber { get; set; }
        public string ClassName { get; set; }
        public string ClassID { get; set; }
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
        public string AttendanceStatus { get; set; }
    }
}
