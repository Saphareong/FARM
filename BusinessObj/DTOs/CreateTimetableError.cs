
namespace BusinessObj.DTOs
{
    public class CreateTimetableError
    {
        public int RoomDetailID { get; set; }
        public string ClassName { get; set; }
        public DateTime Datebusy { get; set; }
        public int slotbusy { get; set; }
        public int RoomID { get; set; }
        public string TeacherName { get; set; }
        public string Message { get; set; }
    }
}
