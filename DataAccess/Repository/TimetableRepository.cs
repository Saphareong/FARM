using BusinessObj.Models;
using BusinessObj.DTOs;

namespace DataAccess.Repository
{
    public class TimetableRepository : ITimetableRepository
    {
        public TimeTableDTO LoadComboData() => TimetableDAO.Instance.LoadComboData();
        public bool IsThatTimetableExists(string classid) => TimetableDAO.Instance.IsThatTimetableExists(classid);
        public IEnumerable<CreateTimetableError> IsThatTimeAvailable(int roomid, 
            int slotStyle, DateTime StartDate, int slotRequire)
        {
            /*
            if (slotRequire <= 0) return false;
            int PositiveOrNegative = (int)StartDate.DayOfWeek;
            if (PositiveOrNegative >= 3 || PositiveOrNegative <= 0) return false;
            bool checkpoint = false;
            if (PositiveOrNegative % 2 == 0) checkpoint = TimetableDAO.Instance.NegativeDayCheck(roomid, slotStyle, StartDate, slotRequire);
            else if(PositiveOrNegative % 2 == 1) checkpoint = TimetableDAO.Instance.PositiveDayCheck(roomid, slotStyle, StartDate, slotRequire);
            return checkpoint;
            */
            List<CreateTimetableError> finalresult = new List<CreateTimetableError>();
            int PositiveOrNegative = (int)StartDate.DayOfWeek;
            if(PositiveOrNegative % 2 == 0)
            {
                foreach (RoomDetail rd in TimetableDAO.Instance.NegativeDayCheck
                    (roomid, slotStyle, StartDate, slotRequire))
                {
                    CreateTimetableError err = new CreateTimetableError();
                    err.RoomDetailID = rd.RoomDetailID;
                    err.ClassName = rd.TimeTable.Class.Name + rd.TimeTable.Class.ClassID;
                    err.TeacherName = rd.TimeTable.User.Ower + " " + rd.TimeTable.AccountId;
                    err.Datebusy = rd.DateBusy;
                    err.slotbusy = rd.SlotCurrentDay;
                    err.RoomID = rd.RoomID;
                    err.Message = "Trùng lặp thời gian";
                    finalresult.Add(err);
                }
            }
            else if( PositiveOrNegative % 2 == 1)
            {
                foreach (RoomDetail rd in TimetableDAO.Instance.PositiveDayCheck
                    (roomid, slotStyle, StartDate, slotRequire))
                {
                    CreateTimetableError err = new CreateTimetableError();
                    err.RoomDetailID = rd.RoomDetailID;
                    err.ClassName = rd.TimeTable.Class.Name + rd.TimeTable.Class.ClassID;
                    err.TeacherName = rd.TimeTable.User.Ower + " " + rd.TimeTable.AccountId;
                    err.Datebusy = rd.DateBusy;
                    err.slotbusy = rd.SlotCurrentDay;
                    err.RoomID = rd.RoomID;
                    err.Message = "Trùng lặp thời gian";
                    finalresult.Add(err);
                }
            }
            return finalresult;
        }

