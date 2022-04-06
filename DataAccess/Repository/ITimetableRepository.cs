using BusinessObj.DTOs;
using BusinessObj.Models;

namespace DataAccess.Repository
{
    public interface ITimetableRepository
    {
        TimeTableDTO LoadComboData();
        bool IsThatTimetableExists(string classid);
        IEnumerable<CreateTimetableError> IsThatTimeAvailable(int roomid, int slotStyle, DateTime StartDate, int slotRequire);
        IEnumerable<CreateTimetableError> IsThatTimeAvailableAdvanced(int roomid, bool[]? WeeklyRepeat, DateTime StartDate,
            int RepeatFollow, int repeateveryFollow, int slotInDay, int slotrequire);
        bool IsThatTeacherBusy(string teacherid, int slotStyle, DateTime StartDate, int slotRequire);
        bool IsThatTeacherBusyAdvanced(string teacherid, bool[]? WeeklyRepeat, DateTime StartDate,
            int RepeatFollow, int repeateveryFollow, int slotrequire, int slotInday);
        void InitializeTimetable(string teacherid, DateTime StartDate, 
            int slotRequire, int roomid, string classid, int slotstyle);
        void InitializeTimetableAdvanced(string teacherid, int roomid,
            string classid, int slotrequire, DateTime StartDate, bool[]? weeklyRepeat,
            int slotinday, int seperatedRepeat, int RepeatType);
        int GetSlotRequireFromClass(string classid);
        bool IsThereAnyTimeTable();
        IEnumerable<TimeTableDTO> LoadTimeTableList();
        IEnumerable<RoomTimeDTO> LoadSpecificTimetable(int timetableid);
        bool IsItTooLateToChangeTimetable(int roomdetailid);
        EditTimeTableDTO GetOldTimetable(int roomdetailid);
        Dictionary<int, string> GetRooms();
        bool CheckingNewTimeTable(DateTime newdate, int newslot, int roomid);
        bool CheckingNewTimeTable(DateTime newdate, int newslot, string teacherid);
        void ModifyTimeTable(int roomdetailid, DateTime newdate, int newslot);
        bool IsThatClassExists(string classid);
        void AddingStudentIntoTimetable(string classid, string studentid);
        void AddingStudentIntoTimetableAfterCreation(string classid);
        //void UpdateStudentIntoNewTimetable(string classid, string studentid);
        IEnumerable<ViewTimeTableDTO> ViewTimetableAsAStudent(string studentid, DateTime date);
        IEnumerable<ViewTimeTableDTO> ViewTimetableAsATeacher(string teacherid, DateTime date);
        bool IsTheStudentRegisteredIntoThatClass(string classid, string studentid);
        bool IsItTooLateToRegisteringThisClass(string classid);

        //remember to delete below line
        void CreateRepeatedWeek(string teacherid, string classid, int roomid, DateTime StartDate,
            bool[] weeklyRepeated, int slotInday, int slotRequire, int seperatedWeek);
    }
}
