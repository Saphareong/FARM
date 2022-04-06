using BusinessObj.DTOs;
using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{//I'm using this class to process everything related to timetable
    public class TimetableDAO
    {
        private static TimetableDAO instance = null;
        private static readonly object instanceLock = new object();
        public TimetableDAO() { }
        public static TimetableDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TimetableDAO();
                    }
                    return instance;
                }
            }
        }

        public TimeTableDTO LoadComboData()
        {
            TimeTableDTO dto = new TimeTableDTO();
            try
            {
                using var context = new FAMContext();
                var firstquery = from u in context.Users
                                 where u.RoleId == "TE"
                                 select u;
                dto.TeacherList = firstquery.ToList();
                //xổ ra những lớp chưa có tkb
                //dto.ClassList = context.Classes.ToList();
                /* cannot use linq for big shit
                var secondquery = from c in context.Classes
                                  join t in context.TimeTables on c.ClassID equals t.ClassID
                                  where t.TimeTableID is not null
                                  select c;
                dto.ClassList = secondquery.ToList();
                */
                //Lowest scum way
                var secondquery = from t in context.TimeTables
                                  select t.ClassID;
                var thirdquery = from c in context.Classes
                                 select c;
                List<Class> darkway = thirdquery.ToList();
                List<string> heheboi = secondquery.ToList();
                foreach (string classid in heheboi)
                {
                    //quan hệ 1 class 1 timetable
                    Class tracktor = darkway.Single(a => a.ClassID == classid);
                    darkway.Remove(tracktor);
                }
                dto.ClassList = darkway;
                //end complex processing
                dto.RoomList = context.Rooms.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dto;
        }

        public bool IsThereAnyTimeTable()
        {
            try
            {
                using var context = new FAMContext();
                return context.TimeTables.Any();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsThatATeacher(string accountid)
        {
            try
            {
                using var context = new FAMContext();
                string? roleid = context.Users.SingleOrDefault(a => a.AccountId == accountid).RoleId;
                if (roleid == null) return false;
                if (roleid == "TE") return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsThatAStudent(string accountid)
        {
            try
            {
                using var context = new FAMContext();
                string? roleid = context.Users.SingleOrDefault(a => a.AccountId == accountid).RoleId;
                if (roleid == null) return false;
                if (roleid == "ST") return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsThatClassExists(string classid)
        {
            try
            {
                using var context = new FAMContext();
                Class? EdoTensei = context.Classes.SingleOrDefault(a => a.ClassID == classid);
                if (EdoTensei == null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsItTooLateToChangeTimetable(int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                DateTime datehappend = context.RoomDetails.Single(a => a.RoomDetailID == roomdetailid).DateBusy;
                int balancer = DateTime.Compare(DateTime.Now.Date, datehappend.Date);
                if (balancer < 0) return false;
                else return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsTheStudentRegisteredIntoThatClass(string classid, string studentid)
        {
            try
            {
                using var context = new FAMContext();
                UserClass? uc = context.UserClasses.FirstOrDefault(a => a.AccountId == studentid && a.ClassID == classid);
                if (uc == null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsItTooLateToRegisteringThisClass(string classid)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable? tt = context.TimeTables.FirstOrDefault(a => a.ClassID == classid);
                if (tt == null) return false;
                else
                {
                    int hmmm = DateTime.Compare(DateTime.Now, tt.DateStart);
                    if (hmmm >= 0) return true;
                    else return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Dictionary<int, string> GetRooms()
        {
            var rooms = new Dictionary<int, string>();
            try
            {
                using var context = new FAMContext();
                foreach (Room r in context.Rooms)
                {
                    rooms.Add(r.RoomID, r.RoomName);
                }
                return rooms;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RoomDetail GetRoomDetail(int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                return context.RoomDetails
                                .Include(a => a.TimeTable.Class.Subject)
                                .Include(a => a.TimeTable.User)
                                .Include(a => a.Room)
                                .Single(a => a.RoomDetailID == roomdetailid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TimeTable GetTimeTable(int timetableid)
        {
            try
            {
                using var context = new FAMContext();
                return context.TimeTables.Single(a => a.TimeTableID == timetableid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TimeTable> GetTimeTables()
        {
            //List<TimeTable> timeTables = new List<TimeTable>();
            try
            {
                using var context = new FAMContext();
                var query = context.TimeTables.Include(a => a.Class.Subject).Include(a => a.User).ToList();
                //timeTables = context.TimeTables.ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetSubjectNameFromClassID(string classid)
        {
            try
            {
                using var context = new FAMContext();
                string subjectid = context.Classes.First(a => a.ClassID == classid).SubjectID;
                return context.Subjects.First(a => a.SubjectID == subjectid).SubjectName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetClassIDFromTimetableid(int timetableid)
        {
            try
            {
                using var context = new FAMContext();
                return context.TimeTables.Single(a => a.TimeTableID == timetableid).ClassID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetSubjectIdFromClassID(string classid)
        {
            try
            {
                using var context = new FAMContext();
                return context.Classes.First(a => a.ClassID == classid).SubjectID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetClassNameFromClassID(string classid)
        {
            try
            {
                using var context = new FAMContext();
                string name = context.Classes.First(a => a.ClassID == classid).Name;
                return name;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<RoomDetail> GetRoomDetails(int timetableid)
        {
            try
            {
                using var context = new FAMContext();
                /*var query = from opop in context.RoomDetails
                            join popo in context.Rooms on opop.RoomID equals popo.RoomID
                            where opop.TimeTableID == timetableid
                            select opop;*/
                var query = context.RoomDetails.Where(a => a.TimeTableID == timetableid)
                                               .OrderBy(a => a.SlotTotal)
                                                .Include(a => a.Room);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TimeTable? GetTimetableFromRoomDetailID(int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable? tt = context.TimeTables.SingleOrDefault(c => c.TimeTableID ==
                context.RoomDetails.SingleOrDefault(a => a.RoomDetailID == roomdetailid).TimeTableID);
                return tt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DateTime GetDateBusyFromRoomDetailID(int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                return context.RoomDetails.Single(a => a.RoomDetailID == roomdetailid).DateBusy;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetSlotCurrentFromRoomDetailID(int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                return context.RoomDetails.Single(a => a.RoomDetailID == roomdetailid).SlotCurrentDay;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetRoomName(int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                int roomid = context.RoomDetails.Single(r => r.RoomDetailID == roomdetailid).RoomID;
                return context.Rooms.Single(r => r.RoomID == roomid).RoomName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetTeacherNameFromAccountID(string accountid)
        {
            try
            {
                using var context = new FAMContext();
                return context.Users.First(a => a.AccountId == accountid).Ower;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsThatTimetableExists(string classid)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable? checker = context.TimeTables.SingleOrDefault(
                    adu => adu.ClassID == classid);
                if (checker != null) return true;
                else return false;
            }
            catch (Exception ex)
            {
                //return false; ((:
                throw new Exception(ex.Message);
            }
        }

        //get Subject require from classid
        public int GetSlotRequireFromClass(string classid)
        {
            try
            {
                using var context = new FAMContext();
                string subjectid = context.Classes.Single(c => c.ClassID == classid).SubjectID;
                return context.Subjects.Single(s => s.SubjectID == subjectid).SlotRequire;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /************ START CHECKING FUNCTION ***********/
        //start with monday
        public IEnumerable<RoomDetail> PositiveDayCheck(int roomid, int slotStyle, DateTime ThatDate, int slotRequire)
        {
            List<RoomDetail> result = new List<RoomDetail>();
            try
            {
                using var context = new FAMContext();
                /* low end code
                int divided = slotRequire / 3;
                DateTime CheckDate = ThatDate;
                for(int i = 0; i < divided;i++)
                {
                    RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                    && r.DateBusy == CheckDate && r.RoomID == roomid);
                    if (rd != null) return false;
                    CheckDate.AddDays(7);
                }
                CheckDate = ThatDate;
                CheckDate.AddDays(2);
                for (int i = 0; i < divided; i++)
                {
                    RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                    && r.DateBusy == CheckDate && r.RoomID == roomid);
                    if (rd != null) return false;
                    CheckDate.AddDays(7);
                }
                CheckDate = ThatDate;
                CheckDate.AddDays(2);
                for (int i = 0; i < divided; i++)
                {
                    RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                    && r.DateBusy == CheckDate && r.RoomID == roomid);
                    if (rd != null) return false;
                    CheckDate.AddDays(7);
                }
                return true;
                */
                DateTime firstdate = ThatDate;
                DateTime seconddate = ThatDate.AddDays(2);
                DateTime thirddate = ThatDate.AddDays(4);
                for (int i = 1; i <= slotRequire; i++)
                {
                    if (i % 3 == 1)
                    {
                        RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                        && r.DateBusy == firstdate && r.RoomID == roomid);
                        if (rd != null) result.Add(rd);
                        /* old weird code
                        rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                        && r.DateBusy == firstdate);
                        if (rd != null)
                        {
                            TimeTable tt = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID);
                            //if (tt.AccountId == teacherid) return false;
                        }
                        */
                    }
                    else if (i % 3 == 2)
                    {
                        RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                        && r.DateBusy == seconddate && r.RoomID == roomid);
                        if (rd != null) result.Add(rd);
                    }
                    else if (i % 3 == 0)
                    {
                        RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                        && r.DateBusy == thirddate && r.RoomID == roomid);
                        if (rd != null) result.Add(rd);
                        firstdate = firstdate.AddDays(7);
                        seconddate = seconddate.AddDays(7);
                        thirddate = thirddate.AddDays(7);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //start with tuesday
        public IEnumerable<RoomDetail> NegativeDayCheck(int roomid, int slotStyle, DateTime ThatDate, int slotRequire)
        {
            List<RoomDetail> result = new List<RoomDetail>();
            try
            {
                using var context = new FAMContext();
                DateTime firstdate = ThatDate;
                DateTime seconddate = ThatDate.AddDays(2);
                int slot1 = slotStyle;
                int slot2 = slotStyle + 1;
                if (slotStyle <= 5)
                {
                    for (int i = 1; i <= slotRequire; i++)
                    {
                        if (i % 3 == 1)
                        {
                            RoomDetail? rd = context.RoomDetails
                                .Include(a => a.TimeTable.Class)
                                .Include(a => a.TimeTable.User)
                                .FirstOrDefault(r => r.RoomID == roomid
                            && r.SlotCurrentDay == slot1 && r.DateBusy == firstdate);
                            if (rd != null) result.Add(rd);
                        }
                        else if (i % 3 == 2)
                        {
                            RoomDetail? rd = context.RoomDetails
                                .Include(a => a.TimeTable.Class)
                                .Include(a => a.TimeTable.User)
                                .FirstOrDefault(r => r.RoomID == roomid
                            && r.SlotCurrentDay == slot2 && r.DateBusy == firstdate);
                            if (rd != null) result.Add(rd);
                        }
                        else if (i % 3 == 0)
                        {
                            RoomDetail? rd = context.RoomDetails
                                .Include(a => a.TimeTable.Class)
                                .Include(a => a.TimeTable.User)
                                .FirstOrDefault(r => r.RoomID == roomid
                            && r.SlotCurrentDay == slot1 && r.DateBusy == seconddate);
                            if (rd != null) result.Add(rd);
                            firstdate = firstdate.AddDays(7);
                            seconddate = seconddate.AddDays(7);
                        }
                    }
                    return result;
                }
                else if (slotStyle >= 6 && slotStyle <= 10)
                {
                    slot1 -= 5;
                    slot2 -= 5;
                    for (int i = 1; i <= slotRequire; i++)
                    {
                        if (i % 3 == 1)
                        {
                            RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(r => r.RoomID == roomid
                            && r.SlotCurrentDay == slot1 && r.DateBusy == firstdate);
                            if (rd != null) result.Add(rd);
                        }
                        else if (i % 3 == 2)
                        {
                            RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(r => r.RoomID == roomid
                            && r.SlotCurrentDay == slot1 && r.DateBusy == seconddate);
                            if (rd != null) result.Add(rd);
                        }
                        else if (i % 3 == 0)
                        {
                            RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(r => r.RoomID == roomid
                            && r.SlotCurrentDay == slot2 && r.DateBusy == seconddate);
                            if (rd != null) result.Add(rd);
                            firstdate = firstdate.AddDays(7);
                            seconddate = seconddate.AddDays(7);
                        }
                    }
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TeacherPositiveDayCheck(string teacherid, int slotStyle, DateTime ThatDate, int slotRequire)
        {
            try
            {
                using var context = new FAMContext();
                DateTime firstdate = ThatDate;
                DateTime seconddate = ThatDate.AddDays(2);
                DateTime thirddate = ThatDate.AddDays(4);
                for (int i = 1; i <= slotRequire; i++)
                {
                    if (i % 3 == 1)
                    {
                        RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                        && r.DateBusy == firstdate);
                        if (rd != null)
                        {
                            string teacher = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                            if (teacher == teacherid) return false;
                        }
                    }
                    else if (i % 3 == 2)
                    {
                        RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                        && r.DateBusy == seconddate);
                        if (rd != null)
                        {
                            string teacher = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                            if (teacher == teacherid) return false;
                        }
                    }
                    else if (i % 3 == 0)
                    {
                        RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slotStyle
                        && r.DateBusy == thirddate);
                        if (rd != null)
                        {
                            string teacher = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                            if (teacher == teacherid) return false;
                        }
                        firstdate = firstdate.AddDays(7);
                        seconddate = seconddate.AddDays(7);
                        thirddate = thirddate.AddDays(7);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TeacherNegativeDayCheck(string teacherid, int slotStyle, DateTime ThatDate, int slotRequire)
        {
            try
            {
                using var context = new FAMContext();
                DateTime firstdate = ThatDate;
                DateTime seconddate = ThatDate.AddDays(2);
                int slot1 = slotStyle;
                int slot2 = slotStyle + 1;
                if (slotStyle <= 5)
                {
                    for (int i = 1; i <= slotRequire; i++)
                    {
                        if (i % 3 == 1)
                        {
                            RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slot1
                            && r.DateBusy == firstdate);
                            if (rd != null)
                            {
                                string teacher = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                                if (teacher == teacherid) return false;
                            }
                        }
                        else if (i % 3 == 2)
                        {
                            RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slot2
                            && r.DateBusy == firstdate);
                            if (rd != null)
                            {
                                string teacher = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                                if (teacher == teacherid) return false;
                            }
                        }
                        else if (i % 3 == 0)
                        {
                            RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slot1
                            && r.DateBusy == seconddate);
                            if (rd != null)
                            {
                                string teacher = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                                if (teacher == teacherid) return false;
                            }
                            firstdate = firstdate.AddDays(7);
                            seconddate = seconddate.AddDays(7);
                        }
                    }
                    return true;
                }
                else if (slotStyle >= 6 && slotStyle <= 10)
                {
                    slot1 -= 5;
                    slot2 -= 5;
                    for (int i = 1; i <= slotRequire; i++)
                    {
                        if (i % 3 == 1)
                        {
                            RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slot1
                            && r.DateBusy == firstdate);
                            if (rd != null)
                            {
                                string teacher = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                                if (teacher == teacherid) return false;
                            }
                        }
                        else if (i % 3 == 2)
                        {
                            RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slot1
                            && r.DateBusy == seconddate);
                            if (rd != null)
                            {
                                string teacher = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                                if (teacher == teacherid) return false;
                            }
                        }
                        else if (i % 3 == 0)
                        {
                            RoomDetail? rd = context.RoomDetails.FirstOrDefault(r => r.SlotCurrentDay == slot2
                            && r.DateBusy == seconddate);
                            if (rd != null)
                            {
                                string teacher = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                                if (teacher == teacherid) return false;
                            }
                            firstdate = firstdate.AddDays(7);
                            seconddate = seconddate.AddDays(7);
                        }
                    }
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //BEGIN ADVANCED UPGRADE CHECKING
        public IEnumerable<RoomDetail> RepeatedDayChecking(int roomid, DateTime StartDate,
            int repeatSperatedDay, int slotInday, int slotRequire)
        {
            List<RoomDetail> result = new List<RoomDetail>();
            try
            {
                using var context = new FAMContext();
                DateTime dummy = StartDate;
                for (int i = 1; i <= slotRequire; i++)
                {
                    RoomDetail? rd = context.RoomDetails
                        .Include(a => a.TimeTable.Class)
                        .Include(a => a.TimeTable.User)
                        .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                        && r.DateBusy == dummy && r.RoomID == roomid);
                    if (rd != null) result.Add(rd);
                    dummy = dummy.AddDays(repeatSperatedDay);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<RoomDetail> RepeatedWeekChecking(int roomid, DateTime StartDate,
            bool[] weeklyRepeated, int slotInday, int slotRequire, int seperatedWeek)
        {
            List<RoomDetail> result = new List<RoomDetail>();
            try
            {
                //please check this
                //if (weeklyRepeated.All(x => false)) return false;
                using var context = new FAMContext();
                int batdaututhumay = 0;
                // 0 sunday 1 monday 2 tuesday 3 wednesday 4 thursday 5 friday 6 saturday
                foreach (bool day in weeklyRepeated)
                {
                    if (day) break;
                    batdaututhumay++;
                }
                int dayofweek = (int)StartDate.DayOfWeek;
                // 0 sunday 1 monday 2 tuesday 3 wednesday 4 thursday 5 friday 6 saturday
                if (dayofweek <= batdaututhumay)
                {
                    int KhiNaoToiGapEm = 0;
                    if (dayofweek != batdaututhumay)
                    {
                        while (dayofweek != batdaututhumay)
                        {
                            dayofweek++;
                            if (dayofweek == 7) dayofweek = 0;
                            if (dayofweek == batdaututhumay) break;
                            KhiNaoToiGapEm++;
                        }
                        KhiNaoToiGapEm++;
                    }
                    DateTime ActualStartDate = StartDate.AddDays(KhiNaoToiGapEm);
                    int totalrepeat = 0;
                    foreach (bool huhu in weeklyRepeated)
                    {
                        if (huhu) totalrepeat++;
                    }
                    int repeatingmakesense = slotRequire / totalrepeat;
                    int remainSlot = slotRequire - (repeatingmakesense * totalrepeat);

                    DateTime firstdate = ActualStartDate;
                    int jackpot = 1;
                    int checkpoint = 0; //in the loop we already mentioned
                    for (int i = 0; i < totalrepeat; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail? rd = context.RoomDetails
                                .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                                .FirstOrDefault(a => a.RoomID == roomid &&
                                                a.DateBusy == firstdate && a.SlotCurrentDay == slotInday);
                                if (rd != null) result.Add(rd);
                                firstdate = firstdate.AddDays(7 * seperatedWeek);
                            }
                            dayofweek++;
                            for (int yeheboi = dayofweek; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            if (checkpoint >= 7) checkpoint = 0;
                        }
                        else if (dayofweek != batdaututhumay)
                        {
                            DateTime ForABetterDay = ActualStartDate.AddDays(jackpot);
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail? rd = context.RoomDetails
                                .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                                .FirstOrDefault(a => a.RoomID == roomid &&
                                                a.DateBusy == ForABetterDay && a.SlotCurrentDay == slotInday);
                                if (rd != null) result.Add(rd);
                                ForABetterDay = ForABetterDay.AddDays(7 * seperatedWeek);
                            }
                            for (int yeheboi = checkpoint; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            jackpot++;
                            if (checkpoint >= 7) checkpoint = 0;
                        }
                    }
                    int KhiNaoEmGapToi = repeatingmakesense * 7 * seperatedWeek;
                    dayofweek = batdaututhumay;
                    DateTime wohoo = ActualStartDate.AddDays(KhiNaoEmGapToi);
                    for (int i = 0; i < remainSlot; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(a => a.RoomID == roomid &&
                                                a.DateBusy == wohoo && a.SlotCurrentDay == slotInday);
                            if (rd != null) result.Add(rd);
                            dayofweek++;
                        }
                        else if (dayofweek != batdaututhumay)
                        {
                            KhiNaoEmGapToi = 1;
                            for (int hihi = dayofweek;
                                hihi < weeklyRepeated.Length; hihi++) //now we use the old technic
                            {
                                dayofweek = hihi + 1;
                                if (weeklyRepeated[hihi]) break;
                                else if (!weeklyRepeated[hihi]) KhiNaoEmGapToi++;
                            }
                            wohoo = wohoo.AddDays(KhiNaoEmGapToi);
                            RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(a => a.RoomID == roomid &&
                                                a.DateBusy == wohoo && a.SlotCurrentDay == slotInday);
                            if (rd != null) result.Add(rd);
                        }
                    }// the end is near
                }
                else if (dayofweek > batdaututhumay)// well google algorithm is far more than this
                                                    //but what can I do to surpass their mind?
                {
                    int PreSlotBeforeLoop = 0;
                    for (int syn = dayofweek; syn < weeklyRepeated.Length; syn++)
                        if (weeklyRepeated[syn]) PreSlotBeforeLoop++;
                    //3 lines above solve how many day from date start to the end of week was looped
                    //có 2 trường hợp, 1 howmanydayswithoutyou sẽ = 0 (là trường hợp tốt nhất)
                    //trường hợp 2 > 0 chúng ta sẽ giải quyết nó bằng cái for
                    if (PreSlotBeforeLoop > 0)
                    {
                        int KhiNaoTaGapNhau = 0;
                        int smallcheckpoint = 0;
                        for (int water = dayofweek; water < weeklyRepeated.Length; water++)
                        {
                            smallcheckpoint = water + 1;
                            if (weeklyRepeated[water]) break;
                            else if (!weeklyRepeated[water]) KhiNaoTaGapNhau++;
                        }
                        DateTime puppet = StartDate.AddDays(KhiNaoTaGapNhau);
                        for (int i = 0; i < PreSlotBeforeLoop; i++)
                        {
                            RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(a => a.RoomID == roomid &&
                                                a.DateBusy == puppet && a.SlotCurrentDay == slotInday);
                            if (rd != null) result.Add(rd);
                            if (smallcheckpoint >= 6) break;
                            int motto = 1;
                            for (int j = smallcheckpoint; j < weeklyRepeated.Length; j++)
                            {
                                smallcheckpoint = j + 1;
                                if (weeklyRepeated[j]) break;
                                else if (!weeklyRepeated[j]) motto++;
                            }
                            puppet = puppet.AddDays(motto);
                        }
                    }
                    //like usual we start to make thing like nothing happend except we need to calculate the remain slot
                    //perfectly balance as it should be
                    int KhiNaoToiGapEm = 1;
                    while (dayofweek != batdaututhumay)
                    {
                        dayofweek++;
                        if (dayofweek == 7) dayofweek = 0;
                        if (dayofweek == batdaututhumay) break;
                        KhiNaoToiGapEm++;
                    }
                    DateTime TheDayAfterProcess = StartDate.AddDays(KhiNaoToiGapEm + (7 * (seperatedWeek - 1)));
                    int totalrepeat = 0;
                    foreach (bool huhu in weeklyRepeated)
                    {
                        if (huhu) totalrepeat++;
                    }
                    int SlotAfterProcess = slotRequire - PreSlotBeforeLoop;
                    int repeatingmakesense = SlotAfterProcess / totalrepeat;
                    int remainSlot = SlotAfterProcess - (repeatingmakesense * totalrepeat);
                    DateTime firstTime = TheDayAfterProcess;
                    int jackpot = 1;
                    int checkpoint = 0;
                    for (int i = 0; i < totalrepeat; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail? rd = context.RoomDetails
                                .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                                .FirstOrDefault(a => a.RoomID == roomid &&
                                                a.DateBusy == firstTime && a.SlotCurrentDay == slotInday);
                                if (rd != null) result.Add(rd);
                                firstTime = firstTime.AddDays(7 * seperatedWeek);
                            }
                            dayofweek++;
                            //throw new Exception(dayofweek.ToString());
                            for (int yeheboi = dayofweek; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            if (checkpoint >= 7) checkpoint = 0;
                            //throw new Exception(jackpot.ToString() + " _ " + checkpoint.ToString() + " _ " + totalrepeat.ToString());
                            //if (dayofweek >= 7) dayofweek = 0;
                        }
                        else if (dayofweek != batdaututhumay)
                        //bắt đầu từ vòng thứ 2 của total repeat nghĩa là dayofweek đã khác với batdaututhumay
                        {
                            DateTime ForABetterDay = TheDayAfterProcess.AddDays(jackpot);
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail? rd = context.RoomDetails
                                .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                                .FirstOrDefault(a => a.RoomID == roomid &&
                                                a.DateBusy == ForABetterDay && a.SlotCurrentDay == slotInday);
                                if (rd != null) result.Add(rd);
                                ForABetterDay = ForABetterDay.AddDays(7 * seperatedWeek);
                            }
                            for (int yeheboi = checkpoint; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            jackpot++;
                            if (checkpoint >= 7) checkpoint = 0;

                            //if(i==3) throw new Exception(checkpoint.ToString());
                        }
                    }// am fucking done with this shit

                    //final check and blow up, solving the remain slot
                    int KhiNaoEmGapToi = repeatingmakesense * 7 * seperatedWeek;
                    dayofweek = batdaututhumay;
                    DateTime wohoo = TheDayAfterProcess.AddDays(KhiNaoEmGapToi);
                    for (int i = 0; i < remainSlot; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(a => a.RoomID == roomid &&
                                                a.DateBusy == wohoo && a.SlotCurrentDay == slotInday);
                            if (rd != null) result.Add(rd);
                            dayofweek++;
                        }
                        else if (dayofweek != batdaututhumay)
                        {
                            KhiNaoEmGapToi = 1;
                            for (int hihi = dayofweek;
                                hihi < weeklyRepeated.Length; hihi++) //now we use the old technic
                            {
                                dayofweek = hihi + 1;
                                if (weeklyRepeated[hihi]) break;
                                else if (!weeklyRepeated[hihi]) KhiNaoEmGapToi++;
                            }
                            wohoo = wohoo.AddDays(KhiNaoEmGapToi);
                            RoomDetail? rd = context.RoomDetails
                            .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                            .FirstOrDefault(a => a.RoomID == roomid &&
                                                a.DateBusy == wohoo && a.SlotCurrentDay == slotInday);
                            if (rd != null) result.Add(rd);
                        }
                    }//end final loop for the remain slot
                }//finishing the big else if block checking input day with start of the week day
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<RoomDetail> RepeatedMonthChecking(int roomid, DateTime StartDate,
            int repeatSeperatedMonth, int slotInday, int slotRequire)
        {
            List<RoomDetail> result = new List<RoomDetail>();
            try
            {
                using var context = new FAMContext();
                DateTime dummy = StartDate;
                for (int i = 1; i <= slotRequire; i++)
                {
                    RoomDetail? rd = context.RoomDetails
                    .Include(a => a.TimeTable.Class)
                            .Include(a => a.TimeTable.User)
                    .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                        && r.DateBusy == dummy && r.RoomID == roomid);
                    if (rd != null) result.Add(rd);
                    dummy = dummy.AddMonths(repeatSeperatedMonth);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TeacherRepeatedDayChecking(string teacherid, DateTime StartDate,
            int repeatSperatedDay, int slotInday, int slotRequire)
        {
            try
            {
                using var context = new FAMContext();
                for (int i = 1; i <= slotRequire; i++)
                {
                    RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                        .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                        && r.DateBusy == StartDate);
                    if (rd != null)
                        if (rd.TimeTable.AccountId == teacherid) return false;
                    StartDate = StartDate.AddDays(repeatSperatedDay);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TeacherRepeatedWeekChecking(string teacherid, DateTime StartDate,
            bool[] weeklyRepeated, int slotInday, int slotRequire, int seperatedWeek)
        {
            //please check this
            if (weeklyRepeated.All(x => false)) return false;
            try
            {
                using var context = new FAMContext();
                int batdaututhumay = 0;
                // 0 sunday 1 monday 2 tuesday 3 wednesday 4 thursday 5 friday 6 saturday
                foreach (bool day in weeklyRepeated)
                {
                    if (day) break;
                    batdaututhumay++;
                }
                int dayofweek = (int)StartDate.DayOfWeek;
                // 0 sunday 1 monday 2 tuesday 3 wednesday 4 thursday 5 friday 6 saturday
                if (dayofweek <= batdaututhumay)
                {
                    int KhiNaoToiGapEm = 0;
                    if (dayofweek != batdaututhumay)
                    {
                        while (dayofweek != batdaututhumay)
                        {
                            dayofweek++;
                            if (dayofweek == 7) dayofweek = 0;
                            if (dayofweek == batdaututhumay) break;
                            KhiNaoToiGapEm++;
                        }
                        KhiNaoToiGapEm++;
                    }
                    DateTime ActualStartDate = StartDate.AddDays(KhiNaoToiGapEm);
                    int totalrepeat = 0;
                    foreach (bool huhu in weeklyRepeated)
                    {
                        if (huhu) totalrepeat++;
                    }
                    int repeatingmakesense = slotRequire / totalrepeat;
                    int remainSlot = slotRequire - (repeatingmakesense * totalrepeat);

                    DateTime firstdate = ActualStartDate;
                    int jackpot = 1;
                    int checkpoint = 0; //in the loop we already mentioned
                    for (int i = 0; i < totalrepeat; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                                                        .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                                                        && r.DateBusy == firstdate);
                                if (rd != null)
                                    if (rd.TimeTable.AccountId == teacherid) return false;
                                firstdate = firstdate.AddDays(7 * seperatedWeek);
                            }
                            dayofweek++;
                            for (int yeheboi = dayofweek; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            if (checkpoint >= 7) checkpoint = 0;
                        }
                        else if (dayofweek != batdaututhumay)
                        {
                            DateTime ForABetterDay = ActualStartDate.AddDays(jackpot);
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                                                    .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                                                    && r.DateBusy == ForABetterDay);
                                if (rd != null)
                                    if (rd.TimeTable.AccountId == teacherid) return false;
                                ForABetterDay = ForABetterDay.AddDays(7 * seperatedWeek);
                            }
                            for (int yeheboi = checkpoint; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            jackpot++;
                            if (checkpoint >= 7) checkpoint = 0;
                        }
                    }
                    int KhiNaoEmGapToi = repeatingmakesense * 7;
                    dayofweek = batdaututhumay;
                    DateTime wohoo = ActualStartDate.AddDays(KhiNaoEmGapToi);
                    for (int i = 0; i < remainSlot; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                                                        .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                                                        && r.DateBusy == wohoo);
                            if (rd != null)
                                if (rd.TimeTable.AccountId == teacherid) return false;
                            dayofweek++;
                        }
                        else if (dayofweek != batdaututhumay)
                        {
                            KhiNaoEmGapToi = 1;
                            for (int hihi = dayofweek;
                                hihi < weeklyRepeated.Length; hihi++) //now we use the old technic
                            {
                                dayofweek = hihi + 1;
                                if (weeklyRepeated[hihi]) break;
                                else if (!weeklyRepeated[hihi]) KhiNaoEmGapToi++;
                            }
                            wohoo = wohoo.AddDays(KhiNaoEmGapToi);
                            RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                                                .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                                                && r.DateBusy == wohoo);
                            if (rd != null)
                                if (rd.TimeTable.AccountId == teacherid) return false;
                        }
                    }// the end is near
                }
                else if (dayofweek > batdaututhumay)// well google algorithm is far more than this
                                                    //but what can I do to surpass their mind?
                {
                    int PreSlotBeforeLoop = 0;
                    for (int syn = dayofweek; syn < weeklyRepeated.Length; syn++)
                        if (weeklyRepeated[syn]) PreSlotBeforeLoop++;
                    //3 lines above solve how many day from date start to the end of week was looped
                    //có 2 trường hợp, 1 howmanydayswithoutyou sẽ = 0 (là trường hợp tốt nhất)
                    //trường hợp 2 > 0 chúng ta sẽ giải quyết nó bằng cái for
                    if (PreSlotBeforeLoop > 0)
                    {
                        int KhiNaoTaGapNhau = 0;
                        int smallcheckpoint = 0;
                        for (int water = dayofweek; water < weeklyRepeated.Length; water++)
                        {
                            smallcheckpoint = water + 1;
                            if (weeklyRepeated[water]) break;
                            else if (!weeklyRepeated[water]) KhiNaoTaGapNhau++;
                        }
                        DateTime puppet = StartDate.AddDays(KhiNaoTaGapNhau);
                        for (int i = 0; i < PreSlotBeforeLoop; i++)
                        {
                            RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                                                        .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                                                        && r.DateBusy == puppet);
                            if (rd != null)
                                if (rd.TimeTable.AccountId == teacherid) return false;
                            if (smallcheckpoint >= 6) break;
                            int motto = 1;
                            for (int j = smallcheckpoint; j < weeklyRepeated.Length; j++)
                            {
                                smallcheckpoint = j + 1;
                                if (weeklyRepeated[j]) break;
                                else if (!weeklyRepeated[j]) motto++;
                            }
                            puppet = puppet.AddDays(motto);
                        }
                    }
                    //like usual we start to make thing like nothing happend except we need to calculate the remain slot
                    //perfectly balance as it should be
                    int KhiNaoToiGapEm = 1;
                    while (dayofweek != batdaututhumay)
                    {
                        dayofweek++;
                        if (dayofweek == 7) dayofweek = 0;
                        if (dayofweek == batdaututhumay) break;
                        KhiNaoToiGapEm++;
                    }
                    DateTime TheDayAfterProcess = StartDate.AddDays(KhiNaoToiGapEm);
                    int totalrepeat = 0;
                    foreach (bool huhu in weeklyRepeated)
                    {
                        if (huhu) totalrepeat++;
                    }
                    int SlotAfterProcess = slotRequire - PreSlotBeforeLoop;
                    int repeatingmakesense = SlotAfterProcess / totalrepeat;
                    int remainSlot = SlotAfterProcess - (totalrepeat * repeatingmakesense);
                    DateTime firstTime = TheDayAfterProcess;
                    int jackpot = 1;
                    int checkpoint = 0;
                    for (int i = 0; i < totalrepeat; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                                                            .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                                                            && r.DateBusy == firstTime);
                                if (rd != null)
                                    if (rd.TimeTable.AccountId == teacherid) return false;
                                firstTime = firstTime.AddDays(7 * seperatedWeek);
                            }
                            dayofweek++;
                            //throw new Exception(dayofweek.ToString());
                            for (int yeheboi = dayofweek; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            if (checkpoint >= 7) checkpoint = 0;
                            //throw new Exception(jackpot.ToString() + " _ " + checkpoint.ToString() + " _ " + totalrepeat.ToString());
                            //if (dayofweek >= 7) dayofweek = 0;
                        }
                        else if (dayofweek != batdaututhumay)
                        //bắt đầu từ vòng thứ 2 của total repeat nghĩa là dayofweek đã khác với batdaututhumay
                        {
                            DateTime ForABetterDay = TheDayAfterProcess.AddDays(jackpot);
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                                                    .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                                                    && r.DateBusy == ForABetterDay);
                                if (rd != null)
                                    if (rd.TimeTable.AccountId == teacherid) return false;
                                ForABetterDay = ForABetterDay.AddDays(7 * seperatedWeek);
                            }
                            for (int yeheboi = checkpoint; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            jackpot++;
                            if (checkpoint >= 7) checkpoint = 0;

                            //if(i==3) throw new Exception(checkpoint.ToString());
                        }
                    }// am fucking done with this shit

                    //final check and blow up, solving the remain slot
                    int KhiNaoEmGapToi = repeatingmakesense * 7;
                    dayofweek = batdaututhumay;
                    DateTime wohoo = TheDayAfterProcess.AddDays(KhiNaoEmGapToi);
                    for (int i = 0; i < remainSlot; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                                                        .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                                                        && r.DateBusy == wohoo);
                            if (rd != null)
                                if (rd.TimeTable.AccountId == teacherid) return false;
                            dayofweek++;
                        }
                        else if (dayofweek != batdaututhumay)
                        {
                            KhiNaoEmGapToi = 1;
                            for (int hihi = dayofweek;
                                hihi < weeklyRepeated.Length; hihi++) //now we use the old technic
                            {
                                dayofweek = hihi + 1;
                                if (weeklyRepeated[hihi]) break;
                                else if (!weeklyRepeated[hihi]) KhiNaoEmGapToi++;
                            }
                            wohoo = wohoo.AddDays(KhiNaoEmGapToi);
                            RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                                                .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                                                && r.DateBusy == wohoo);
                            if (rd != null)
                                if (rd.TimeTable.AccountId == teacherid) return false;
                        }
                    }//end final loop for the remain slot
                }//finishing the big else if block checking input day with start of the week day
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TeacherRepeatedMonthChecking(string teacherid, DateTime StartDate,
            int repeatSeperatedMonth, int slotInday, int slotRequire)
        {
            try
            {
                using var context = new FAMContext();
                for (int i = 1; i <= slotRequire; i++)
                {
                    RoomDetail? rd = context.RoomDetails.Include(a => a.TimeTable)
                        .FirstOrDefault(r => r.SlotCurrentDay == slotInday
                        && r.DateBusy == StartDate);
                    if (rd != null)
                        if (rd.TimeTable.AccountId == teacherid) return false;
                    StartDate = StartDate.AddMonths(repeatSeperatedMonth);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /****** END CHECKING FUNCTION *****/

        /**** START TIMETABLE CREATION *****/
        public void CreateTimetablePositiveDay(string teacherid, DateTime StartDate,
            int slotRequire, int roomid, string classid, int slotcurrent)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable tt = new TimeTable();
                tt.ClassID = classid;
                tt.AccountId = teacherid;
                tt.DateStart = StartDate;
                tt.DateCreated = DateTime.Now;
                context.TimeTables.Add(tt);
                context.SaveChanges();
                //finishing creation by adding time to that timetable
                DateTime firstdate = StartDate;
                DateTime seconddate = StartDate.AddDays(2);
                DateTime thirddate = StartDate.AddDays(4);
                for (int i = 1; i <= slotRequire; i++)
                {
                    if (i % 3 == 1)
                    {
                        RoomDetail rd = new RoomDetail();
                        rd.RoomID = roomid;
                        rd.TimeTableID = tt.TimeTableID;
                        rd.SlotCurrentDay = slotcurrent;
                        rd.DateBusy = firstdate;
                        rd.AttendanceAction = "No Action yet";
                        rd.SlotTotal = i;
                        context.RoomDetails.Add(rd);
                        context.SaveChanges();
                    }
                    else if (i % 3 == 2)
                    {
                        RoomDetail rd = new RoomDetail();
                        rd.RoomID = roomid;
                        rd.TimeTableID = tt.TimeTableID;
                        rd.SlotCurrentDay = slotcurrent;
                        rd.DateBusy = seconddate;
                        rd.AttendanceAction = "No Action yet";
                        rd.SlotTotal = i;
                        context.RoomDetails.Add(rd);
                        context.SaveChanges();
                    }
                    else if (i % 3 == 0)
                    {
                        RoomDetail rd = new RoomDetail();
                        rd.RoomID = roomid;
                        rd.TimeTableID = tt.TimeTableID;
                        rd.SlotCurrentDay = slotcurrent;
                        rd.DateBusy = thirddate;
                        rd.AttendanceAction = "No Action yet";
                        rd.SlotTotal = i;
                        context.RoomDetails.Add(rd);
                        context.SaveChanges();
                        firstdate = firstdate.AddDays(7);
                        seconddate = seconddate.AddDays(7);
                        thirddate = thirddate.AddDays(7);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateTimetableNegativeDay(string teacherid, DateTime StartDate,
            int slotRequire, int roomid, string classid, int slotcurrent)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable tt = new TimeTable();
                tt.ClassID = classid;
                tt.AccountId = teacherid;
                tt.DateStart = StartDate;
                tt.DateCreated = DateTime.Now;
                context.TimeTables.Add(tt);
                context.SaveChanges();
                //finishing creation by adding time to that timetable
                DateTime firstdate = StartDate;
                DateTime seconddate = StartDate.AddDays(2);
                int slot1 = slotcurrent;
                int slot2 = slotcurrent + 1;
                if (slotcurrent <= 5)
                {
                    for (int i = 1; i <= slotRequire; i++)
                    {
                        if (i % 3 == 1)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot1;
                            rd.DateBusy = firstdate;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = i;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                        }
                        else if (i % 3 == 2)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot2;
                            rd.DateBusy = firstdate;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = i;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                        }
                        else if (i % 3 == 0)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot1;
                            rd.DateBusy = seconddate;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = i;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                            firstdate = firstdate.AddDays(7);
                            seconddate = seconddate.AddDays(7);
                        }
                    }
                }
                else if (slotcurrent >= 6 && slotcurrent <= 10)
                {
                    slot1 -= 5;
                    slot2 -= 5;
                    for (int i = 1; i <= slotRequire; i++)
                    {
                        if (i % 3 == 1)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot1;
                            rd.DateBusy = firstdate;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = i;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                        }
                        else if (i % 3 == 2)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot1;
                            rd.DateBusy = seconddate;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = i;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                        }
                        else if (i % 3 == 0)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot2;
                            rd.DateBusy = seconddate;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = i;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                            firstdate = firstdate.AddDays(7);
                            seconddate = seconddate.AddDays(7);
                        }
                    }
                }
                /*else if (slotcurrent >= 11 && slotcurrent <= 15)
                {
                    slot1 -= 10;
                    slot2 -= 10;
                    for (int i = 1; i <= slotRequire; i++)
                    {
                        if (i % 3 == 1)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot1;
                            rd.DateBusy = firstdate;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                        }
                        else if (i % 3 == 2)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot2;
                            rd.DateBusy = firstdate;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                        }
                        else if (i % 3 == 0)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot2;
                            rd.DateBusy = seconddate;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                            firstdate.AddDays(7);
                            seconddate.AddDays(7);
                        }
                    }
                }
                else if (slotcurrent >= 16 && slotcurrent <= 20)
                {
                    slot1 -= 15;
                    slot2 -= 15;
                    for (int i = 1; i <= slotRequire; i++)
                    {
                        if (i % 3 == 1)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot2;
                            rd.DateBusy = firstdate;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                        }
                        else if (i % 3 == 2)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot1;
                            rd.DateBusy = seconddate;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                        }
                        else if (i % 3 == 0)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slot2;
                            rd.DateBusy = seconddate;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                            firstdate.AddDays(7);
                            seconddate.AddDays(7);
                        }
                    }
                }*/
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateRepeatedDay(string teacherid, string classid, int roomid,
            DateTime StartDate, int repeatSperatedDay, int slotInday, int slotRequire)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable tt = new TimeTable();
                tt.ClassID = classid;
                tt.AccountId = teacherid;
                tt.DateStart = StartDate;
                tt.DateCreated = DateTime.Now;
                context.TimeTables.Add(tt);
                context.SaveChanges();
                for (int i = 1; i <= slotRequire; i++)
                {
                    RoomDetail rd = new RoomDetail();
                    rd.RoomID = roomid;
                    rd.TimeTableID = tt.TimeTableID;
                    rd.SlotCurrentDay = slotInday;
                    rd.DateBusy = StartDate;
                    rd.AttendanceAction = "No Action yet";
                    rd.SlotTotal = i;
                    context.RoomDetails.Add(rd);
                    context.SaveChanges();
                    StartDate = StartDate.AddDays(repeatSperatedDay);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateRepeatedWeek(string teacherid, string classid, int roomid,
            DateTime StartDate, bool[] weeklyRepeated, int slotInday, int slotRequire, int seperatedWeek)
        {
            //please check this
            if (weeklyRepeated.All(x => false)) throw new Exception("ThatsNotHowThingWork");
            try
            {
                using var context = new FAMContext();
                TimeTable tt = new TimeTable();
                tt.ClassID = classid;
                tt.AccountId = teacherid;
                tt.DateStart = StartDate;
                tt.DateCreated = DateTime.Now;
                context.TimeTables.Add(tt);
                context.SaveChanges();
                //finishing creation by adding time to that timetable

                //giải quyết vấn đề input ngày bắt đầu lệch cái ngày trong tuần bắt đầu
                //giải quyết vấn đề ngày nào được chọn trong tuần này
                int batdaututhumay = 0;
                // 0 sunday 1 monday 2 tuesday 3 wednesday 4 thursday 5 friday 6 saturday
                foreach (bool day in weeklyRepeated)
                {
                    if (day) break;
                    batdaututhumay++;
                }
                int dayofweek = (int)StartDate.DayOfWeek;
                //throw new Exception("bat dau: " + batdaututhumay.ToString() + " _ " + dayofweek);
                // 0 sunday 1 monday 2 tuesday 3 wednesday 4 thursday 5 friday 6 saturday
                if (dayofweek <= batdaututhumay)
                {
                    int KhiNaoToiGapEm = 0;
                    if (dayofweek != batdaututhumay)
                    {
                        while (dayofweek != batdaututhumay) //trường hợp nhập ngày bắt đầu lệch 
                        {
                            dayofweek++;
                            if (dayofweek == 7) dayofweek = 0;
                            if (dayofweek == batdaututhumay) break;
                            KhiNaoToiGapEm++;
                        }//my brain is dying for those little code
                        KhiNaoToiGapEm++;
                    }
                    //int dayofweekforremain = dayofweek;
                    DateTime ActualStartDate = StartDate.AddDays(KhiNaoToiGapEm);
                    //đếm xem bao nhiêu ngày bị tách
                    int totalrepeat = 0;
                    foreach (bool huhu in weeklyRepeated)
                    {
                        if (huhu) totalrepeat++;
                    }
                    int repeatingmakesense = slotRequire / totalrepeat;
                    //throw new Exception(repeatingmakesense.ToString());
                    int remainSlot = slotRequire - (repeatingmakesense * totalrepeat);
                    //throw new Exception(remainSlot.ToString());

                    DateTime firstdate = ActualStartDate;
                    int jackpot = 1;
                    int checkpoint = 0; //in the loop we already mentioned
                    int slot2 = 2;            //throw new Exception(totalrepeat.ToString());
                    int holdingSlot = slot2;
                    for (int i = 0; i < totalrepeat; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            int slot = 1;
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail rd = new RoomDetail();
                                rd.RoomID = roomid;
                                rd.TimeTableID = tt.TimeTableID;
                                rd.SlotCurrentDay = slotInday;
                                rd.DateBusy = firstdate;
                                rd.AttendanceAction = "No Action yet";
                                rd.SlotTotal = slot;
                                context.RoomDetails.Add(rd);
                                context.SaveChanges();
                                firstdate = firstdate.AddDays(7 * seperatedWeek);
                                slot += totalrepeat;
                            }
                            dayofweek++;
                            //throw new Exception(dayofweek.ToString());
                            for (int yeheboi = dayofweek; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            if (checkpoint >= 7) checkpoint = 0;
                            //throw new Exception(jackpot.ToString() + " _ " + checkpoint.ToString() + " _ " + totalrepeat.ToString());
                            //if (dayofweek >= 7) dayofweek = 0;
                        }
                        else if (dayofweek != batdaututhumay)
                        //bắt đầu từ vòng thứ 2 của total repeat nghĩa là dayofweek đã khác với batdaututhumay
                        {
                            //int KhiNaoEmGapToi = 1;
                            //dayofweek chắc chắn đã + 1 từ vòng lặp trên
                            /*
                            for (int yeheboi = dayofweek; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                if (!weeklyRepeated[yeheboi]) KhiNaoEmGapToi++;
                                else if (weeklyRepeated[yeheboi]) break;
                            }*/
                            DateTime ForABetterDay = ActualStartDate.AddDays(jackpot);
                            //throw new Exception(ForABetterDay.ToString());
                            /*
                            if (i == 1)
                                throw new Exception(dayofweek.ToString() + " _ " + batdaututhumay.ToString() + "_" + ForABetterDay.ToString());
                            */
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail rd = new RoomDetail();
                                rd.RoomID = roomid;
                                rd.TimeTableID = tt.TimeTableID;
                                rd.SlotCurrentDay = slotInday;
                                rd.DateBusy = ForABetterDay;
                                rd.AttendanceAction = "No Action yet";
                                rd.SlotTotal = slot2;
                                context.RoomDetails.Add(rd);
                                context.SaveChanges();
                                ForABetterDay = ForABetterDay.AddDays(7 * seperatedWeek);
                                slot2 += totalrepeat;
                            }
                            for (int yeheboi = checkpoint; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            jackpot++;
                            holdingSlot++;
                            slot2 = holdingSlot;
                            if (checkpoint >= 7) checkpoint = 0;

                            //if(i==3) throw new Exception(checkpoint.ToString());
                        }
                    }// am fucking done with this shit

                    //final check and blow up, solving the remain slot
                    int KhiNaoEmGapToi = repeatingmakesense * 7 * seperatedWeek;//số ngày sau khi hoàn thành đống code dị hụm ở trên
                    dayofweek = batdaututhumay;
                    DateTime wohoo = ActualStartDate.AddDays(KhiNaoEmGapToi);
                    int startRemainSlot = repeatingmakesense * totalrepeat + 1;
                    //my brain tell me that this wont exceed slot require. Or Am I wrong?
                    for (int i = 0; i < remainSlot; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slotInday;
                            rd.DateBusy = wohoo;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = startRemainSlot;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                            dayofweek++;
                            startRemainSlot++;
                        }
                        else if (dayofweek != batdaututhumay)
                        {
                            KhiNaoEmGapToi = 1;
                            for (int hihi = dayofweek;
                                hihi < weeklyRepeated.Length; hihi++) //now we use the old technic
                            {
                                dayofweek = hihi + 1;
                                if (weeklyRepeated[hihi]) break;
                                else if (!weeklyRepeated[hihi]) KhiNaoEmGapToi++;
                            }
                            wohoo = wohoo.AddDays(KhiNaoEmGapToi);
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slotInday;
                            rd.DateBusy = wohoo;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = startRemainSlot;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                            startRemainSlot++;
                        }
                    }// the end is near
                }
                else if (dayofweek > batdaututhumay)// well google algorithm is far more than this
                                                    //but what can I do to surpass their mind?
                {
                    int PreSlotBeforeLoop = 0;
                    for (int syn = dayofweek; syn < weeklyRepeated.Length; syn++)
                        if (weeklyRepeated[syn]) PreSlotBeforeLoop++;
                    //3 lines above solve how many day from date start to the end of week was looped
                    //có 2 trường hợp, 1 howmanydayswithoutyou sẽ = 0 (là trường hợp tốt nhất)
                    //trường hợp 2 > 0 chúng ta sẽ giải quyết nó bằng cái for
                    //giải quyết slot course session numba
                    int slot = 1;
                    if (PreSlotBeforeLoop > 0)
                    {
                        int KhiNaoTaGapNhau = 0; //this look very very sus
                        int smallcheckpoint = 0;
                        for (int water = dayofweek; water < weeklyRepeated.Length; water++)
                        {
                            smallcheckpoint = water + 1;
                            if (weeklyRepeated[water]) break;
                            else if (!weeklyRepeated[water]) KhiNaoTaGapNhau++;
                        }
                        //throw new Exception(KhiNaoTaGapNhau.ToString());
                        DateTime puppet = StartDate.AddDays(KhiNaoTaGapNhau);
                        for (int i = 0; i < PreSlotBeforeLoop; i++)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slotInday;
                            rd.DateBusy = puppet;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = slot;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                            //if (smallcheckpoint >= 6) break;
                            int motto = 1;
                            for (int j = smallcheckpoint; j < weeklyRepeated.Length; j++)
                            {
                                smallcheckpoint = j + 1;
                                if (weeklyRepeated[j]) break;
                                else if (!weeklyRepeated[j]) motto++;
                            }
                            //if (i == 1) throw new Exception(motto.ToString());
                            puppet = puppet.AddDays(motto);
                            slot++;
                        }
                    }
                    //like usual we start to make thing like nothing happend except we need to calculate the remain slot
                    //perfectly balance as it should be
                    int KhiNaoToiGapEm = 1;
                    while (dayofweek != batdaututhumay)
                    {
                        dayofweek++;
                        if (dayofweek == 7) dayofweek = 0;
                        if (dayofweek == batdaututhumay) break;
                        KhiNaoToiGapEm++;
                    }
                    DateTime TheDayAfterProcess = StartDate.AddDays(KhiNaoToiGapEm);
                    //throw new Exception(KhiNaoToiGapEm.ToString());
                    int totalrepeat = 0;
                    foreach (bool huhu in weeklyRepeated)
                    {
                        if (huhu) totalrepeat++;
                    }
                    int SlotAfterProcess = slotRequire - PreSlotBeforeLoop;
                    int repeatingmakesense = SlotAfterProcess / totalrepeat;
                    int remainSlot = SlotAfterProcess - (repeatingmakesense * totalrepeat);
                    //Note chia lấy dư sẽ có nhiều trường hợp debug hơn nên là...
                    DateTime firstTime = TheDayAfterProcess;
                    int jackpot = 1;
                    int checkpoint = 0;
                    int HoldThisForMeWillYou = slot;
                    int ForRemainSlot = 0;
                    //throw new Exception(slot.ToString());
                    for (int i = 0; i < totalrepeat; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail rd = new RoomDetail();
                                rd.RoomID = roomid;
                                rd.TimeTableID = tt.TimeTableID;
                                rd.SlotCurrentDay = slotInday;
                                rd.DateBusy = firstTime;
                                rd.AttendanceAction = "No Action yet";
                                rd.SlotTotal = slot;
                                context.RoomDetails.Add(rd);
                                context.SaveChanges();
                                firstTime = firstTime.AddDays(7 * seperatedWeek);
                                slot += totalrepeat;
                            }
                            dayofweek++;
                            //throw new Exception(dayofweek.ToString());
                            for (int yeheboi = dayofweek; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            if (checkpoint >= 7) checkpoint = 0;
                            HoldThisForMeWillYou++;
                            //throw new Exception(jackpot.ToString() + " _ " + checkpoint.ToString() + " _ " + totalrepeat.ToString());
                            //if (dayofweek >= 7) dayofweek = 0;
                        }
                        else if (dayofweek != batdaututhumay)
                        //bắt đầu từ vòng thứ 2 của total repeat nghĩa là dayofweek đã khác với batdaututhumay
                        {
                            slot = HoldThisForMeWillYou;
                            DateTime ForABetterDay = TheDayAfterProcess.AddDays(jackpot);
                            for (int j = 0; j < repeatingmakesense; j++)//surely beginining
                            {
                                RoomDetail rd = new RoomDetail();
                                rd.RoomID = roomid;
                                rd.TimeTableID = tt.TimeTableID;
                                rd.SlotCurrentDay = slotInday;
                                rd.DateBusy = ForABetterDay;
                                rd.AttendanceAction = "No Action yet";
                                rd.SlotTotal = slot;
                                context.RoomDetails.Add(rd);
                                context.SaveChanges();
                                ForABetterDay = ForABetterDay.AddDays(7 * seperatedWeek);
                                slot += totalrepeat;
                            }
                            for (int yeheboi = checkpoint; yeheboi < weeklyRepeated.Length; yeheboi++)
                            {
                                checkpoint = yeheboi + 1;
                                if (weeklyRepeated[yeheboi]) break;
                                else if (!weeklyRepeated[yeheboi]) jackpot++;
                            }
                            jackpot++;
                            if (checkpoint >= 7) checkpoint = 0;
                            HoldThisForMeWillYou++;
                            ForRemainSlot = slot - totalrepeat;
                            slot = HoldThisForMeWillYou;
                            //if(i==3) throw new Exception(checkpoint.ToString());
                        }
                    }// am fucking done with this shit

                    //final check and blow up, solving the remain slot
                    //throw new Exception(ForRemainSlot.ToString());
                    int KhiNaoEmGapToi = repeatingmakesense * 7 * seperatedWeek;
                    dayofweek = batdaututhumay;
                    DateTime wohoo = TheDayAfterProcess.AddDays(KhiNaoEmGapToi);
                    ForRemainSlot++;
                    for (int i = 0; i < remainSlot; i++)
                    {
                        if (dayofweek == batdaututhumay)
                        {
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slotInday;
                            rd.DateBusy = wohoo;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = ForRemainSlot;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                            dayofweek++;
                            ForRemainSlot++;
                        }
                        else if (dayofweek != batdaututhumay)
                        {
                            KhiNaoEmGapToi = 1;
                            for (int hihi = dayofweek;
                                hihi < weeklyRepeated.Length; hihi++) //now we use the old technic
                            {
                                dayofweek = hihi + 1;
                                if (weeklyRepeated[hihi]) break;
                                else if (!weeklyRepeated[hihi]) KhiNaoEmGapToi++;
                            }
                            wohoo = wohoo.AddDays(KhiNaoEmGapToi);
                            RoomDetail rd = new RoomDetail();
                            rd.RoomID = roomid;
                            rd.TimeTableID = tt.TimeTableID;
                            rd.SlotCurrentDay = slotInday;
                            rd.DateBusy = wohoo;
                            rd.AttendanceAction = "No Action yet";
                            rd.SlotTotal = ForRemainSlot;
                            context.RoomDetails.Add(rd);
                            context.SaveChanges();
                            ForRemainSlot++;
                        }
                    }//end final loop for the remain slot
                }//finishing the big else if block checking input day with start of the week day
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateRepeatedMonth(string teacherid, string classid, int roomid,
            DateTime StartDate, int repeatSeperatedMonth, int slotInday, int slotRequire)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable tt = new TimeTable();
                tt.ClassID = classid;
                tt.AccountId = teacherid;
                tt.DateStart = StartDate;
                tt.DateCreated = DateTime.Now;
                context.TimeTables.Add(tt);
                context.SaveChanges();
                for (int i = 1; i <= slotRequire; i++)
                {
                    RoomDetail rd = new RoomDetail();
                    rd.RoomID = roomid;
                    rd.TimeTableID = tt.TimeTableID;
                    rd.SlotCurrentDay = slotInday;
                    rd.DateBusy = StartDate;
                    rd.AttendanceAction = "No Action yet";
                    rd.SlotTotal = i;
                    context.RoomDetails.Add(rd);
                    context.SaveChanges();
                    StartDate = StartDate.AddMonths(repeatSeperatedMonth);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /************ END TIMETABLE CREATION ***********/

        /*********** START TIMETABLE MODIFY *********/
        public bool CheckingNewTimeTable(DateTime newdate, int newslot, int roomid)
        {
            try
            {
                using var context = new FAMContext();
                RoomDetail? rd = context.RoomDetails.FirstOrDefault(a => a.DateBusy.Date == newdate.Date &&
                a.SlotCurrentDay == newslot && a.RoomID == roomid);
                if (rd != null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CheckingNewTimeTable(DateTime newdate, int newslot, string teacherid)
        {
            try
            {
                using var context = new FAMContext();
                RoomDetail? rd = context.RoomDetails.FirstOrDefault(a => a.DateBusy.Date == newdate.Date &&
                a.SlotCurrentDay == newslot);
                if (rd != null)
                {
                    string teach = context.TimeTables.Single(a => a.TimeTableID == rd.TimeTableID).AccountId;
                    if (teach == teacherid) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ModifyTimeTable(int roomdetailid, DateTime newdate, int newslot)
        {
            try
            {
                using var context = new FAMContext();
                RoomDetail rd = context.RoomDetails.Single(a => a.RoomDetailID == roomdetailid);
                rd.DateBusy = newdate;
                rd.SlotCurrentDay = newslot;
                context.RoomDetails.Update(rd);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /*********** END TIMETABLE MODIFY **********/

        /********** START REGISTER WITH TIMETABLE LOGIC PROCESSING **********/
        public bool IsThatClassHaveTimetable(string classid)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable? ez = context.TimeTables.FirstOrDefault(a => a.ClassID == classid);
                if (ez == null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public bool 

        public void AddingStudentIntoTimetable(string studentid, string classid)
        {
            try
            {
                using var context = new FAMContext();
                int timetableid = context.TimeTables.SingleOrDefault(a => a.ClassID == classid).TimeTableID;
                var query = from cc in context.RoomDetails
                            where cc.TimeTableID == timetableid
                            select cc;
                int userclassid = context.UserClasses.First(a => a.ClassID == classid && a.AccountId == studentid).Id;
                List<RoomDetail> roomdetailsssssssssss = query.ToList();
                foreach (RoomDetail rd in roomdetailsssssssssss)
                {
                    Attendance ez = new Attendance();
                    ez.AccountId = studentid;
                    ez.SlotTotal = rd.SlotTotal;
                    ez.Status = "No Action Yet";
                    ez.RoomDetailID = rd.RoomDetailID;
                    ez.UserClassID = userclassid;
                    context.Attendances.Add(ez);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /*
        public void UpdateStudentIntoNewTimetable(string studentid, string classid, int userclassid)
        {
            try
            {
                using var context = new FAMContext();
                int timetableid = context.TimeTables.SingleOrDefault(a => a.ClassID == classid).TimeTableID;
                var query = from cc in context.RoomDetails
                            where cc.TimeTableID == timetableid
                            select cc;
                List<RoomDetail> roomdetailsssssssssss = query.ToList();
                int slot = 1;
                foreach (RoomDetail rd in roomdetailsssssssssss)
                {
                    Attendance ez = new Attendance();
                    ez.AccountId = studentid;
                    ez.SlotTotal = slot;
                    ez.Status = "No Action Yet";
                    ez.RoomDetailID = rd.RoomDetailID;
                    ez.UserClassID = userclassid;
                    context.Attendances.Add(ez);
                    context.SaveChanges();
                    slot++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }*/

        public bool IsThereAnyStudentRegisteredWithoutTimetable(string classid)
        {
            try
            {
                using var context = new FAMContext();
                UserClass? uc = context.UserClasses.FirstOrDefault(a => a.ClassID == classid);
                if (uc == null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddingStudentIntoTimetableAfterCreateTimetable(string classid)
        {
            try
            {
                using var context = new FAMContext();
                var query = from cc in context.UserClasses
                            where cc.ClassID == classid
                            select cc;
                List<UserClass> complex = query.ToList();
                //xử lí 30 slot và roomdetailid
                int timetableid = context.TimeTables.SingleOrDefault(a => a.ClassID == classid).TimeTableID;
                var secondquery = from cc in context.RoomDetails
                                  where cc.TimeTableID == timetableid
                                  select cc;
                List<RoomDetail> roomdetailsssssssssss = secondquery.ToList();
                //thuật toán O(n^2) :D 
                foreach (UserClass uc in complex)
                {
                    foreach (RoomDetail rd in roomdetailsssssssssss)
                    {
                        Attendance ez = new Attendance();
                        ez.AccountId = uc.AccountId; //studentid
                        ez.SlotTotal = rd.SlotTotal;
                        ez.Status = "No Action Yet";
                        ez.RoomDetailID = rd.RoomDetailID;
                        ez.UserClassID = uc.Id;
                        context.Attendances.Add(ez);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** END REGISTER WITH TIMETABLE LOGIC PROCESSING **********/

        /********** START VIEW TIMETABLE *********/
        public IEnumerable<Attendance> GetAttendancesFromStudentID(string studentid, IEnumerable<int> busylist)
        {
            List<Attendance> attendances = new List<Attendance>();
            try
            {
                using var context = new FAMContext();
                foreach (int roomdetailid in busylist)
                {
                    Attendance? ez = context.Attendances
                        .Include(a => a.RoomDetail.TimeTable.Class.Subject)
                        .Include(a => a.RoomDetail.Room)
                        .SingleOrDefault(c => c.RoomDetailID == roomdetailid
                        && c.AccountId == studentid); //only one and only with 1 studentid with 1 roomdetailid
                    if (ez != null) attendances.Add(ez);
                }
                return attendances;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<int> GetTimetableIDsFromTeacherID(string teacherid, IEnumerable<RoomDetail> busylist)
        {
            List<int> tts = new List<int>();
            try
            {
                using var context = new FAMContext();
                foreach (RoomDetail rd in busylist)
                {
                    TimeTable? tt = context.TimeTables.FirstOrDefault(a => a.TimeTableID == rd.TimeTableID
                        && teacherid == a.AccountId);
                    if (tt != null) tts.Add(tt.TimeTableID);
                }

                return tts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<int> GetRoomDetailIDsWithSpecificWeek(DateTime inputtedDate)
        {
            List<int> rdidlist = new List<int>();
            try
            {
                DateTime startweek = StartOfAWeek(inputtedDate);
                DateTime endweek = EndOfAWeek(inputtedDate);
                using var context = new FAMContext();
                var query = from cc in context.RoomDetails
                            where startweek <= cc.DateBusy && endweek >= cc.DateBusy
                            select cc.RoomDetailID;
                //lấy ra những roomdetail nằm từ thứ 2 đến chủ nhật.
                rdidlist = query.ToList();
                return rdidlist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<RoomDetail> GetRoomdetailsWithSpecificWeek(DateTime inputtedDate)
        {
            List<RoomDetail> rdidlist = new List<RoomDetail>();
            try
            {
                DateTime startweek = StartOfAWeek(inputtedDate);
                DateTime endweek = EndOfAWeek(inputtedDate);
                using var context = new FAMContext();
                /*
                var query = from cc in context.RoomDetails
                            where startweek <= cc.DateBusy && endweek >= cc.DateBusy
                            select cc;
                */
                var query = context.RoomDetails.Where(a => a.DateBusy >= startweek && a.DateBusy <= endweek)
                                                .Include(a => a.TimeTable.Class.Subject)
                                                .Include(a => a.Room);
                //lấy ra những roomdetail nằm từ thứ 2 đến chủ nhật.
                rdidlist = query.ToList();
                return rdidlist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** END VIEW TIMETABLE *********/

        /********** START PRIVATE FUNCTION *****/
        // thuật toán bởi thằng loser tên NamTH
        private DateTime StartOfAWeek(DateTime date)//return monday no matter what happend
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-6);
                case 1: return date;
                case 2: return date.AddDays(-1);
                case 3: return date.AddDays(-2);
                case 4: return date.AddDays(-3);
                case 5: return date.AddDays(-4);
                case 6: return date.AddDays(-5);
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime EndOfAWeek(DateTime date)//return sunday no matter what happend
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date;
                case 1: return date.AddDays(6);
                case 2: return date.AddDays(5);
                case 3: return date.AddDays(4);
                case 4: return date.AddDays(3);
                case 5: return date.AddDays(2);
                case 6: return date.AddDays(1);
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime ConvertToTuesday(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-5).Date;
                case 1: return date.AddDays(1).Date;
                case 2: return date.Date;
                case 3: return date.AddDays(-1).Date;
                case 4: return date.AddDays(-2).Date;
                case 5: return date.AddDays(-3).Date;
                case 6: return date.AddDays(-4).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime ItIsWednesdayMyDudeAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-4).Date;
                case 1: return date.AddDays(2).Date;
                case 2: return date.AddDays(1).Date;
                case 3: return date.Date;
                case 4: return date.AddDays(-1).Date;
                case 5: return date.AddDays(-2).Date;
                case 6: return date.AddDays(-3).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime ConvertToThurstyDay(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-3).Date;
                case 1: return date.AddDays(3).Date;
                case 2: return date.AddDays(2).Date;
                case 3: return date.AddDays(1).Date;
                case 4: return date.Date;
                case 5: return date.AddDays(-1).Date;
                case 6: return date.AddDays(-2).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime LastFridayNight(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-2).Date;
                case 1: return date.AddDays(4).Date;
                case 2: return date.AddDays(3).Date;
                case 3: return date.AddDays(2).Date;
                case 4: return date.AddDays(1).Date;
                case 5: return date.Date;
                case 6: return date.AddDays(-1).Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }

        private DateTime SaturdayNightIsAlright(DateTime date)
        {
            int dayname = (int)date.DayOfWeek;
            switch (dayname)
            {
                case 0: return date.AddDays(-1).Date;
                case 1: return date.AddDays(5).Date;
                case 2: return date.AddDays(4).Date;
                case 3: return date.AddDays(3).Date;
                case 4: return date.AddDays(2).Date;
                case 5: return date.AddDays(1).Date;
                case 6: return date.Date;
                default: throw new Exception("C# bị ngu đó mọi người");
            }
        }
        /********** END PRIVATE FUNCTION *******/
    }
}