        public IEnumerable<CreateTimetableError> IsThatTimeAvailableAdvanced(int roomid, bool[]? WeeklyRepeat, DateTime StartDate,
            int RepeatFollow, int repeateveryFollow, int slotInDay, int slotrequire)
        {
            /*
            bool checkpoint = false;
            if(RepeatFollow == 1)
                checkpoint = TimetableDAO.Instance.RepeatedDayChecking(roomid, StartDate,
                    repeateveryFollow, slotInDay, slotrequire);
            else if(RepeatFollow == 2) 
                checkpoint = TimetableDAO.Instance.RepeatedWeekChecking(roomid, StartDate,
                    WeeklyRepeat, slotInDay, slotrequire, repeateveryFollow);
            else if(RepeatFollow == 3)
                checkpoint = TimetableDAO.Instance.RepeatedMonthChecking(roomid, StartDate,
                    repeateveryFollow, slotInDay, slotrequire);
            return checkpoint;
            */
            List<CreateTimetableError> finalresult = new List<CreateTimetableError>();
            if(RepeatFollow == 1)
            {
                foreach (RoomDetail rd in TimetableDAO.Instance.RepeatedDayChecking
                (roomid, StartDate, repeateveryFollow, slotInDay, slotrequire))
                {
                    CreateTimetableError err = new CreateTimetableError();
                    err.RoomDetailID = rd.RoomDetailID;
                    err.ClassName = rd.TimeTable.Class.Name + rd.TimeTable.Class.ClassID;
                    err.TeacherName = rd.TimeTable.User.Ower + " " + rd.TimeTable.AccountId;
                    err.Datebusy = rd.DateBusy;
                    err.slotbusy = rd.SlotCurrentDay;
                    err.RoomID = rd.RoomID;
                    err.Message = "Trùng lặp thời gian";
                    finalresult.Add(err);
                }
            }
            else if(RepeatFollow == 2)
            {
                //trường hợp chỉ có 1 ngày duy nhất trong tuần thì pass qua lặp ngày cách nhau 1 tuần :D ehehe
                if(WeeklyRepeat.Where(a => a == true).Count() == 1)
                {
                    int dayofweek = (int)StartDate.DayOfWeek;
                    int startday = 0;
                    int dayUntilItStart = 0;
                    foreach(bool huhu in WeeklyRepeat)
                    {
                        if (huhu) break;
                        startday++;
                    }
                    if(dayofweek != startday)
                    {
                        while (dayofweek != startday)
                        {
                            dayofweek++;
                            if (dayofweek == 7) dayofweek = 0;
                            if (dayofweek == startday) break;
                            dayUntilItStart++;
                        }
                        dayUntilItStart++; //thoát vòng lặp phải + 1 mới đủ huhu
                    }
                    StartDate = StartDate.AddDays(dayUntilItStart);
                    foreach (RoomDetail rd in TimetableDAO.Instance.RepeatedDayChecking
                        (roomid, StartDate, 7 * repeateveryFollow, slotInDay, slotrequire))
                    {
                        CreateTimetableError err = new CreateTimetableError();
                        err.RoomDetailID = rd.RoomDetailID;
                        err.ClassName = rd.TimeTable.Class.Name + rd.TimeTable.Class.ClassID;
                        err.TeacherName = rd.TimeTable.User.Ower + " " + rd.TimeTable.AccountId;
                        err.Datebusy = rd.DateBusy;
                        err.slotbusy = rd.SlotCurrentDay;
                        err.RoomID = rd.RoomID;
                        err.Message = "Trùng lặp thời gian";
                        finalresult.Add(err);
                    }
                }
                //trường hợp chọn full ngày trong tuần vẫn pass qua lặp ngày 
                else
                {
                    foreach (RoomDetail rd in TimetableDAO.Instance.RepeatedWeekChecking
                        (roomid, StartDate, WeeklyRepeat, slotInDay, slotrequire, repeateveryFollow))
                    {
                        CreateTimetableError err = new CreateTimetableError();
                        err.RoomDetailID = rd.RoomDetailID;
                        err.ClassName = rd.TimeTable.Class.Name + rd.TimeTable.Class.ClassID;
                        err.TeacherName = rd.TimeTable.User.Ower + " " + rd.TimeTable.AccountId;
                        err.Datebusy = rd.DateBusy;
                        err.slotbusy = rd.SlotCurrentDay;
                        err.RoomID = rd.RoomID;
                        err.Message = "Trùng lặp thời gian";
                        finalresult.Add(err);
                    }
                }
            }
            else if(RepeatFollow == 3)
            {
                foreach (RoomDetail rd in TimetableDAO.Instance.RepeatedMonthChecking
                (roomid, StartDate, repeateveryFollow, slotInDay, slotrequire))
                {
                    CreateTimetableError err = new CreateTimetableError();
                    err.RoomDetailID = rd.RoomDetailID;
                    err.ClassName = rd.TimeTable.Class.Name + rd.TimeTable.Class.ClassID;
                    err.TeacherName = rd.TimeTable.User.Ower + " " + rd.TimeTable.AccountId;
                    err.Datebusy = rd.DateBusy;
                    err.slotbusy = rd.SlotCurrentDay;
                    err.RoomID = rd.RoomID;
                    err.Message = "Trùng lặp thời gian";
                    finalresult.Add(err);
                }
            }
            return finalresult;
        }

        public bool IsThatTeacherBusy(string teacherid, int slotStyle, DateTime StartDate, int slotRequire)
        {
            if(slotRequire <= 0) return false;
            int PositiveOrNegative = (int)StartDate.DayOfWeek;
            if (PositiveOrNegative >= 3 || PositiveOrNegative <= 0) return false;
            bool checkpoint = false;
            if (PositiveOrNegative % 2 == 0) checkpoint = TimetableDAO.Instance.TeacherNegativeDayCheck(teacherid, slotStyle, StartDate, slotRequire);
            else if (PositiveOrNegative % 2 == 1) checkpoint = TimetableDAO.Instance.TeacherPositiveDayCheck(teacherid, slotStyle, StartDate, slotRequire);
            return checkpoint;
        }

