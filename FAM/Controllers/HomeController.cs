using Microsoft.AspNetCore.Mvc;
using DataAccess.Repository;
using BusinessObj.Models;
using System.Text.Json;
using System.Diagnostics;

namespace FAM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserRepository userRepository;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //dependency injection
            userRepository = new UserRepository();
        }

        public IActionResult Index()
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
                        if (loginuser.Status == "ON")
                        {
                            return View("~/Views/Teacher/Index.cshtml", loginuser);
                        }
                        else
                        {
                            return View("~/Views/Home/404.cshtml");
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                        if (loginuser.Status == "ON")
                        {
                            return View("~/Views/Student/Index.cshtml", loginuser);
                        }
                        else
                        {
                            return View("~/Views/Home/404.cshtml");
                        }
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin
                        //For FE (if admin view has been committed)
                        //return View("~/Views/Admin/Index.cshtml", loginuser);
                        //For BE testing
                        if (loginuser.Status == "ON")
                        {
                            return View("~/Views/Admin/admin_index.cshtml", loginuser);
                        }
                        else
                        {
                            return View("~/Views/Home/404.cshtml");
                        }
                    }
                    if (loginuser.RoleId == "PA")
                    {
                        //nếu là Parent
                        if (loginuser.Status == "ON")
                        {
                            return View("~/Views/Parent/Index.cshtml", loginuser);
                        }
                        else
                        {
                            return View("~/Views/Home/404.cshtml");
                        }
                    }
                }
                else return NotFound();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("LOGININFO") != null) return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            //_logger.LogInformation(username + " _ " + password);
            if (HttpContext.Session.GetString("LOGININFO") != null) return RedirectToAction("Index");
            if (ModelState.IsValid) //validate follow model
            {
                try
                {
                    User user = userRepository.CheckLogin(username, password);
                    //still not validate follow the requirement yet
                    if (user != null)
                    {
                        user.Password = null; //do not take the password
                        string logininfo = JsonSerializer.Serialize(user);
                        HttpContext.Session.SetString("LOGININFO", logininfo);
                        //adding more parameters for session.
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ERROR = "Username or password is not correct.";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                ViewBag.ERROR = "There is no way this message can carry to Client side because the model is allow null";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LOGININFO");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}