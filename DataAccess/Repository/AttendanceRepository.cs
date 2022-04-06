using BusinessObj.Models;
using BusinessObj.DTOs;

namespace DataAccess.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        public List<Attendance> GetList(string searchText) => AttendanceDAO.Instance.GetList(searchText);

        

        public Attendance GetUserID(int AttId) => AttendanceDAO.Instance.GetUserID(AttId);

        
        public Attendance CheckAtt1(Attendance attendance) => AttendanceDAO.Instance.CheckAtt1(attendance);

        public Attendance CheckAtt2(Attendance attendance) => AttendanceDAO.Instance.CheckAtt2(attendance);
        
        public ViewDetailTimetableDTO getSpecificTimetableForStudent(string studentid, int roomdetailid)
        {
            ViewDetailTimetableDTO dto = new ViewDetailTimetableDTO();
            Attendance? IsItHaveIt = AttendanceDAO.Instance.GetAttendanceForDetailTimetable(studentid, roomdetailid);
            if (IsItHaveIt != null)
            {
                dto.RoomDetailId = IsItHaveIt.RoomDetailID;
                dto.ThatSlotDate = IsItHaveIt.RoomDetail.DateBusy;
                dto.slotInThatDate = IsItHaveIt.RoomDetail.SlotCurrentDay;
                dto.courseSessionNumber = IsItHaveIt.SlotTotal;
                dto.ClassName = IsItHaveIt.RoomDetail.TimeTable.Class.Name
                    + IsItHaveIt.RoomDetail.TimeTable.Class.ClassID;
                dto.ClassID = IsItHaveIt.RoomDetail.TimeTable.Class.ClassID;
                dto.TeacherName = IsItHaveIt.RoomDetail.TimeTable.User.Ower;
                dto.SubjectName = IsItHaveIt.RoomDetail.TimeTable.Class.Subject.SubjectName;
                dto.AttendanceStatus = IsItHaveIt.Status;
                return dto;
            }
            return null;
        }

        public ViewDetailTimetableDTO getSpecificTimetableForTeacher(string teacherid, int roomdetailid)
        {
            ViewDetailTimetableDTO dto = new ViewDetailTimetableDTO();
            RoomDetail? CanItWork = AttendanceDAO.Instance.GetRoomDetailForDetailTimetable(roomdetailid);
            if(CanItWork != null)
            {
                //validate teacher không cho bấm lớp khác điểm danh
                //if (CanItWork.TimeTable.User.AccountId != teacherid) return null;
                dto.RoomDetailId = CanItWork.RoomDetailID;
                dto.ThatSlotDate = CanItWork.DateBusy;
                dto.slotInThatDate = CanItWork.SlotCurrentDay;
                dto.courseSessionNumber = CanItWork.SlotTotal;
                dto.ClassName = CanItWork.TimeTable.Class.Name
                    + CanItWork.TimeTable.Class.ClassID;
                dto.ClassID = CanItWork.TimeTable.Class.ClassID;
                dto.TeacherName = CanItWork.TimeTable.User.Ower;
                dto.SubjectName = CanItWork.TimeTable.Class.Subject.SubjectName;
                dto.AttendanceStatus = CanItWork.AttendanceAction;
                return dto;
            }
            return null;
        }

        public IEnumerable<ViewClassTimeTableDTO> LoadStudentsFromClass(string classid)
        {
            List<ViewClassTimeTableDTO> finallist = new List<ViewClassTimeTableDTO>();
            IEnumerable<UserClass> rawlist = AttendanceDAO.Instance.GetStudentInThatClass(classid);
            if(rawlist.Any())
            {
                foreach(UserClass uc in rawlist)
                {
                    ViewClassTimeTableDTO dto = new ViewClassTimeTableDTO();
                    dto.AccountId = uc.AccountId;
                    dto.AccountOwner = uc.User.Ower;
                    finallist.Add(dto);
                }
            }
            return finallist;
        }

        public IEnumerable<ViewClassTimeTableDTO> LoadStudentsFromClass(string classid, int roomdetailid)
        {
            List<ViewClassTimeTableDTO> finallist = new List<ViewClassTimeTableDTO>();
            IEnumerable<UserClass> rawlist = AttendanceDAO.Instance.GetStudentInThatClass(classid);
            if (rawlist.Any())
            {
                foreach (UserClass uc in rawlist)
                {
                    ViewClassTimeTableDTO dto = new ViewClassTimeTableDTO();
                    dto.AccountId = uc.AccountId;
                    dto.AccountOwner = uc.User.Ower;
                    dto.IsPresent = AttendanceDAO.Instance.GetAttendanceStatus(roomdetailid, uc.AccountId);
                    finallist.Add(dto);
                }
            }
            return finallist;
        }

        public bool CanThisUserTakeAttendance(string accountid, string classid) 
            => AttendanceDAO.Instance.CanThisUserTakeAttendance(accountid, classid);

        public bool IsItTooLateToUpdateAttendance(int roomdetailid)
            => AttendanceDAO.Instance.IsItTooLateToUpdateAttendance(roomdetailid);

        public IEnumerable<ErrorAttendance> TakeSeriousAttendance
            (string classid, int roomdetailid, string[] studentid, string[] attendanceAction)
        {
            List<ErrorAttendance> errorList = new List<ErrorAttendance>();
            for(int i = 0; i < studentid.Length; i++)
            {
                if(!TimetableDAO.Instance.IsThatAStudent(studentid[i]))
                {
                    ErrorAttendance er = new ErrorAttendance();
                    er.AccountId = studentid[i];
                    er.ErrorMessage = "This accountId is not a student";
                    errorList.Add(er);
                }
                if(!AttendanceDAO.Instance.IsThisStudentBelongInThisClass(studentid[i], classid))
                {
                    ErrorAttendance er = new ErrorAttendance();
                    er.AccountId = studentid[i];
                    er.ErrorMessage = "This accountId is not belong to this class";
                    errorList.Add(er);
                }
                //nếu logic đúng thì 2 Trường hợp trên khum xảy ra
                if(!AttendanceDAO.Instance.TakeAttendance
                    (studentid[i], roomdetailid, attendanceAction[i], classid))
                {
                    ErrorAttendance er = new ErrorAttendance();
                    er.AccountId = studentid[i];
                    er.ErrorMessage = "This accountId Failed to take attendance";
                    errorList.Add(er);
                }
            }
            AttendanceDAO.Instance.SwitchLanes(roomdetailid);
            return errorList;
        }

        public IEnumerable<ViewSubjectAttendanceDTO> GetRegisteredClassSubject(string studentid)
        {
            List<ViewSubjectAttendanceDTO> finallist = new List<ViewSubjectAttendanceDTO>();
            foreach (var uc in AttendanceDAO.Instance.GetUserClassesFromStudentid(studentid))
            {
                ViewSubjectAttendanceDTO dto = new ViewSubjectAttendanceDTO();
                dto.classid = uc.ClassID;
                dto.ClassName = uc.Class.Name + uc.ClassID;
                dto.SubjectName = uc.Subject.SubjectID;
                dto.DateStart = AttendanceDAO.Instance.GetDateStartFromClassid(uc.ClassID);
                finallist.Add(dto);
            }

            return finallist;
        }

        public IEnumerable<ViewAttendanceDTO> GetAttendanceClassForStudent(string studentid, string classid)
        {
            List<ViewAttendanceDTO> finallist = new List<ViewAttendanceDTO>();
            foreach (var att in AttendanceDAO.Instance.GetAttendancesFromClassIdAndStudentid(classid, studentid))
            {
                ViewAttendanceDTO dto = new ViewAttendanceDTO();
                dto.CourseNumba = att.SlotTotal;
                dto.DateStart = att.RoomDetail.DateBusy;
                dto.slot = att.RoomDetail.SlotCurrentDay;
                dto.roomid = att.RoomDetail.RoomID;
                dto.TeacherName = att.RoomDetail.TimeTable.User.Ower;
                dto.ClassName = att.RoomDetail.TimeTable.Class.Name
                    + att.RoomDetail.TimeTable.Class.ClassID;
                dto.AttendanceStatus = att.Status;
                finallist.Add(dto);
            }
            return finallist;
        }

        public IEnumerable<ViewAttendanceDTO> GetAttendanceClassForTeacher(string teacherid, string classid)
        {
            List<ViewAttendanceDTO> finallist = new List<ViewAttendanceDTO>();
            foreach (var rd in AttendanceDAO.Instance.GetRoomDetailFromClassIdAndTeacherid(classid, teacherid))
            {
                ViewAttendanceDTO dto = new ViewAttendanceDTO();
                dto.CourseNumba = rd.SlotTotal;
                dto.DateStart = rd.DateBusy;
                dto.slot = rd.SlotCurrentDay;
                dto.roomid = rd.RoomID;
                dto.TeacherName = rd.TimeTable.User.Ower;
                dto.ClassName = rd.TimeTable.Class.Name
                    + rd.TimeTable.Class.ClassID;
                dto.AttendanceStatus = rd.AttendanceAction;
                finallist.Add(dto);
            }
            return finallist;
        }

        public IEnumerable<ViewSubjectAttendanceDTO> GetTeachingClasses(string teacherid)
        {
            List<ViewSubjectAttendanceDTO> finallist = new List<ViewSubjectAttendanceDTO>();
            foreach (var tt in AttendanceDAO.Instance.GetTimetableFromTeacherid(teacherid))
            {
                ViewSubjectAttendanceDTO dto = new ViewSubjectAttendanceDTO();
                dto.classid = tt.ClassID;
                dto.ClassName = tt.Class.Name + tt.ClassID;
                dto.SubjectName = tt.Class.Subject.SubjectID;
                dto.DateStart = tt.DateStart.Day + "/" 
                    + tt.DateStart.Month + "/" + tt.DateStart.Year;
                finallist.Add(dto);
            }
            return finallist;
        }

        public bool IsThatClassHaveTimetable(string classid) 
            => AttendanceDAO.Instance.IsThatClassHaveTimetable(classid);
    }
}
