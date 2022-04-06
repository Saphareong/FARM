
namespace BusinessObj.DTOs
{
    public class EditTimeTableDTO
    {
        public string? ClassName { get; set; }
        public string? SubjectName { get; set; }
        public string? TeacherName { get; set; }
        public string? TeacherID { get; set; }
        public DateTime? DateStarted { get; set; }

        public int RoomID { get; set; }
        public string? RoomName { get; set; }
        public int? Oldslotcurrent { get; set; }
        public DateTime OldDateBusy { get; set; }

    }
}
