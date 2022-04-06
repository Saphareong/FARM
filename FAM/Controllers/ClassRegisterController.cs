using BusinessObj.Models;
using DataAccess.Repository;
using FAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace FAM.Controllers
{
    public class ClassRegisterController : Controller
    {
        public IClassRegisterRepository registerRepo = new ClassRegisterRepository();
        public IUserRepository userRepo = new UserRepository();
        //public IMajorRepository majorRepo = new MajorRepository();
        public IMajorRegisterRepository majorRepo = new MajorRegisterRepository();
        public IClassRepository classRepo = new ClassRepository();
        public ISubjectRepository subjectRepo = new SubjectRepository();
        private ITimetableRepository _timetableRepository = new TimetableRepository();
        private readonly ILogger<ClassRegisterController> _logger;
        public ClassRegisterController(ILogger<ClassRegisterController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index(string SearchText, int pg = 1, int pageSize = 5)
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
                    if (loginuser.RoleId == "AD")
                    {
                        try
                        {
                            ViewBag.SearchText = SearchText;

                            List<UserClass> registers = registerRepo.GetListClass(SearchText);

                            PaginatedList<UserClass> _registers = new PaginatedList<UserClass>(registers, pg, pageSize);

                            var pager = new PagerModel(_registers.TotalRecords, pg, pageSize);
                            this.ViewBag.Pager = pager;

                            return View(_registers);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    if (loginuser.RoleId == "ST")
                    {
                        try
                        {
                            List<UserClass> show = registerRepo.GetInformationOfStudent(loginuser.AccountId);
                            PaginatedList<UserClass> _showlst = new PaginatedList<UserClass>(show, pg, pageSize);

                            var pager = new PagerModel(_showlst.TotalRecords, pg, pageSize);
                            this.ViewBag.Pager = pager;
                            ViewData["classinfo"] = show;
                            foreach (UserClass user in show)
                            {
                                _logger.LogInformation(user.SubjectID + "+" + user.Subject.SubjectName + " - " + user.AccountId + " - " + user.Class.Name);
                                //ko nhiểu Tag Nam, Hoàng or Dũng sẽ support nha!
                            }
                            return View("~/Views/Student/ClassRegIndex.cshtml", _showlst);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }
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
                    if (loginuser.RoleId == "ST" || loginuser.RoleId == "AD")
                    {
                        try
                        {
                            var userMajor = majorRepo.GetAccountId(loginuser.AccountId);
                            ViewBag.AccountId = loginuser.AccountId;
                            UserClass register = new UserClass();
                            ViewBag.Subject = GetSubject(userMajor.MajorCode);
                            //ViewBag.Class = GetClass();
                            return View("~/Views/Student/ClassRegCreate.cshtml", register);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string SubjectID)
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
                    if (loginuser.RoleId == "ST" || loginuser.RoleId == "AD")
                    {
                        try
                        {
                            if (registerRepo.checkSubjectIsEmpty(SubjectID))
                            {
                                var userMajor = majorRepo.GetAccountId(loginuser.AccountId);
                                ViewBag.AccountId = loginuser.AccountId;
                                UserClass register = new UserClass();
                                ViewBag.Subject = GetSubject(userMajor.MajorCode);
                                //ViewBag.Class = GetClass();
                                TempData["SHOW"] = "Vui lòng chọn môn học!";
                                return View("~/Views/Student/ClassRegCreate.cshtml", register);
                            }
                            else
                            {
                                TempData["SUBJECTID"] = SubjectID;
                                ViewBag.SubjectV2 = SubjectID;
                                ViewBag.Class = GetClass(SubjectID);
                                return View("~/Views/Student/ClassRegConfirm.cshtml");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Conform(string ClassID)
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
                    if (loginuser.RoleId == "ST" || loginuser.RoleId == "AD")
                    {
                        string subjectid = (string)TempData["SUBJECTID"];
                        try
                        {
                            //_logger.LogInformation(subjectid);
                            //_logger.LogInformation("alo");
                            if (registerRepo.checkClassIsEmpty(ClassID))
                            {
                                ViewBag.SubjectV2 = subjectid;
                                ViewBag.Class = GetClass(subjectid);
                                ViewData["SHOWV2"] = "Vui lòng chọn lớp để học!";
                                return View("~/Views/Student/ClassRegConfirm.cshtml");
                            }
                            else
                            {
                                if(_timetableRepository.IsTheStudentRegisteredIntoThatClass(ClassID
                                    , loginuser.AccountId))//return true nghĩa là bị trùng lớp nên sẽ ngăn lại
                                {
                                    ViewBag.SubjectV2 = subjectid;
                                    ViewBag.Class = GetClass(subjectid);
                                    ViewData["SHOWV2"] = "Bạn đã đăng ký vào lớp này!";
                                    return View("~/Views/Student/ClassRegConfirm.cshtml");
                                }
                                if(_timetableRepository.IsItTooLateToRegisteringThisClass
                                    (ClassID))//return true nghĩa là tkb lớp này đã đi trước rùi hen
                                {
                                    ViewBag.SubjectV2 = subjectid;
                                    ViewBag.Class = GetClass(subjectid);
                                    ViewData["SHOWV2"] = "Lớp " + ClassID + " Đã khai giảng, Bạn vui lòng chọn lớp khác";
                                    return View("~/Views/Student/ClassRegConfirm.cshtml");
                                }
                                UserClass userClass = new UserClass();
                                userClass.SubjectID = subjectid;
                                userClass.ClassID = ClassID;
                                userClass.AccountId = loginuser.AccountId;
                                registerRepo.ConformClass(userClass);
                                _timetableRepository.AddingStudentIntoTimetable(ClassID, loginuser.AccountId);
                                TempData["AlertMessage"] = "Đăng kí môn học thành công.";
                                return RedirectToAction("Index");
                            }
                        }
                        catch (Exception ex)
                        {
                            if(ex.Message == "NOTASTUDENT")
                            {
                                ViewBag.SubjectV2 = subjectid;
                                ViewBag.Class = GetClass(subjectid);
                                ViewData["SHOWV2"] = "Bạn không phải là Sinh Viên!";
                                return View("~/Views/Student/ClassRegConfirm.cshtml");
                            }
                            if(ex.Message == "CLASSNOTEXITS")
                            {
                                ViewBag.SubjectV2 = subjectid;
                                ViewBag.Class = GetClass(subjectid);
                                ViewData["SHOWV2"] = "Lớp " + ClassID + " Không tồn tại!";
                                return View("~/Views/Student/ClassRegConfirm.cshtml");
                            }
                            //throw new Exception(ex.Message);
                            _logger.LogInformation(ex.Message);
                        }
                    }

                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            /*
            _logger.LogInformation(register.ClassID);
            bool set = false;
            string errMessage = "";
            try
            {
                if (userRepo.IsItemExists(register.AccountId) != true)
                {
                    errMessage = errMessage + "Account Id " + register.AccountId + " Not Exists";
                }
                if (errMessage == "")
                {
                    register = registerRepo.Create(register);
                    _timetableRepository.AddingStudentIntoTimetable(register.ClassID,
                        register.AccountId, register.Id);
                    set = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "PHILOGICVCL")
                {
                    errMessage = "Class doesn't exists or that wasn't a student. " +
                        "Don't blame me I just adding student into my timetable ((:";
                }
                else
                    errMessage = errMessage + " " + ex.Message;
            }*/
            return NotFound();
        }
        public IActionResult Edit(int id)
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
                    if (loginuser.RoleId == "AD")
                    {
                        try
                        {
                            UserClass? userClass = majorRepo.GetIdClass(id);
                            if (userClass != null)
                            {
                                ViewBag.SubjectID = userClass.SubjectID;
                                ViewBag.Class = GetClassV2(userClass.SubjectID, userClass.ClassID);
                                ViewBag.AccountId = userClass.AccountId;
                            }
                            return View();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(UserClass register)
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
                    if (loginuser.RoleId == "AD")
                    {
                        try
                        {
                            register = registerRepo.Edit(register);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        private List<SelectListItem> GetClass(string subjectCode)
        {
            var lstClass = new List<SelectListItem>();
            List<Class> classs = majorRepo.GetSubjectCode(subjectCode).ToList();
            lstClass = classs.Select(cl => new SelectListItem()
            {
                Value = cl.ClassID,
                Text = cl.Name
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "-Select-"
            };

            lstClass.Insert(0, defItem);
            return lstClass;
        }
        private List<SelectListItem> GetClassV2(string subjectCode, string ClassID)
        {
            var lstClass = new List<SelectListItem>();
            List<Class> classs = majorRepo.GetSubjectCode(subjectCode).ToList();
            Class Check = classs.FirstOrDefault(a => a.ClassID == ClassID);
            classs.Remove(Check);
            lstClass = classs.Select(cl => new SelectListItem()
            {
                Value = cl.ClassID,
                Text = cl.Name
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "Select Class"
            };

            lstClass.Insert(0, defItem);
            return lstClass;
        }
        private List<SelectListItem> GetSubject(string majorCode)
        {
            var lstSubject = new List<SelectListItem>();
            List<Subject> subjects = majorRepo.GetMajorCode(majorCode).ToList();
            lstSubject = subjects.Select(su => new SelectListItem()
            {
                Value = su.SubjectID,
                Text = su.SubjectName
            }).ToList();
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "-Select-"
            };

            lstSubject.Insert(0, defItem);
            return lstSubject;
        }
        //User có thể Create 2 môn trở lên và chư check trùng.
    }
}