        public bool IsThatTeacherBusyAdvanced(string teacherid, bool[]? WeeklyRepeat, DateTime StartDate,
            int RepeatFollow, int repeateveryFollow, int slotrequire, int slotInday)
        {
            bool checkpoint = false;
            if(RepeatFollow == 1)
                checkpoint = TimetableDAO.Instance.TeacherRepeatedDayChecking(teacherid, StartDate,
                                repeateveryFollow, slotInday, slotrequire);
            else if(RepeatFollow == 2)
                checkpoint = TimetableDAO.Instance.TeacherRepeatedWeekChecking(teacherid, StartDate,
                                WeeklyRepeat, slotInday, slotrequire, repeateveryFollow);
            else if(RepeatFollow == 3)
                checkpoint = TimetableDAO.Instance.TeacherRepeatedMonthChecking(teacherid, StartDate,
                                repeateveryFollow, slotInday, slotrequire);
            return checkpoint;
        }

        public void InitializeTimetable(string teacherid, DateTime StartDate, 
            int slotRequire, int roomid, string classid, int slotstyle)
        {
            if (!TimetableDAO.Instance.IsThatATeacher(teacherid))
                throw new Exception("NOTATEACHER");
            if (slotRequire <= 0) return;
            int PositiveOrNegative = (int)StartDate.DayOfWeek;
            if (PositiveOrNegative >= 3 || PositiveOrNegative <= 0) 
                throw new Exception("vai lz thằng nào dùng hàm của tao thế");
            if (PositiveOrNegative == 1)
            {
                if (slotstyle > 6 || slotstyle < 1) throw new Exception("SLOTPROBLEM");
                TimetableDAO.Instance.CreateTimetablePositiveDay(teacherid, StartDate,
                 slotRequire, roomid, classid, slotstyle);
            }
            else if(PositiveOrNegative == 2)
            {
                if (slotstyle > 10 || slotstyle < 1) throw new Exception("SLOTPROBLEM");
                TimetableDAO.Instance.CreateTimetableNegativeDay(teacherid, StartDate,
                    slotRequire, roomid, classid, slotstyle);
            }
        }

        public void InitializeTimetableAdvanced(string teacherid, int roomid,
            string classid, int slotrequire, DateTime StartDate, bool[]? weeklyRepeat,
            int slotinday, int seperatedRepeat, int RepeatType)
        {
            if (RepeatType == 1)
            {
                TimetableDAO.Instance.CreateRepeatedDay(teacherid, classid,
                    roomid, StartDate, seperatedRepeat, slotinday, slotrequire);
            }
            else if (RepeatType == 2)
            {
                //trường hợp chỉ có 1 ngày duy nhất trong tuần thì pass qua lặp ngày cách nhau 1 tuần :D ehehe
                if (weeklyRepeat.Where(a => a == true).Count() == 1)
                {
                    int dayofweek = (int)StartDate.DayOfWeek;
                    int startday = 0;
                    int dayUntilItStart = 0;
                    foreach (bool huhu in weeklyRepeat)
                    {
                        if (huhu) break;
                        startday++;
                    }
                    if (dayofweek != startday)
                    {
                        while (dayofweek != startday)
                        {
                            dayofweek++;
                            if (dayofweek == 7) dayofweek = 0;
                            if (dayofweek == startday) break;
                            dayUntilItStart++;
                        }
                        dayUntilItStart++; //thoát vòng lặp phải + 1 mới đủ huhu
                    }
                    StartDate = StartDate.AddDays(dayUntilItStart);
                    TimetableDAO.Instance.CreateRepeatedDay(teacherid, classid,
                        roomid, StartDate, 7 * seperatedRepeat, slotinday, slotrequire);
                }
                //trường hợp chọn full ngày trong tuần vẫn pass qua lặp ngày 
                else
                {
                    TimetableDAO.Instance.CreateRepeatedWeek(teacherid, classid, 
                        roomid, StartDate, weeklyRepeat, slotinday, slotrequire, seperatedRepeat);
                }
            }
            else if (RepeatType == 3)
            {
                TimetableDAO.Instance.CreateRepeatedMonth(teacherid, classid, roomid,
                    StartDate, seperatedRepeat, slotinday, slotrequire);
            }
        }

