using BusinessObj.Models;
using DataAccess.Repository;
using FAM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FAM.Controllers
{
    public class MajorController : Controller
    {
        public IMajorRepository majorRepo = new MajorRepository();
        public IMajorRegisterRepository registerRepo = new MajorRegisterRepository();

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
                        ViewBag.SearchText = SearchText;

                        List<Major> majors = majorRepo.GetItems(SearchText);

                        PaginatedList<Major> reMajors = new PaginatedList<Major>(majors, pg, pageSize);

                        var pager = new PagerModel(reMajors.TotalRecords, pg, pageSize);
                        this.ViewBag.Pager = pager;

                        return View("~/Views/Admin/admin_major_list.cshtml", reMajors);
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
                    if (loginuser.RoleId == "AD")
                    {
                        Major major = new Major();
                        return View("~/Views/Admin/admin_major_create.cshtml", major);
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Major major)
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
                            major = majorRepo.Create(major);
                        }
                        catch(Exception ex)
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

        public IActionResult Details(string majorCode)
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
                        Major major = majorRepo.GetMajor(majorCode);
                        return View("~/Views/Admin/admin_major_create.cshtml", major);
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        public IActionResult Edit(string id)
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
                        var major = majorRepo.GetMajor(id);
                        if (major == null)
                        {
                            return NotFound();
                        }
                        return View("~/Views/Admin/admin_major_edit.cshtml", major);
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Major major)
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
                            major = majorRepo.Edit(major);
                        }
                        catch(Exception ex)
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

        public IActionResult Delete(string id)
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
                        var major = majorRepo.GetMajor(id);
                        if (major == null)
                        {
                            return NotFound();
                        }
                        return View("~/Views/Admin/admin_major_delete.cshtml",major);
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(Major major)
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
                            if (registerRepo.IsItemExists(major.MajorCode) != true)
                            {
                                major = majorRepo.Delete(major);
                            }
                            else
                            {
                                throw new Exception("Hi there");
                            }
                        }
                        catch(Exception ex)
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
    }
}
