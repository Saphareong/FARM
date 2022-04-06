

namespace BusinessObj.DTOs
{
    public class ViewTimeTableDTO
    {
        public int RoomDetailId { get; set; }
        public string SubjectName { get; set; }
        public string ClassName { get; set; }
        public DateTime? DateHappend { get; set; } //just saved for rainy day
        public int DayName { get; set; }
        //0 = sunday, 1 = monday, 2 = tuesday,... 6 = saturday
        public int slotCurrentDay { get; set; }
        public string RoomName { get; set; }
        public string status { get; set; }
    }
}