        public int GetSlotRequireFromClass(string classid) => TimetableDAO.Instance.GetSlotRequireFromClass(classid);

        public bool IsThereAnyTimeTable() => TimetableDAO.Instance.IsThereAnyTimeTable();

        //delete this
        public void CreateRepeatedWeek(string teacherid, string classid, int roomid, DateTime StartDate,
            bool[] weeklyRepeated, int slotInday, int slotRequire, int seperatedWeek)
            => TimetableDAO.Instance.CreateRepeatedWeek(teacherid, classid, roomid, StartDate, weeklyRepeated,
                slotInday, slotRequire, seperatedWeek);

        public IEnumerable<TimeTableDTO> LoadTimeTableList()
        {
            var list = new List<TimeTableDTO>();
            var rawtimetable = TimetableDAO.Instance.GetTimeTables();
            foreach(TimeTable tt in rawtimetable)
            {
                TimeTableDTO ez = new TimeTableDTO();
                ez.TimetableID = tt.TimeTableID;
                ez.ClassName = tt.Class.Name + tt.Class.ClassID; //This one look sus
                //ez.SubjectName = TimetableDAO.Instance.GetSubjectNameFromClassID(tt.ClassID);
                ez.SubjectName = tt.Class.Subject.SubjectName;
                ez.TeacherID = tt.AccountId; //this one look sus too
                //ez.TeacherName = TimetableDAO.Instance.GetTeacherNameFromAccountID(tt.AccountId);
                ez.TeacherName = tt.User.Ower;
                ez.DateStarted = tt.DateStart;
                ez.DateCreated = tt.DateCreated;
                list.Add(ez);
            }
            return list;
        }

        public IEnumerable<RoomTimeDTO> LoadSpecificTimetable(int timetableid)
        {
            var list = new List<RoomTimeDTO>();
            var rawroomdetail = TimetableDAO.Instance.GetRoomDetails(timetableid);
            foreach(RoomDetail r in rawroomdetail)
            {
                RoomTimeDTO dto = new RoomTimeDTO();
                //dto.RoomName = TimetableDAO.Instance.GetRoomName(r.RoomID);
                dto.RoomName = r.Room.RoomName;
                dto.Status = StatusConverter(r.DateBusy);
                dto.slotCurrent = r.SlotCurrentDay;
                dto.dateTime = r.DateBusy;
                dto.roomdetailID = r.RoomDetailID;
                dto.coursenumba = r.SlotTotal;
                list.Add(dto);
            }
            return list;
        }

        public bool IsItTooLateToChangeTimetable(int roomdetailid) => TimetableDAO.Instance.IsItTooLateToChangeTimetable(roomdetailid);
        
        public EditTimeTableDTO GetOldTimetable(int roomdetailid)
        {
            RoomDetail rd = TimetableDAO.Instance.GetRoomDetail(roomdetailid);
            //TimeTable tt = TimetableDAO.Instance.GetTimeTable(rd.TimeTableID);
            EditTimeTableDTO dto = new EditTimeTableDTO();
            //dto.ClassName = TimetableDAO.Instance.GetClassNameFromClassID(tt.ClassID);
            //dto.SubjectName = TimetableDAO.Instance.GetSubjectNameFromClassID(tt.ClassID);
            //dto.TeacherName = TimetableDAO.Instance.GetTeacherNameFromAccountID(tt.AccountId);
            dto.ClassName = rd.TimeTable.Class.Name + rd.TimeTable.Class.ClassID;
            dto.SubjectName = rd.TimeTable.Class.Subject.SubjectName;
            dto.TeacherName = rd.TimeTable.User.Ower;
            dto.TeacherID = rd.TimeTable.User.AccountId;
            dto.DateStarted = rd.TimeTable.DateStart;
            dto.RoomID = rd.RoomID;
            //dto.RoomName = TimetableDAO.Instance.GetRoomName(rd.RoomID);
            dto.RoomName = rd.Room.RoomName;
            dto.OldDateBusy = rd.DateBusy.Date;
            dto.Oldslotcurrent = rd.SlotCurrentDay;
            return dto;
        }

        public bool IsThatClassExists(string classid) => TimetableDAO.Instance.IsThatClassExists(classid);

        public Dictionary<int, string> GetRooms() => TimetableDAO.Instance.GetRooms();

