using BusinessObj.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using FAM.Models;
using System.Text.Json;

namespace FAM.Controllers
{
    public class SubjectsController : Controller
    {
        public ISubjectRepository subjectRepo = new SubjectRepository();
        
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
                    if (loginuser.RoleId == "TE")
                    {
                        //nếu là teacher
                        try
                        {
                            ViewBag.SearchText = SearchText;

                            List<Subject> subjects = subjectRepo.GetItems(SearchText);

                            PaginatedList<Subject> reSubjects = new PaginatedList<Subject>(subjects, pg, pageSize);

                            var pager = new PagerModel(reSubjects.TotalRecords, pg, pageSize);
                            this.ViewBag.Pager = pager;

                            return View(reSubjects);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    if (loginuser.RoleId == "ST")
                    {
                        //nếu là student
                        try
                        {
                            ViewBag.SearchText = SearchText;

                            List<Subject> subjects = subjectRepo.GetItems(SearchText);

                            PaginatedList<Subject> reSubjects = new PaginatedList<Subject>(subjects, pg, pageSize);

                            var pager = new PagerModel(reSubjects.TotalRecords, pg, pageSize);
                            this.ViewBag.Pager = pager;

                            return View("~/Views/Student/SubjectIndex.cshtml", reSubjects);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    if (loginuser.RoleId == "AD")
                    {
                        //nếu là admin
                        try
                        {
                            ViewBag.SearchText = SearchText;

                            List<Subject> subjects = subjectRepo.GetItems(SearchText);

                            PaginatedList<Subject> reSubjects = new PaginatedList<Subject>(subjects, pg, pageSize);

                            var pager = new PagerModel(reSubjects.TotalRecords, pg, pageSize);
                            this.ViewBag.Pager = pager;

                            return View("~/Views/Admin/admin_subject_list.cshtml", reSubjects);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                   /* if (loginuser.RoleId == "PA")
                    {
                        //nếu là admin
                    }*/
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
                        //nếu là admin
                        try
                        {
                            Subject subject = new Subject();
                        return View("~/Views/Admin/admin_subject_create.cshtml", subject);

                        }catch (Exception ex)
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
        public IActionResult Create(Subject subject)
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
                        try
                        //nếu là admintry
                        {
                            subject = subjectRepo.Create(subject);
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

        public IActionResult Details(string subjectCode)
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
                        Subject subject = subjectRepo.GetSubject(subjectCode);
                        return View("~/Views/Admin/admin_subject_list.cshtml", subject);

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
                        //nếu là admin
                        var subject = subjectRepo.GetSubject(id);
                        if (subject == null)
                        {
                            return NotFound();
                        }
                        return View("~/Views/Admin/admin_subject_edit.cshtml", subject);

                    }
                    
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }

            return NotFound();

        }

        [HttpPost]
        public IActionResult Edit(Subject subject)
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
                        try
                        {
                            subject = subjectRepo.Edit(subject);
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
                        //nếu là admin
                        var subject = subjectRepo.GetSubject(id);
                        if (subject == null)
                        {
                            return NotFound();
                        }
                        return View("~/Views/Admin/admin_subject_delete.cshtml",subject);

                    }
                   
                }
                else throw new Exception("HACKER-JUSTJOININ");
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(Subject subject)
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
                        try
                        {
                            if ( subjectRepo.checkSubjectIsEmpty(subject.SubjectID) != true)
                            {  
                                subject = subjectRepo.Delete(subject);
                            }
                            else { throw new Exception("Can't"); }
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
    }
}
