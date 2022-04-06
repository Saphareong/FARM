using Microsoft.AspNetCore.Mvc;
using FAM.Models;
using DataAccess.Repository;
using BusinessObj.DTOs;
using BusinessObj.Models;
using System.Text.Json;
//using Newtonsoft.Json;

namespace FAM.Controllers
{
    public class TimetableController : Controller
    {
        private readonly ILogger<TimetableController> _logger;
        private ITimetableRepository _timetableRepository;
        public TimetableController(ILogger<TimetableController> logger)
        {
            _logger = logger;      
            _timetableRepository = new TimetableRepository();
        }

        [HttpGet]
        public IActionResult Create()
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
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin
                        try
                        {
                            TimeTableDTO dto = new TimeTableDTO();
                            dto = _timetableRepository.LoadComboData();
                            ViewData["CHOOSEN"] = dto;
                            if (TempData["ERRORLIST"] != null)
                            {
                                IEnumerable<CreateTimetableError> hoho =
                                    JsonSerializer.Deserialize<IEnumerable<CreateTimetableError>>
                                    (TempData["ERRORLIST"].ToString());
                                ViewData["ERRORLIST"] = hoho;
                            }
                        }
                        catch (Exception ex)
                        {
                            //throw new Exception(ex.Message);
                            _logger.LogInformation(ex.Message);
                            return NotFound(); //handle later on rather than display exception
                        }
                        return View();
                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(TimetableModel input)
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
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        if (ModelState.IsValid)
                        {
                            //_logger.LogInformation("first stage");
                            int dayname = (int)input.DateStart.DayOfWeek;
                            int hmmm = DateTime.Compare(DateTime.Now, input.DateStart);
                            if(hmmm >= 0)
                            {
                                TempData["Message"] = "Cannot created because date start was before right now or today. " +
                                    "Caution: Nếu FE validate chỗ này thì message này sẽ ko cần và t sẽ trả notfound luôn";
                                return RedirectToAction("Create");
                            }
                            if (dayname != 1 && dayname != 2) return NotFound();
                            try
                            {
                                if (_timetableRepository.IsThatClassExists(input.ClassID))
                                {
                                    if (!_timetableRepository.IsThatTimetableExists(input.ClassID))
                                    {
                                        _logger.LogInformation("second stage");
                                        int slotrequire = _timetableRepository.GetSlotRequireFromClass(input.ClassID);
                                        var errorlist = _timetableRepository.IsThatTimeAvailable(input.RoomID,
                                            input.RepeatedSlot, input.DateStart, slotrequire);
                                        if (!errorlist.Any())
                                        {
                                            if (_timetableRepository.IsThatTeacherBusy(input.TeacherID, input.RepeatedSlot,
                                            input.DateStart, slotrequire))
                                            {
                                                _logger.LogInformation("final stage");
                                                _timetableRepository.InitializeTimetable(input.TeacherID, input.DateStart, slotrequire,
                                                input.RoomID, input.ClassID, input.RepeatedSlot);
                                                _timetableRepository.AddingStudentIntoTimetableAfterCreation(input.ClassID);
                                                TempData["Message"] = "Successfully created time table for " + input.ClassID;
                                                return RedirectToAction("TimetableList");
                                            }
                                            //_logger.LogInformation("third stage");
                                            else
                                            {
                                                TempData["Message"] = "Teacher is conflicted with timetable. Choose another teacher";
                                                return RedirectToAction("Create");
                                            }
                                        }
                                        else //in ra danh sách những tkb bị trùng
                                        {
                                            TempData["ERRORLIST"] = JsonSerializer.Serialize(errorlist);
                                            TempData["Message"] = "Time is conflicted check the list below to see if you can fix it";
                                            return RedirectToAction("Create");
                                        }
                                    }
                                    else TempData["Message"] = "Timetable For that class is already created.";
                                }
                                else return NotFound();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "SLOTPROBLEM")
                                {
                                    TempData["Message"] = "we cant repeated slot " + input.RepeatedSlot + "(if you input monday then it only accept 1 to 6)";
                                    return RedirectToAction("Create");
                                }
                                else if (ex.Message == "NOTATEACHER")
                                {
                                    TempData["Message"] = "How can you enter the wrong ID when am using " +
                                        "combo box ๏_๏ Oh you fix it with Developer mode";//this message will never delivered to client
                                }
                                //throw new Exception(ex.Message);
                                _logger.LogInformation(ex.Message);
                                return NotFound(); //handle later on rather than display exception
                            }
                            //return RedirectToAction("Create");
                        }
                        else return NotFound();

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else return NotFound();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult TimetableList()
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
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        try
                        {
                            if (_timetableRepository.IsThereAnyTimeTable())
                                ViewData["ZA_WARUDO"] = _timetableRepository.LoadTimeTableList();
                            else ViewBag.Message = "Database có gì đâu mà show trời :((";
                        }
                        catch (Exception ex)
                        {
                            //throw new Exception(ex.Message);
                            _logger.LogInformation(ex.Message);
                            return NotFound(); //handle later on rather than display exception
                        }
                        return View("TimetableList");
                    }
                    if (loginuser.RoleId == "PA")
                    {
                        
                    }
                }
                else return NotFound();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int timetableid)
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
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        try
                        {
                            if (timetableid == 0) return NotFound();
                            ViewData["SPECIFICTIMETABLE"] = _timetableRepository.LoadSpecificTimetable(timetableid);
                            TempData["TIMETABLEID_CLICKED"] = timetableid;
                        }
                        catch (Exception ex)
                        {
                            //throw new Exception(ex.Message);
                            _logger.LogInformation(ex.Message);
                            return NotFound(); //handle later on rather than display exception
                        }
                        return View("EditTimetable");
                    }
                    if (loginuser.RoleId == "PA")
                    {
                        
                    }
                }
                else return NotFound();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult EditTimetable(int roomdetailid)
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
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        try
                        {
                            if (roomdetailid == 0) return NotFound();
                            if (_timetableRepository.IsItTooLateToChangeTimetable(roomdetailid))
                            {
                                TempData["Message"] = "What happend in FA. Stay in FA";
                                return RedirectToAction("Edit", new { timetableid = TempData["TIMETABLEID_CLICKED"] });
                            }
                            else
                            {
                                EditTimeTableDTO dto = _timetableRepository.GetOldTimetable(roomdetailid);
                                ViewData["OLD_DATA"] = dto;
                                TempData["VERY_OLD_DATA"] = JsonSerializer.Serialize(dto);
                                ViewData["ROOM_LIST"] = _timetableRepository.GetRooms();
                                TempData["ROOMDETAILID_CLICKED"] = roomdetailid;
                            }
                        }
                        catch (Exception ex)
                        {
                            //throw new Exception(ex.Message);
                            _logger.LogInformation(ex.Message);
                            return NotFound(); //handle later on rather than display exception
                        }
                        return View("Edit");
                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult EditTimetable(EditTimeTableModel input)
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
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                //only change html can change date time constraint so we direct it to 404
                                if (DateTime.Compare(DateTime.Now.Date, input.NewDateBusy.Date) > 0) return NotFound();
                                if (TempData["VERY_OLD_DATA"] != null)
                                {
                                    EditTimeTableDTO olddata = JsonSerializer.Deserialize<EditTimeTableDTO>(TempData["VERY_OLD_DATA"].ToString());
                                    _logger.LogInformation(olddata.ClassName);
                                    int balancer = DateTime.Compare(olddata.OldDateBusy.Date, input.NewDateBusy.Date);
                                    if (balancer == 0 &&
                                        input.NewSlotCurrent == olddata.Oldslotcurrent &&
                                        input.RoomID == olddata.RoomID) //ko thay đổi gì so với data cũ
                                    {
                                        TempData["Message"] = "You didn't modify anything therefore the timetable still remain";
                                        return RedirectToAction("Edit", new { timetableid = TempData["TIMETABLEID_CLICKED"] });
                                    }
                                    else
                                    {
                                        int roomdetailid = (int)TempData["ROOMDETAILID_CLICKED"];
                                        if (_timetableRepository.CheckingNewTimeTable(input.NewDateBusy, input.NewSlotCurrent, input.RoomID))
                                        {
                                            if (_timetableRepository.CheckingNewTimeTable(input.NewDateBusy, input.NewSlotCurrent, olddata.TeacherID))
                                            {
                                                _timetableRepository.ModifyTimeTable(roomdetailid, input.NewDateBusy, input.NewSlotCurrent);
                                                TempData["Message"] = "New schedule has been updated for class " + olddata.ClassName
                                                + " Subject " + olddata.SubjectName;
                                                return RedirectToAction("Edit", new { timetableid = TempData["TIMETABLEID_CLICKED"] });
                                            }
                                            else
                                            {
                                                TempData["Message"] = "Conflicted " + olddata.TeacherName + " Cannot teach 2 timetable at a same time." +
                                                    " Please choose another time";
                                                return RedirectToAction("EditTimetable", new { roomdetailid = roomdetailid });
                                            }
                                        }
                                        else
                                        {
                                            TempData["Message"] = "Conflict with other timetable please choose another time, room";
                                            return RedirectToAction("EditTimetable", new { roomdetailid = roomdetailid });
                                        }
                                    }
                                }
                                else return NotFound();
                            }
                            catch (Exception ex)
                            {
                                //throw new Exception(ex.Message);
                                _logger.LogInformation(ex.Message);
                                return NotFound(); //handle later on rather than display exception
                            }
                        }
                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else return NotFound();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ViewTimetable()
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
                    ViewBag.Day = DateTime.Now.Day;
                    ViewBag.Month = DateTime.Now.Month;
                    ViewBag.Year = DateTime.Now.Year;
                    if (loginuser.RoleId == "TE")
                    {
                        _logger.LogInformation("teacher");
                        try
                        {
                            var amlazy = _timetableRepository
                                .ViewTimetableAsATeacher(loginuser.AccountId, DateTime.Now.Date);
                            foreach(ViewTimeTableDTO dto in amlazy)
                            {
                                _logger.LogInformation(dto.ClassName + " _ " + dto.SubjectName);
                            }
                            if (amlazy.Any())
                            {
                                ViewData["TIMETABLE"] = amlazy;
                            }
                            return View(new ViewTimetableModel(DateTime.Now));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "FREETIMEBABY")
                            {
                                ViewBag.Message = "Tuần này bạn không có lịch học.";
                                return View(new ViewTimetableModel(DateTime.Now));
                            }
                            throw new Exception(ex.Message);
                            //return NotFound();
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        _logger.LogInformation("student");
                        try
                        {
                            var amlazy = _timetableRepository
                                .ViewTimetableAsAStudent(loginuser.AccountId, DateTime.Now.Date).ToList();
                            if (amlazy.Any())
                            {
                                ViewData["TIMETABLE"] = amlazy;
                            }
                            return View("~/Views/Student/TimetableIndex.cshtml", new ViewTimetableModel(DateTime.Now));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "FREETIMEBABY")
                            {
                                ViewBag.Message = "Tuần này bạn không có lịch học.";
                                return View("~/Views/Student/TimetableIndex.cshtml", new ViewTimetableModel(DateTime.Now));
                            }
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
                        //nếu là parent
                    }
                }
                return View();
            }
        }

        [HttpPost]
        public IActionResult ViewTimetable(DateTime? dateinput)
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
                    DateTime IsItChoosed = dateinput ?? DateTime.Now.Date;
                    if (loginuser.RoleId == "TE")
                    {
                        try
                        {
                            ViewBag.Day = IsItChoosed.Day;
                            ViewBag.Month = IsItChoosed.Month;
                            ViewBag.Year = IsItChoosed.Year;
                            var amlazy = _timetableRepository.
                                ViewTimetableAsATeacher(loginuser.AccountId, IsItChoosed);
                            foreach(var wat in amlazy)
                            {
                                _logger.LogInformation(wat.ClassName + " _ " + wat.RoomName);
                            }
                            if (amlazy != null)
                            {
                                ViewData["TIMETABLE"] = amlazy;
                            }
                            return View(new ViewTimetableModel(IsItChoosed));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "FREETIMEBABY")
                            {
                                ViewBag.Message = "Tuần này bạn không có lịch học.";
                                return View(new ViewTimetableModel(IsItChoosed));
                            }
                            throw new Exception(ex.Message);
                            //return NotFound();
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        try
                        {
                            ViewBag.Day = IsItChoosed.Day;
                            ViewBag.Month = IsItChoosed.Month;
                            ViewBag.Year = IsItChoosed.Year;
                            var amlazy = _timetableRepository.
                                ViewTimetableAsAStudent(loginuser.AccountId, IsItChoosed);
                            if (amlazy != null)
                            {
                                ViewData["TIMETABLE"] = amlazy;
                            }
                            return View("~/Views/Student/TimetableIndex.cshtml", new ViewTimetableModel(IsItChoosed));
                        }
                        catch (Exception ex)
                        {
                            if(ex.Message == "FREETIMEBABY")
                            {
                                ViewBag.Message = "Tuần này bạn không có lịch học.";
                                return View("~/Views/Student/TimetableIndex.cshtml", new ViewTimetableModel(IsItChoosed));
                            }
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
                        //nếu là parent
                    }
                }
                return View();
            }
        }

        //[HttpGet]
        //public IActionResult LMAO()
        //{//ghi đường dẫn /Timetable/LMAO để chạy qua đây
        //    try
        //    {
        //        //LƯU Ý ĐỪNG ĐỂ CÁI TẤT CẢ PHẦN TỬ MẢNG FALSE VÌ TUI CHƯA VALIDATE VỤ ĐÓ
        //        //từ trái sang phải sunday, monday, tuesday, wednesday, thursday, friday, saturday
        //        bool[] gopro = { true, false, false, false, false, true, true };
        //        /*for(int i = 0; i < gopro.Length; i++)
        //        {
        //            _logger.LogInformation(gopro[i].ToString());
        //        }*/
        //        DateTime middleweek = new DateTime(2022, 4, 6);
        //        DateTime rightfuckingnow = DateTime.Now.Date;
        //        DateTime monday = new DateTime(2022, 4, 4);                 // <------- hoặc tự đổi giá trị ngày bên kia
        //        DateTime sunday = new DateTime(2022, 4, 10);                //thay đổi thời gian bắt đầu chỗ này
        //                                                                    //này nè
        //        _timetableRepository.CreateRepeatedWeek("10000101", "1", 1, middleweek, gopro, 1, 34, 1);
        //        //tham số lần lượt teacherid, classid, roomid, Ngày bắt đầu, cái mảng tuần, slot 1-6
        //        //, số slot yêu cầu của môn, số tuần bị cách ra để lặp
        //        return RedirectToAction("ViewTimetable");
        //    }
        //    catch(Exception ex)
        //    {
        //        //throw new Exception(ex.Message);
        //        _logger.LogInformation(ex.Message);
        //        return RedirectToAction("ViewTimetable");
        //    }
        //}

        [HttpGet]
        public IActionResult CustomCreate()
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
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin
                        try
                        {
                            TimeTableDTO dto = new TimeTableDTO();
                            dto = _timetableRepository.LoadComboData();
                            ViewData["CHOOSEN"] = dto;
                            if (TempData["ERRORLIST"] != null)
                            {
                                IEnumerable<CreateTimetableError> hoho =
                                    JsonSerializer.Deserialize<IEnumerable<CreateTimetableError>>
                                    (TempData["ERRORLIST"].ToString());
                                ViewData["ERRORLIST"] = hoho;
                            }
                        }
                        catch (Exception ex)
                        {
                            //throw new Exception(ex.Message);
                            _logger.LogInformation(ex.Message);
                            return NotFound(); //handle later on rather than display exception
                        }
                        return View();
                    }
                    if (loginuser.RoleId == "PA")
                    {

                    }
                }
                else return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult CustomCreate(UpgradeTimetableModel input)
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
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        if (ModelState.IsValid)
                        {
                            //_logger.LogInformation("first stage");
                            int hmmm = DateTime.Compare(DateTime.Now, input.DateStart);
                            if (hmmm >= 0)
                            {
                                TempData["Message"] = "Cannot created because date start was before right now or today. " +
                                    "Caution: Nếu FE validate chỗ này thì message này sẽ ko cần và t sẽ trả notfound luôn";
                                return RedirectToAction("CustomCreate");
                            }
                            try
                            {
                                if (_timetableRepository.IsThatClassExists(input.ClassID))
                                {
                                    if (!_timetableRepository.IsThatTimetableExists(input.ClassID))
                                    {
                                        _logger.LogInformation("second stage");
                                        bool[] weekrepeating = new bool[7];
                                        if(input.RepeatType == 2)
                                        {
                                            weekrepeating[0] = input.Sunday;
                                            weekrepeating[1] = input.Monday;
                                            weekrepeating[2] = input.Tuesday;
                                            weekrepeating[3] = input.Wednesday;
                                            weekrepeating[4] = input.Thursday;
                                            weekrepeating[5] = input.Friday;
                                            weekrepeating[6] = input.Saturday;
                                            if (weekrepeating.All(a => a == false)) return NotFound();
                                        }
                                        int slotrequire = _timetableRepository.GetSlotRequireFromClass(input.ClassID);
                                        var errorlist = _timetableRepository.IsThatTimeAvailableAdvanced(input.RoomID,
                                                            weekrepeating, input.DateStart, input.RepeatType, 
                                                            input.RepeatSeperated, input.RepeatedSlot, slotrequire);
                                        if (!errorlist.Any())
                                        {
                                            if (_timetableRepository.IsThatTeacherBusyAdvanced(input.TeacherID, weekrepeating,
                                            input.DateStart, input.RepeatType, input.RepeatSeperated, slotrequire, input.RepeatedSlot))
                                            {
                                                _logger.LogInformation("final stage");
                                                _timetableRepository.InitializeTimetableAdvanced(input.TeacherID, 
                                                    input.RoomID, input.ClassID, slotrequire, input.DateStart, weekrepeating,
                                                     input.RepeatedSlot, input.RepeatSeperated, input.RepeatType);
                                                _timetableRepository.AddingStudentIntoTimetableAfterCreation(input.ClassID);
                                                TempData["Message"] = "Successfully created time table for " + input.ClassID;
                                                return RedirectToAction("TimetableList");
                                            }
                                            //_logger.LogInformation("third stage");
                                            else
                                            {
                                                TempData["Message"] = "Teacher is conflicted with timetable. Choose another teacher";
                                                return RedirectToAction("CustomCreate");
                                            }
                                        }
                                        else //in ra danh sách những tkb bị trùng
                                        {
                                            TempData["ERRORLIST"] = JsonSerializer.Serialize(errorlist);
                                            TempData["Message"] = "Time is conflicted check the list below to see if you can fix it";
                                            return RedirectToAction("CustomCreate");
                                        }
                                    }
                                    else TempData["Message"] = "Timetable For that class is already created.";
                                }
                                else return NotFound();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "SLOTPROBLEM")
                                {
                                    TempData["Message"] = "we cant repeated slot " + input.RepeatedSlot + "(if you input monday then it only accept 1 to 6)";
                                    return RedirectToAction("Create");
                                }
                                else if (ex.Message == "NOTATEACHER")
                                {
                                    TempData["Message"] = "How can you enter the wrong ID when am using " +
                                        "combo box ๏_๏ Oh you fix it with Developer mode";//this message will never delivered to client
                                }
                                //throw new Exception(ex.Message);
                                _logger.LogInformation(ex.Message);
                                return NotFound(); //handle later on rather than display exception
                            }
                            //return RedirectToAction("Create");
                        }
                        else return NotFound();

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else return NotFound();
            }
            return NotFound();
        }

        /*
        private bool CreateValidation(TimetableModel input)
        {
            bool checkpoint = false;
            checkpoint = input.RoomID > 0;
            int hmmm = DateTime.Compare(DateTime.Now, input.DateStart);
            checkpoint = hmmm < 0;
            checkpoint = input.RepeatedSlot <= 5 && input.RepeatedSlot >= 1;
            checkpoint = input.ClassID != null;
            checkpoint = input.TeacherID != null;
            return checkpoint;
        }

        private bool CustomCreateValidation(UpgradeTimetableModel input)
        {

        }*/
    }
}
