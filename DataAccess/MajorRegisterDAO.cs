using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MajorRegisterDAO
    {
        private static MajorRegisterDAO instance = null;
        private static readonly object instanceLock = new object();
        public MajorRegisterDAO() { }
        public static MajorRegisterDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MajorRegisterDAO();
                    }
                    return instance;
                }
            }
        }
        public List<UserMajor> GetList(string SearchText)
        {
            List<UserMajor> res;
            using (var db = new FAMContext())
            {
                if (SearchText != "" && SearchText != null)
                {
                    res = db.UserMajors.Where(m => m.AccountId.Contains(SearchText))
                        .Include(m => m.Major)
                        .Include(u => u.User)
                        .ToList();
                }
                else
                {
                    res = db.UserMajors
                        .Include(m => m.Major)
                        .Include(u => u.User)
                        .ToList();
                }
            }
            return res;
        }
        public UserMajor GetId(int id)
        {
            UserMajor res = null;
            using (var db = new FAMContext())
            {
                res = db.UserMajors.FirstOrDefault(m => m.Id == id);
            }
            return res;
        }
        public UserMajor GetAccountId(string accountId)
        {
            UserMajor res = null;
            using (var db = new FAMContext())
            {
                res = db.UserMajors.FirstOrDefault(m => m.AccountId == accountId);
            }
            return res;
        }
        public UserMajor CreateRegisterMajor(UserMajor res)
        {
            UserMajor check = GetAccountId(res.AccountId);
            if (check == null)
            {
                using (var db = new FAMContext())
                {
                    db.UserMajors.Add(res);
                    db.SaveChanges();
                    return res;
                }
            }
            else
            {
                throw new Exception("Account already exist!");
            }
        }
        public UserMajor EditRegisterMajor(UserMajor res)
        {
            UserMajor check = GetAccountId(res.AccountId);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.UserMajors.Attach(res);
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
        public bool IsItemExists(string majorCode)
        {
            using (var db = new FAMContext())
            {
                int ct = db.UserMajors.Where(m => m.MajorCode.ToLower() == majorCode.ToLower()).Count();
                if (ct > 0)
                    return true;
                else
                    return false;
            }
        }
        public IEnumerable<Subject> GetSubjectFormMajorCode(string majorCode)
        {
            using (var db = new FAMContext())
            {
                var query = from su in db.Subjects
                            where su.MajorCode == majorCode
                            select su;
                return query.ToList();
            }
        }

        public IEnumerable<Class> GetClassFormSubject(string subjectCode)
        {
            using (var db = new FAMContext())
            { 
                var query = from cl in db.Classes
                            where cl.SubjectID == subjectCode
                            select cl;
                return query.ToList();
            }
        }
        public UserClass GetIdClass(int id)
        {
            UserClass res = null;
            using (var db = new FAMContext())
            {
                res = db.UserClasses.FirstOrDefault(m => m.Id == id);
            }
            return res;
        }
    }
}
