using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObj.Models;
using DataAccess.Repository;
using System.Text.Json;

namespace FAM.Controllers
{
    public class TeacherController : Controller
    {
        public IUserRepository userRepo = new UserRepository();
        // GET: TeacherController
        public ActionResult Index()
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

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return View();
        }

        // GET: TeacherController/Details/5
        public ActionResult DetailsTeacher()
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

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            string id = HttpContext.Session.GetString("LOGININFO");
            if(id != null)
            {
                User teach = JsonSerializer.Deserialize<User>(id);
                if (teach == null)
                {
                    return View("Error");
                }
                return View(teach);
            }
            return RedirectToAction("Login", "Home");
            
        }

        // GET: TeacherController/Create
        public ActionResult Create()
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

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Edit/5
        public ActionResult Edit(int id)
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

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return View();
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeacherController/Delete/5
        public ActionResult Delete(int id)
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

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return View();
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Profile()
        {
            string id = HttpContext.Session.GetString("LOGININFO");
            if (id != null)
            {
                User teacher = JsonSerializer.Deserialize<User>(id);
                if (teacher == null)
                {
                    return View("Error");
                }
                return View("~/Views/Teacher/Profile.cshtml");
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Classes()
        {
            string id = HttpContext.Session.GetString("LOGININFO");
            if (id != null)
            {
                User teacher = JsonSerializer.Deserialize<User>(id);
                if (teacher == null)
                {
                    return View("Error");
                }
                return View("~/Views/Teacher/Classes.cshtml");
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult subjectList()
        {
            string id = HttpContext.Session.GetString("LOGININFO");
            if (id != null)
            {
                User teacher = JsonSerializer.Deserialize<User>(id);
                if (teacher == null)
                {
                    return View("Error");
                }
                return View("~/Views/Teacher/SubjectList.cshtml");
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult subjectViewOne()
        {
            string id = HttpContext.Session.GetString("LOGININFO");
            if (id != null)
            {
                User teacher = JsonSerializer.Deserialize<User>(id);
                if (teacher == null)
                {
                    return View("Error");
                }
                return View("~/Views/Teacher/Subject.cshtml");
            }
            return RedirectToAction("Login", "Home");
        }

    }
}