        public bool CheckingNewTimeTable(DateTime newdate, int newslot, int roomid) => TimetableDAO.Instance.CheckingNewTimeTable(newdate, newslot, roomid);

        public bool CheckingNewTimeTable(DateTime newdate, int newslot, string teacherid) => TimetableDAO.Instance.CheckingNewTimeTable(newdate, newslot, teacherid);

        public void ModifyTimeTable(int roomdetailid, DateTime newdate, int newslot) => TimetableDAO.Instance.ModifyTimeTable(roomdetailid, newdate, newslot);

        public bool IsTheStudentRegisteredIntoThatClass(string classid, string studentid) => TimetableDAO.Instance.IsTheStudentRegisteredIntoThatClass(classid, studentid);

        public bool IsItTooLateToRegisteringThisClass(string classid) => TimetableDAO.Instance.IsItTooLateToRegisteringThisClass(classid);

        public void AddingStudentIntoTimetable(string classid, string studentid)
        {
            /*
            bool checkpoint = TimetableDAO.Instance.IsThatAStudent(studentid);
            checkpoint = TimetableDAO.Instance.IsThatClassExists(classid);
            if(!checkpoint) throw new Exception("PHILOGICVCL");
            checkpoint = TimetableDAO.Instance.IsThatClassHaveTimetable(classid);
            if (checkpoint)
                TimetableDAO.Instance.AddingStudentIntoTimetable(studentid, classid, userclassid);
            */
            if (TimetableDAO.Instance.IsThatAStudent(studentid))
            {
                if (TimetableDAO.Instance.IsThatClassExists(classid))
                {
                    if (TimetableDAO.Instance.IsThatClassHaveTimetable(classid))
                        TimetableDAO.Instance.AddingStudentIntoTimetable(studentid, classid);
                }
                else throw new Exception("CLASSNOTEXITS");
            }
            else throw new Exception("NOTASTUDENT");
        }

        public void AddingStudentIntoTimetableAfterCreation(string classid)
        {
            if(TimetableDAO.Instance.IsThereAnyStudentRegisteredWithoutTimetable(classid))
                TimetableDAO.Instance.AddingStudentIntoTimetableAfterCreateTimetable(classid);
        }

        public void UpdateStudentIntoNewTimetable(string classid, string studentid, int userclassid)
        {

        }

        public IEnumerable<ViewTimeTableDTO> ViewTimetableAsAStudent(string studentid, DateTime inputtedDate)
        {
            IEnumerable<int> busylist = TimetableDAO.Instance.GetRoomDetailIDsWithSpecificWeek(inputtedDate);
            if (busylist.Any()) //có lớp học vào ngày đó
            {
                IEnumerable<Attendance> thereyouare = TimetableDAO.Instance.GetAttendancesFromStudentID(studentid, busylist);
                if (thereyouare.Any()) //thằng này có tkb của giờ busylist
                {
                    List<ViewTimeTableDTO> finallist = new List<ViewTimeTableDTO>();
                    foreach (Attendance attendance in thereyouare)
                    {
                        /*
                        TimeTable? tt = TimetableDAO.Instance.GetTimetableFromRoomDetailID(attendance.RoomDetailID);
                        ViewTimeTableDTO dto = new ViewTimeTableDTO();
                        dto.SubjectName = TimetableDAO.Instance.GetSubjectIdFromClassID(tt.ClassID);
                        dto.ClassName = TimetableDAO.Instance.GetClassNameFromClassID(tt.ClassID) + tt.ClassID;
                        DateTime busydate = TimetableDAO.Instance.GetDateBusyFromRoomDetailID(attendance.RoomDetailID);
                        dto.DateHappend = busydate;
                        dto.DayName = (int)busydate.DayOfWeek;
                        dto.slotCurrentDay = TimetableDAO.Instance.GetSlotCurrentFromRoomDetailID(attendance.RoomDetailID);
                        dto.RoomName = TimetableDAO.Instance.GetRoomName(attendance.RoomDetailID);
                        dto.status = attendance.Status;
                        finallist.Add(dto);
                        */
                        ViewTimeTableDTO dto = new ViewTimeTableDTO();
                        dto.RoomDetailId = attendance.RoomDetailID;
                        dto.SubjectName = attendance.RoomDetail.TimeTable.Class.Subject.SubjectID;
                        dto.ClassName = attendance.RoomDetail.TimeTable.Class.Name
                            + attendance.RoomDetail.TimeTable.Class.ClassID;
                        DateTime busydate = attendance.RoomDetail.DateBusy;
                        dto.DateHappend = busydate;
                        dto.DayName = (int)busydate.DayOfWeek;
                        dto.slotCurrentDay = attendance.RoomDetail.SlotCurrentDay;
                        dto.RoomName = attendance.RoomDetail.Room.RoomName;
                        dto.status = attendance.Status;
                        finallist.Add(dto);
                    }
                    return finallist;
                }
                else throw new Exception("FREETIMEBABY");
            }
            else throw new Exception("FREETIMEBABY");
        }

