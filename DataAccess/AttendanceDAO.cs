using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AttendanceDAO
    {
        private static AttendanceDAO instance = null;
        private static readonly object instanceLock = new object();
        public AttendanceDAO() { }
        public static AttendanceDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AttendanceDAO();
                    }
                    return instance;
                }
            }

        }
        public List<Attendance> GetList()
        {
            var attendances = new List<Attendance>();
            using (var db = new FAMContext())
            {
                attendances = db.Attendances.ToList();
            }
            return attendances;
        }

        public List<Attendance> GetList(string searchText)
        {
            List<Attendance> attendances;
            using (var db = new FAMContext())
            {
                if (searchText != null)
                {

                    attendances = db.Attendances.Where(m => m.AccountId.Contains(searchText)).ToList();
                }
                else
                {
                    attendances = db.Attendances.ToList();
                }
            }
            return attendances;
        }
        public Attendance GetUserID(int AttendanceID)
        {
            Attendance attendance = null;
            using (var db = new FAMContext())
            {
                attendance = db.Attendances.Where(m => m.AttendanceID == AttendanceID).FirstOrDefault();
            }
            return attendance;
        }

        
        
        
        public Attendance CheckAtt1(Attendance attendance)
        {
            Attendance check = GetUserID(attendance.AttendanceID);
            if(check != null)
            {
                using (var db = new FAMContext())
                {
                    attendance.Status = "NO";
                    db.Attendances.Attach(attendance);
                    db.Entry(attendance).State = EntityState.Modified;
                    db.SaveChanges();
                    return attendance;
                }
            }
            else
            {
                throw new Exception("User not exits!");
            }
        }
        public Attendance CheckAtt2(Attendance attendance)
        {
            Attendance check = GetUserID(attendance.AttendanceID);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    attendance.Status = "YES";
                    db.Attendances.Attach(attendance);
                    db.Entry(attendance).State = EntityState.Modified;
                    db.SaveChanges();
                    return attendance;
                }
            }
            else
            {
                throw new Exception("Attendance not exits!");
            }
        }

        public Attendance? GetAttendanceForDetailTimetable(string studentid, int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                return context.Attendances.Include(a => a.RoomDetail.TimeTable.Class.Subject)
                                        .Include(a => a.RoomDetail.TimeTable.User)
                                        .FirstOrDefault(a => a.AccountId == studentid && a.RoomDetailID == roomdetailid);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<UserClass> GetStudentInThatClass(string classid)
        {
            List<UserClass> students = new List<UserClass>();
            try
            {
                using var context = new FAMContext();
                var query = context.UserClasses.Where(a => a.ClassID == classid)
                                    .Include(a => a.User);
                students = query.ToList();
                return students;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RoomDetail? GetRoomDetailForDetailTimetable(int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                return context.RoomDetails.Include(a => a.TimeTable.Class.Subject)
                                            .Include(a => a.TimeTable.User)
                                .SingleOrDefault(a => a.RoomDetailID == roomdetailid); 
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CanThisUserTakeAttendance(string accountid, string classid)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable? tt = context.TimeTables.SingleOrDefault(a => a.ClassID == classid);
                if(tt != null)
                {
                    if (tt.AccountId == accountid) return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsThisStudentBelongInThisClass(string studentid, string classid)
        {
            try
            {
                using var context = new FAMContext();
                Attendance? att = context.Attendances//.Include(a => a.RoomDetail.TimeTable)
                                                .FirstOrDefault(a => a.AccountId == studentid
                                                && a.RoomDetail.TimeTable.ClassID == classid);
                if (att != null) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TakeAttendance(string studentid, int roomdetailid, string action, string classid)
        {
            try
            {
                using var context = new FAMContext();
                Attendance? att = context.Attendances//.Include(a => a.RoomDetail.TimeTable)
                    .FirstOrDefault(a => a.AccountId == studentid 
                    && a.RoomDetailID == roomdetailid && a.RoomDetail.TimeTable.ClassID == classid);
                if(att != null)
                {
                    if (action == "LessGo") att.Status = "Present";
                    else if (action == "AFK") att.Status = "Absent";
                    else return false;
                    context.Attendances.Update(att);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsItTooLateToUpdateAttendance(int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                RoomDetail rd = context.RoomDetails.Single(a => a.RoomDetailID == roomdetailid);
                if (rd.AttendanceAction == "No Action yet")
                    return false;
                if (rd.AttendanceAction == "Taken" || rd.AttendanceAction == "Attendance Edited")
                {
                    rd.DateBusy = rd.DateBusy.AddDays(2);
                    int wat = DateTime.Compare(DateTime.Now, rd.DateBusy);
                    if (wat >= 0) return false;
                    else return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SwitchLanes(int roomdetailid)
        {
            try
            {
                using var context = new FAMContext();
                RoomDetail rd = context.RoomDetails.Single(a => a.RoomDetailID == roomdetailid);
                if (rd.AttendanceAction == "No Action yet")
                {
                    rd.AttendanceAction = "Taken";
                    rd.TakeAttendanceTime = DateTime.Now;
                } 
                else if (rd.AttendanceAction == "Taken")
                    rd.AttendanceAction = "Attendance Edited";
                context.RoomDetails.Update(rd);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool GetAttendanceStatus(int roomdetailid, string accountid)
        {
            try
            {
                using var context = new FAMContext();
                Attendance? att = context.Attendances.FirstOrDefault
                    (a => a.AccountId == accountid && a.RoomDetailID == roomdetailid);
                if(att != null)
                {
                    if (att.Status == "Present") return true;
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<UserClass> GetUserClassesFromStudentid(string studentid)
        {
            List<UserClass> list = new List<UserClass>();
            try
            {
                using var context = new FAMContext();
                var query = context.UserClasses.Where(a => a.AccountId == studentid)
                                .Include(a => a.Subject)
                                .Include(a => a.Class);
                list = query.ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetDateStartFromClassid(string classid)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable? tt = context.TimeTables.SingleOrDefault(a => a.ClassID == classid);
                if (tt == null) return "Vẫn chưa khai giảng";
                else
                {
                    DateTime dt = tt.DateStart;
                    return dt.Day + "/" + dt.Month + "/" + dt.Year;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TimeTable> GetTimetableFromTeacherid(string teacherid)
        {
            List<TimeTable> list = new List<TimeTable>();
            try
            {
                using var context = new FAMContext();
                var query = context.TimeTables.Where(a => a.AccountId == teacherid)
                                .Include(a => a.Class.Subject);
                list = query.ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsThatClassHaveTimetable(string classid)
        {
            try
            {
                using var context = new FAMContext();
                TimeTable? tt = context.TimeTables.SingleOrDefault(a => a.ClassID == classid);
                if (tt == null) return false;
                else return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Attendance> GetAttendancesFromClassIdAndStudentid(string classid, string studentid)
        {
            //i hope i won't get notice by this low end performance coding because if it does then i'm screwed
            List<Attendance> list = new List<Attendance>();
            try
            {
                using var context = new FAMContext();
                var query = context.Attendances.Where(a => a.RoomDetail.TimeTable.ClassID == classid 
                                && a.AccountId == studentid)
                                .OrderBy(a => a.SlotTotal)
                                .Include(a => a.RoomDetail.TimeTable.Class)
                                .Include(a => a.RoomDetail.TimeTable.User);
                list = query.ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<RoomDetail> GetRoomDetailFromClassIdAndTeacherid(string classid, string teacherid)
        {
            //i hope i won't get notice by this low end performance coding because if it does then i'm screwed
            List<RoomDetail> list = new List<RoomDetail>();
            try
            {
                using var context = new FAMContext();
                var query = context.RoomDetails.Where(a => a.TimeTable.ClassID == classid
                                && a.TimeTable.AccountId == teacherid)
                                .OrderBy(a => a.SlotTotal)
                                .Include(a => a.TimeTable.Class)
                                .Include(a => a.TimeTable.User);
                list = query.ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
