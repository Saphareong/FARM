using Microsoft.AspNetCore.Mvc;
using DataAccess.Repository;
using BusinessObj.Models;
using System.Text.Json;

namespace FAM.Controllers
{
    public class StudentController : Controller
    {
        public IUserRepository userRepo = new UserRepository();
        public IApplicationRepository applicationRepo = new ApplicationRepository();

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
                        return View("~/Views/Student/Index.cshtml", loginuser);
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

        public ActionResult Profile()
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
                        return View("~/Views/Student/Profile.cshtml", loginuser);
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
                else return NotFound();
            }
            return NotFound();
        }
    }
}
