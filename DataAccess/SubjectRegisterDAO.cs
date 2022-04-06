using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SubjectRegisterDAO
    {
        private static SubjectRegisterDAO instance = null;
        private static readonly object instanceLock = new object();
        public SubjectRegisterDAO() { }
        public static SubjectRegisterDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SubjectRegisterDAO();
                    }
                    return instance;
                }
            }
        }

        public List<UserClass> GetList(string SearchText)
        {
            List<UserClass> res;
            using (var db = new FAMContext())
            {
                if (SearchText != "" && SearchText != null)
                {
                    res = db.UserClasses.Where(m => m.SubjectID.Contains(SearchText) || m.AccountId.Contains(SearchText) || m.ClassID.Contains(SearchText))
                        .Include(m => m.User)
                        .Include(m => m.Class)
                        .Include(m => m.Subject)
                        .ToList();
                }
                else
                {
                    res = db.UserClasses
                        .Include(m => m.User)
                        .Include(m => m.Class)
                        .Include(m => m.Subject)
                        .ToList();
                }
            }
            return res;
        }
        public List<UserClass> GetInformationOfStudent(string id)
        {
            using (var db = new FAMContext())
            {
                var list = from d in db.UserClasses
                           where d.AccountId == id
                           select d;
                return list.Include(d => d.Class)
                           .Include(d => d.Subject)
                           .Include(d => d.User)
                           .ToList();
            }
        }

        public bool checkSubjectIsEmpty(string subID)
        {
            using (var db = new FAMContext())
            {
                Subject? check = db.Subjects.FirstOrDefault(m => m.SubjectID == subID);
                if (check == null)
                    return true;
                else
                    return false;
            }
        }
        public bool checkClassIsEmpty(string ClassID)
        {
            using (var db = new FAMContext())
            {
                Class? check = db.Classes.FirstOrDefault(m => m.ClassID == ClassID);
                if (check == null)
                    return true;
                else
                    return false;
            }
        }


        public UserClass GetId(int id)
        {
            UserClass res = null;
            using (var db = new FAMContext())
            {
                res = db.UserClasses.Where(m => m.Id == id).FirstOrDefault();
            }
            return res;
        }
        public UserClass GetAccountId(string accountId)
        {
            UserClass res = null;
            using (var db = new FAMContext())
            {
                res = db.UserClasses.Where(m => m.AccountId == accountId).FirstOrDefault();
            }
            return res;
        }

        public UserClass CreateRegisterClass(UserClass res)
        {
            UserClass check = GetAccountId(res.AccountId);
            if (check == null)
            {
                using (var db = new FAMContext())
                {
                    db.UserClasses.Add(res);
                    db.SaveChanges();
                    return res;
                }
            }
            else
            {
                throw new Exception("Account already exist!");
            }
        }
        public UserClass EditRegisterClass(UserClass res)
        {
            UserClass check = GetAccountId(res.AccountId);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.UserClasses.Attach(res);
                    db.Entry(res).State = EntityState.Modified;
                    db.SaveChanges();
                    return res;
                }
            }
            else
            {
                throw new Exception("Account not exist!");
            }
        }

        public void ConformClass(UserClass res)
        {
            using (var db = new FAMContext())
            {
                db.UserClasses.Add(res);
                db.SaveChanges();
            }
        }
    }
}
