using BusinessObj.Models;
using DataAccess.Repository;
using FAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace FAM.Controllers
{
    public class MajorRegisterController : Controller
    {
        public IMajorRegisterRepository registerRepo = new MajorRegisterRepository();
        public IMajorRepository majorRepo = new MajorRepository();
        public IUserRepository userRepo = new UserRepository();

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

                        List<UserMajor> registers = registerRepo.GetItems(SearchText);

                        PaginatedList<UserMajor> _registers = new PaginatedList<UserMajor>(registers, pg, pageSize);

                        var pager = new PagerModel(_registers.TotalRecords, pg, pageSize);
                        this.ViewBag.Pager = pager;

                        return View(_registers);
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
                        UserMajor register = new UserMajor();
                        var accountId = TempData["mydata"];
                        ViewBag.AccountId = accountId;
                        ViewBag.Majors = GetMajors();
                        return View(register);
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserMajor register)
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
                                set = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            errMessage = errMessage + " " + ex.Message;
                        }

                        if (set == false)
                        {
                            ModelState.AddModelError("", errMessage);
                            ViewBag.Majors = GetMajors();
                            return View(register);
                        }
                        else
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
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
                        ViewBag.Majors = GetMajors();
                        var res = registerRepo.GetId(id);
                        if (res == null)
                        {
                            return NotFound();
                        }
                        return View(res);
                    }
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(UserMajor register)
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

        private List<SelectListItem> GetMajors()
        {
            var lstMajors = new List<SelectListItem>();
            List<Major> majors = majorRepo.GetItems("");
            lstMajors = majors.Select(ma => new SelectListItem()
            {
                Value = ma.MajorCode,
                Text = ma.MajorName
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "Select Majors"
            };

            lstMajors.Insert(0, defItem);
            return lstMajors;
        }
    }
}
