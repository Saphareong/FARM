using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class MajorDAO
    {
        private static MajorDAO instance = null;
        private static readonly object instanceLock = new object();
        public MajorDAO() { }
        public static MajorDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MajorDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Major> GetList(string SearchText)
        {
            List<Major> majors;
            using (var db = new FAMContext())
            {
                if(SearchText != null)
                {
                    majors = db.Majors.Where(m => m.MajorCode.Contains(SearchText)).ToList();
                }
                else
                {
                    majors = db.Majors.ToList();
                }
            }

            return majors;
        }
        public Major GetMajorCode(string majorCode)
        {
            Major major = null;
            using (var db = new FAMContext())
            {
                major = db.Majors.Where(m => m.MajorCode == majorCode).FirstOrDefault();
            }
            return major;
        }
        public Major CreateMajor(Major major)
        {
            Major check = GetMajorCode(major.MajorCode);
            if (check == null)
            {
                using (var db = new FAMContext())
                {
                    db.Majors.Add(major);
                    db.SaveChanges();
                    return major;
                }
            }
            else
            {
                throw new Exception("Major already exist!");
            }
        }
        public Major EditMajor(Major major)
        {
            Major check = GetMajorCode(major.MajorCode);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.Majors.Attach(major);
                    db.Entry(major).State = EntityState.Modified;
                    db.SaveChanges();
                    return major;
                }
            }
            else
            {
                throw new Exception("User not exist!");
            }
        }
        public Major DeleteMajor(Major major)
        {
            Major check = GetMajorCode(major.MajorCode);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.Majors.Attach(major);
                    db.Entry(major).State = EntityState.Deleted;
                    db.SaveChanges();
                    return major;
                }
            }
            else
            {
                throw new Exception("User not exist!");
            }
        }
    }
}