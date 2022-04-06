using BusinessObj.Models;
using BusinessObj.DTOs;

namespace DataAccess.Repository
{
    public interface IAttendanceRepository
    {
        List<Attendance> GetList(string searchText);
        
        Attendance GetUserID(int AttId);
        

        Attendance CheckAtt1(Attendance attendance);

        Attendance CheckAtt2(Attendance attendance);

        ViewDetailTimetableDTO getSpecificTimetableForStudent(string studentid, int roomdetailid);

        ViewDetailTimetableDTO getSpecificTimetableForTeacher(string teacherid, int roomdetailid);

        IEnumerable<ViewClassTimeTableDTO> LoadStudentsFromClass(string classid);

        IEnumerable<ViewClassTimeTableDTO> LoadStudentsFromClass(string classid, int roomdetailid);

        bool CanThisUserTakeAttendance(string accountid, string classid);

        IEnumerable<ErrorAttendance> TakeSeriousAttendance
            (string classid, int roomdetailid, string[] studentid, string[] attendanceAction);

        IEnumerable<ViewSubjectAttendanceDTO> GetRegisteredClassSubject(string studentid);

        bool IsThatClassHaveTimetable(string classid);

        bool IsItTooLateToUpdateAttendance(int roomdetailid);
        
        IEnumerable<ViewSubjectAttendanceDTO> GetTeachingClasses(string teacherid);

        IEnumerable<ViewAttendanceDTO> GetAttendanceClassForStudent(string studentid, string classid);

        IEnumerable<ViewAttendanceDTO> GetAttendanceClassForTeacher(string teacherid, string classid);
    }
}
