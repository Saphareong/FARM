using BusinessObj.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using FAM.Models;
using System.Text.Json;

namespace FAM.Controllers
{
    public class ApplicationController : Controller
    {
        public IApplicationRepository applicationRepo = new ApplicationRepository();

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
                    if (loginuser.RoleId == "ST")
                    {
                        try
                        {
                            ViewBag.SearchText = SearchText;
                            List<Application> applications =
                                applicationRepo.GetApplicationsStudent(SearchText, loginuser.AccountId);
                            PaginatedList<Application> reApplications =
                                new PaginatedList<Application>(applications, pg, pageSize);
                            var pager = new PagerModel(reApplications.TotalRecords, pg, pageSize);
                            this.ViewBag.Pager = pager;
                            return View("~/Views/Student/ApplicationIndex.cshtml", reApplications);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                    if (loginuser.RoleId == "AD")
                    {
                        ViewBag.SearchText = SearchText;
                        List<Application> applications = applicationRepo.GetApplications(SearchText);
                        PaginatedList<Application> reApplications = new PaginatedList<Application>(applications, pg, pageSize);
                        var pager = new PagerModel(reApplications.TotalRecords, pg, pageSize);
                        this.ViewBag.Pager = pager;
                        return View("~/Views/Application/Index.cshtml", reApplications);

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
                        try
                        {
                            Application application = new Application();
                            return View("~/Views/Student/ApplicationCreate.cshtml", application);
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
        public IActionResult Create(Application application)
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
                    if (loginuser.RoleId == "ST")
                    {
                        try
                        {
                            application = applicationRepo.Create(application, loginuser.AccountId);
                            TempData["AlertMessage"] = "Application created successfully.";
                            return RedirectToAction(nameof(Index));
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

        public IActionResult Details(int id)
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
                        //nếu là admin

                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            Application application = applicationRepo.GetApplication(id);
            return View(application);
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
                        //nếu là admin

                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            var application = applicationRepo.GetApplication(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        [HttpPost]
        public IActionResult Edit(Application application)
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
                application = applicationRepo.Edit(application);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
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
            var application = applicationRepo.GetApplication(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        [HttpPost]
        public IActionResult Delete(Application application)
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
                application = applicationRepo.DeleteV2(applicationRepo.GetApplication(application.ApplicationID));
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
