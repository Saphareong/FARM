using BusinessObj.Models;
using DataAccess.Repository;
using FAM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FAM.Controllers
{
    public class UserController : Controller
    {
        public IUserRepository userRepo = new UserRepository();

        public IActionResult Index(string SearchText, int pg = 1, int pageSize = 10)
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

                            List<User> users = userRepo.GetList(SearchText);

                            PaginatedList<User> reUser = new PaginatedList<User>(users, pg, pageSize);

                            var pager = new PagerModel(reUser.TotalRecords, pg, pageSize);
                            this.ViewBag.Pager = pager;

                            return View("~/Views/Admin/admin_account_list.cshtml",reUser);
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
                    if (loginuser.RoleId == "AD")
                    {
                        try
                        {
                            User user = new User();
                            return View("~/Views/Admin/admin_account_create.cshtml",user);
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
        public IActionResult Create(User user)
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
                            user = userRepo.Create(user);
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

        public IActionResult Details(string id)
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
                            User user = userRepo.GetUserID(id);
                            return View("~/Views/Admin/admin_account_viewone.cshtml", user);
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
                        try
                        {
                            var user = userRepo.GetUserID(id);
                            if (user == null)
                            {
                                return NotFound();
                            }
                            return View("~/Views/Admin/admin_account_edit.cshtml", user);
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
        public IActionResult Edit(User user)
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
                            user = userRepo.Edit(user);
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
                        try
                        {
                            var user = userRepo.GetUserID(id);
                            if (userRepo == null)
                            {
                                return NotFound();
                            }
                            return View("~/Views/Admin/admin_account_delete.cshtml", user);
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
        public IActionResult Delete(User user)
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
                            user = userRepo.DeleteV2(userRepo.GetUserID(user.AccountId));
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

        public IActionResult Active(string id)
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
                            var user = userRepo.GetUserID(id);
                            if (userRepo == null)
                            {
                                return NotFound();
                            }
                            return View("~/Views/Admin/admin_account_active.cshtml", user);
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
        public IActionResult Active(User user)
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
                            user = userRepo.DeleteV3(userRepo.GetUserID(user.AccountId));
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

        public IActionResult Change(string id)
        {
            TempData["mydata"] = id;
            return RedirectToAction("Create", "MajorRegister");
        }
    }
}
