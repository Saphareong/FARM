
namespace BusinessObj.DTOs
{
    public class ViewAttendanceDTO
    {
        public int CourseNumba { get; set; }
        public DateTime DateStart { get; set; }
        public int slot { get; set; }
        public int roomid { get; set; }
        public string TeacherName { get; set; }
        public string ClassName { get; set; }
        public string AttendanceStatus { get; set; }
    }
}