        public IEnumerable<ViewTimeTableDTO> ViewTimetableAsATeacher(string teacherid, DateTime inputtedDate)
        {
            IEnumerable<RoomDetail> busylist = TimetableDAO.Instance.GetRoomdetailsWithSpecificWeek(inputtedDate);
            if (busylist.Any()) //có lớp học vào ngày đó
            {
                //throw new Exception("go pro");
                IEnumerable<int> thereyouare = TimetableDAO.Instance.GetTimetableIDsFromTeacherID(teacherid, busylist);
                if (thereyouare.Any()) //thằng này có tkb của giờ busylist
                {
                    //throw new Exception("go pro x2");
                    /******* BELOW CODE DOESN'T AFFECT DATABASE PERFORMANCE *****/
                    List<RoomDetail> busylistOfAteacher = busylist.ToList();
                    foreach(RoomDetail rd in busylist.ToList())
                    {
                        if (thereyouare.Any(timetableid => timetableid == rd.TimeTableID)) continue;
                        else busylistOfAteacher.Remove(rd);
                    }

                    //if (busylistOfAteacher.Any()) throw new Exception("holy fuck");
                    //else throw new Exception("oke lessgo");

                    /******* ABOVE CODE DOESN'T AFFECT DATABASE PERFORMANCE *****/
                    //throw new Exception("go pro x3");
                    //sau khi đã loại dần busylist sẽ còn của teacher
                    List<ViewTimeTableDTO> finallist = new List<ViewTimeTableDTO>();
                    // now it does affect database
                    foreach(RoomDetail ez in busylistOfAteacher)
                    {
                        /*
                        string classid = TimetableDAO.Instance.GetClassIDFromTimetableid(nam.TimeTableID);
                        ViewTimeTableDTO dto = new ViewTimeTableDTO();
                        dto.SubjectName = TimetableDAO.Instance.GetSubjectIdFromClassID(classid);
                        dto.ClassName = TimetableDAO.Instance.GetClassNameFromClassID(classid) + classid;
                        DateTime busydate = TimetableDAO.Instance.GetDateBusyFromRoomDetailID(nam.RoomDetailID);
                        dto.DateHappend = busydate;
                        dto.DayName = (int)busydate.DayOfWeek;
                        dto.slotCurrentDay = TimetableDAO.Instance.GetSlotCurrentFromRoomDetailID(nam.RoomDetailID);
                        dto.RoomName = TimetableDAO.Instance.GetRoomName(nam.RoomDetailID);
                        dto.status = nam.AttendanceAction;
                        finallist.Add(dto);
                        */
                        ViewTimeTableDTO dto = new ViewTimeTableDTO();
                        dto.RoomDetailId = ez.RoomDetailID;
                        dto.SubjectName = ez.TimeTable.Class.Subject.SubjectID;
                        dto.ClassName = ez.TimeTable.Class.Name + ez.TimeTable.Class.ClassID;
                        DateTime busydate = ez.DateBusy;
                        dto.DateHappend = busydate;
                        dto.DayName = (int)busydate.DayOfWeek;
                        dto.slotCurrentDay = ez.SlotCurrentDay;
                        dto.RoomName = ez.Room.RoomName;
                        dto.status = ez.AttendanceAction;
                        finallist.Add(dto);
                    }
                    //throw new Exception("go pro x4");
                    return finallist;
                }
                else throw new Exception("FREETIMEBABY");
            }
            else throw new Exception("FREETIMEBABY");
        }

        /* START PRIVATE FUNCTION */
        private string StatusConverter(DateTime datebusy)
        {
            DateTime rightfuckingnow = DateTime.Now.Date;
            int vodka = DateTime.Compare(rightfuckingnow, datebusy.Date);
            if (vodka < 0) return "Future";
            if (vodka == 0) return "Happening";
            if (vodka > 0) return "Past";
            return "If you see this line that mean you are dead not me.";
        }
        /* END PRIVATE FUNCTIon */
    }
}
