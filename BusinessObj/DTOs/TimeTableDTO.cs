using BusinessObj.Models;

namespace BusinessObj.DTOs
{
    public class TimeTableDTO
    {
        /* serving combobox when creating timetable */
        public List<Class>? ClassList { get; set; }

        public List<User>? TeacherList { get; set; }
        
        public List<Room>? RoomList { get; set; }

        /*serving when listing timetable */
        public int? TimetableID { get; set; }

        public string? ClassName { get; set; }
        
        public string? SubjectName { get; set; }

        public string? TeacherID { get; set; }

        public string? TeacherName { get; set; }

        public DateTime? DateStarted { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
