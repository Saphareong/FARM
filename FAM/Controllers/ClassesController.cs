#nullable disable
using BusinessObj.Models;
using DataAccess;
using DataAccess.Repository;
using FAM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FAM.Controllers
{
    public class ClassesController : Controller
    {
        private IClassRepository dao = new ClassRepository();


        // GET: Classes
        public IActionResult Index(String name, int pg = 1, int pageSize = 5)
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
                            List<Class> Classes = dao.GetAllClass();

                            PaginatedList<Class> Classpaging = new PaginatedList<Class>(Classes, pg, pageSize);

                            var pager = new PagerModel(Classpaging.TotalRecords, pg, pageSize);
                            this.ViewBag.Pager = pager;

                            if (Classes == null)
                            {
                                return View();
                            }
                            if (name != null)
                            {
                                List<Class> ClassesByName = dao.GetClassesByName(name);
                                return View(ClassesByName);
                            }
                            return View("~/Views/Admin/admin_class_list.cshtml", Classes);
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

        // GET: Classes/Details/5
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
                            if (id == null)
                            {
                                return NotFound();
                            }


                            Class classFound = dao.GetClassID(id);


                            if (classFound == null)
                            {
                                return NotFound();
                            }

                            return View("~/Views/Admin/admin_class_viewone.cshtml", classFound);
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

        // GET: Classes/Create
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
                            //Lay subject
                            List<Subject> listsubject = SubjectDAO.Instance.GetList();
                            ViewBag.Subjects = listsubject;

                            return View("~/Views/Admin/admin_class_create.cshtml"); //truyen subject vao view
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

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ClassID,Name,Status,SubjectID")] Class @class)
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
                            List<Subject> listsubject = SubjectDAO.Instance.GetList();
                            ViewBag.Subjects = listsubject;
                            String message = dao.validate(@class.Name);
                            if (message != null)
                            {
                                ViewBag.message = message;
                                return View(@class);
                            }
                            Class checkNameClass = dao.GetClassByName(@class.Name);
                            if (checkNameClass != null)
                            {
                                ViewBag.message = "Class already exist";
                                return View(@class);
                            }
                            if (ModelState.IsValid)
                            {
                                Class addedClass = dao.CreateClass(@class);
                                if (addedClass == null)
                                {
                                    return View(@class);
                                }
                                return RedirectToAction(nameof(Index));
                            }
                            return View(@class);
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

        // GET: Classes/Edit/5
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
                            if (id == null)
                            {
                                return NotFound();
                            }

                            Class classFound = dao.GetClassID(id);
                            if (classFound == null)
                            {
                                return NotFound();
                            }
                            List<Subject> listsubject = SubjectDAO.Instance.GetList();
                            ViewBag.Subjects = listsubject;
                            return View("~/Views/Admin/admin_class_edit.cshtml", classFound);
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

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("ClassID,Name,Status,SubjectID")] Class @class)
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
                            List<Subject> listsubject = SubjectDAO.Instance.GetList();
                            ViewBag.Subjects = listsubject;
                            String message = dao.validate(@class.Name);
                            if (message != null)
                            {
                                ViewBag.message = message;
                                return View(@class);
                            }
                            if (id != @class.ClassID)
                            {
                                return NotFound();
                            }
                            Class checkNameClass = dao.GetClassByName(@class.Name);
                            if (checkNameClass != null)
                            {
                                ViewBag.message = "Class already exist";
                                return View(@class);
                            }

                            if (ModelState.IsValid)
                            {
                                Class classFound = dao.GetClassID(@class.ClassID);
                                Class check = dao.EditClass(@class);
                                if (check == null)
                                {
                                    View(@class);
                                }
                                return RedirectToAction(nameof(Index));
                            }
                            return View(@class);
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

        // GET: Classes/Delete/5
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
                            if (id == null)
                            {
                                return NotFound();
                            }

                            Class classFound = dao.GetClassID(id);
                            if (classFound == null)
                            {
                                return NotFound();
                            }

                            return View("~/Views/Admin/admin_class_delete.cshtml", classFound);
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

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
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
                            Class classFound = dao.DeleteClass(dao.GetClassID(id));
                            if (classFound == null)
                            {
                                return View();
                            }
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


    }
}
