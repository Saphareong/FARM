using BusinessObj.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using FAM.Models;
using System.Text.Json;

namespace FAM.Controllers
{
    public class ScoreController : Controller
    {
        private IClassRepository classRepo = new ClassRepository();
        private IScoreRepository scoreRepo = new ScoreRepository();

        private double defaultScore = 0;

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
            ViewBag.SearchText = SearchText;

            List<Class> classes = new List<Class>();

            if (SearchText == null || SearchText.Equals(string.Empty))
                classes = classRepo.GetAllClass();
            else
                classes = classRepo.GetClassesByName(SearchText);

            PaginatedList<Class> reClasses = new PaginatedList<Class>(classes, pg, pageSize);

            var pager = new PagerModel(reClasses.TotalRecords, pg, pageSize);
            this.ViewBag.Pager = pager;

            return View(reClasses);
        }

        public IActionResult UpdateClassScores(string classID)
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
            if (classID == null) return RedirectToAction(nameof(Test));

            ViewBag.ClassCode = classID;

            var classScores = new List<Score>();

            var check = classRepo.GetClassID(classID);

            /*if (check != null) return RedirectToAction(nameof(Test));*/
            if (check == null) return RedirectToAction(nameof(Test));

            if (check != null) 
            {
                classScores = scoreRepo.GetScoresInClass(classID);
                return View(classScores);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult UpdateClassScores(List<Score> classScores)
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
            if (classScores == null || classScores.Count == 0) return NotFound();

            var classID = classScores.First().ClassID;            

            try
            {
                foreach (Score score in classScores)
                {
                    if (score.FE != defaultScore) return NotFound();

                    var result = scoreRepo.Edit(score);
                }

                return RedirectToAction(nameof(UpdateClassScores), new {classID = classID});
            }
            catch
            {
                return NotFound();
            }
            
        }

        public IActionResult Edit(int accountId)
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
            var scoreRecord = scoreRepo.GetScoresOfStudent(accountId.ToString());
            if (scoreRecord == null)
            {
                return NotFound();
            }
            return View(scoreRecord);
        }

        [HttpPost]
        public IActionResult Edit(Score scoreRecord)
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
                scoreRecord = scoreRepo.Edit(scoreRecord);
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Test()
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
            ViewBag.Message = "Can't find class!";
            return View();
        }
    }
}
