using BusinessObj.Models;
using DataAccess.Repository;
using FAM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BusinessObj.DTOs;

namespace FAM.Controllers
{
    public class AttendanceController : Controller
    {
        public IAttendanceRepository attendanceRepo = new AttendanceRepository();

        private readonly ILogger<AttendanceController> _logger;
        public AttendanceController(ILogger<AttendanceController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string SearchText, int page = 1, int pageSize = 10)
        {
            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        //nếu là teacher
                        try
                        {
                            ViewBag.SearchText = SearchText;
                            List<Attendance> attendances = attendanceRepo.GetList(SearchText);
                            PaginatedList<Attendance> reAttendance =
                                new PaginatedList<Attendance>(attendances, page, pageSize);
                            var pager = new PagerModel(reAttendance.TotalRecords, page, pageSize);
                            this.ViewBag.Pager = pager;
                            return View("~/Views/Teacher/AttendanceIndex.cshtml", reAttendance);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        public IActionResult Check(int id)
        {
            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        //nếu là teacher
                        try
                        {
                            var attendance = attendanceRepo.GetUserID(id);
                            if (attendanceRepo == null)
                            {
                                return NotFound();
                            }

                            return View(attendance);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Check(Attendance attendance)
        {
            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        //nếu là teacher
                        try
                        {
                            attendance = attendanceRepo.CheckAtt1(attendanceRepo.GetUserID(attendance.AttendanceID));
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        public IActionResult ReCheck(int id)
        {
            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        //nếu là teacher
                        try
                        {
                            var attendance = attendanceRepo.GetUserID(id);
                            if (attendanceRepo == null)
                            {
                                return NotFound();
                            }

                            return View(attendance);
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }
        
        [HttpPost]
        public IActionResult ReCheck(Attendance attendance)
        {
            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        //nếu là teacher
                        try
                        {
                            attendance = attendanceRepo.CheckAtt2(attendanceRepo.GetUserID(attendance.AttendanceID));
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ViewClassInfo(int roomdetailid)
        {
            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        try
                        {
                            var data = attendanceRepo.getSpecificTimetableForTeacher(loginuser.AccountId, roomdetailid);
                            if (data != null) //nếu cứ bấm theo màn hình mà ko modify url thì kiểu gì cũng vào đây
                            {
                                ViewData["CLASSINFO"] = data;
                                return View("~/Views/Attendance/ClassInfo.cshtml");
                            }
                            else return NotFound();//trường hợp thay đổi url
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                            //return NotFound();
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        try
                        {
                            var data = attendanceRepo.getSpecificTimetableForStudent(loginuser.AccountId, roomdetailid);
                            if(data != null) //nếu cứ bấm theo màn hình mà ko modify url thì kiểu gì cũng vào đây
                            {
                                ViewData["CLASSINFO"] = data;
                                return View("~/Views/Attendance/ClassInfo.cshtml");
                            }
                            else return NotFound();//trường hợp thay đổi url
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                            //return NotFound();
                        }
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ViewStudentsClass(string classid, int roomdetailid)
        {
            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        try
                        {
                            if (attendanceRepo.CanThisUserTakeAttendance(loginuser.AccountId, classid))
                            {//giáo viên lớp này
                                ViewBag.ALLOW = "Turning red";
                                if (attendanceRepo.IsItTooLateToUpdateAttendance(roomdetailid))
                                {//quá trễ để edit r
                                    ViewBag.ALLOW = "So Sad";
                                }
                            }
                            var data = attendanceRepo.LoadStudentsFromClass(classid, roomdetailid);
                            if (data != null)
                                ViewData["CLASSSTUDENTLIST"] = data;
                            else ViewBag.Message = "Khum có học sinh nào trong lớp này hết";
                            TempData["CACHE_CLASSID"] = classid;
                            TempData["CACHE_RoomDetailId"] = roomdetailid;
                            return View("~/Views/Attendance/ClassStudentList.cshtml");
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                            //return NotFound();
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        try
                        {
                            var data = attendanceRepo.LoadStudentsFromClass(classid);
                            if (data != null)
                                ViewData["CLASSSTUDENTLIST"] = data;
                            else ViewBag.Message = "Khum có học sinh nào trong lớp này hết và nếu thế thật thì do bạn modify url";
                            return View("~/Views/Attendance/ClassStudentList.cshtml");
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                            //return NotFound();
                        }
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult TakeAttendance(string[] studentid, string[] attendance)
        {
            /*
            for(int i = 0; i < studentid.Length; i++)
            {
                _logger.LogInformation(studentid[i] + " _ " + attendance[i]);
            }
            return RedirectToAction("ViewStudentsClass", new { classid = TempData["CACHE_CLASSID"] });
            */

            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        try
                        {
                            string classid = (string)TempData["CACHE_CLASSID"];
                            int roomdetailid = (int)TempData["CACHE_RoomDetailId"];
                            if (attendanceRepo.CanThisUserTakeAttendance(
                                loginuser.AccountId, classid))
                            {
                                IEnumerable<ErrorAttendance> aduvip = attendanceRepo
                                    .TakeSeriousAttendance(classid, roomdetailid, studentid, attendance);
                                if (!aduvip.Any()) return RedirectToAction("ViewTimetable", "Timetable");
                                else
                                {
                                    TempData["ERRORATTENDANCE"] = aduvip;
                                    return RedirectToAction("ViewStudentsClass", new { classid = classid, roomdetailid = roomdetailid });
                                }
                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation(ex.Message);
                            return NotFound();
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ViewAttendanceReport()
        {
            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        IEnumerable<ViewSubjectAttendanceDTO> sup = attendanceRepo
                            .GetTeachingClasses(loginuser.AccountId);
                        if (sup.Any())
                        {
                            ViewData["CLASS"] = sup;
                            TempData["CLASS"] = JsonSerializer.Serialize(sup); ;
                        }   
                        else ViewBag.Message = "You currently don't have any classes";
                        return View("~/Views/Attendance/AttendanceReport.cshtml");
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        IEnumerable<ViewSubjectAttendanceDTO> sup = attendanceRepo
                            .GetRegisteredClassSubject(loginuser.AccountId);
                        if (sup.Any())
                        {
                            ViewData["CLASS"] = sup;
                            TempData["CLASS"] = JsonSerializer.Serialize(sup); ;
                        }
                        else ViewBag.Message = "You currently don't have any classes";
                        return View("~/Views/Attendance/AttendanceReport.cshtml");
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ViewAttendanceReportDetail(string classid)
        {
            string? loginjson = HttpContext.Session.GetString("LOGININFO");
            if (loginjson == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                User? loginuser = JsonSerializer.Deserialize<User>(loginjson);
                if (loginuser != null)
                {
                    if (loginuser.RoleId == "TE")
                    {
                        if(!attendanceRepo.IsThatClassHaveTimetable(classid))
                        {
                            TempData["Message"] = "This class doesn't have Timetable"; 
                            return RedirectToAction("ViewAttendanceReport");
                        }
                        IEnumerable<ViewSubjectAttendanceDTO>? sup = 
                            JsonSerializer.Deserialize<IEnumerable<ViewSubjectAttendanceDTO>>(TempData["CLASS"].ToString());
                        ViewData["CLASS"] = sup;
                        ViewData["ATTENDANCE"] = attendanceRepo.GetAttendanceClassForTeacher(loginuser.AccountId, classid);
                        return View("~/Views/Attendance/AttendanceReport.cshtml");
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        if (!attendanceRepo.IsThatClassHaveTimetable(classid))
                        {
                            ViewBag.Message = "This class doesn't have Timetable";
                            return RedirectToAction("ViewAttendanceReport");
                        }
                        IEnumerable<ViewSubjectAttendanceDTO>? sup =
                            JsonSerializer.Deserialize<IEnumerable<ViewSubjectAttendanceDTO>>(TempData["CLASS"].ToString());
                        ViewData["CLASS"] = sup;
                        ViewData["ATTENDANCE"] = attendanceRepo.GetAttendanceClassForStudent(loginuser.AccountId, classid);
                        return View("~/Views/Attendance/AttendanceReport.cshtml");
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }
    }
}
