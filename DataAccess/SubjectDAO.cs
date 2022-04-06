using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SubjectDAO
    {
        private static SubjectDAO instance = null;
        private static readonly object instanceLock = new object();
        public SubjectDAO() { }
        public static SubjectDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SubjectDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Subject> GetList()
        {
            var subjects = new List<Subject>();
            using (var db = new FAMContext())
            {
                subjects = db.Subjects.ToList();
            }
            return subjects;
        }
        public List<Subject> GetList(string SearchText)
        {
            List<Subject> subjects;
            using (var db = new FAMContext())
            {
                if (SearchText != null)
                {
                    subjects = db.Subjects.Where(m => m.SubjectID.Contains(SearchText) || m.SubjectName.Contains(SearchText))
                        .Include(m => m.Major)
                        .ToList();

                }
                else
                {
                    subjects = db.Subjects
                        .Include(m => m.Major)
                        .ToList();
                }

            }
            return subjects;
        }
        public Subject GetSubjectID(string subjectID)
        {
            Subject subject = null;
            using (var db = new FAMContext())
            {
                subject = db.Subjects.Where(m => m.SubjectID == subjectID).FirstOrDefault();
            }
            return subject;
        }
        public Subject CreateSubject(Subject subject)
        {
            Subject check = GetSubjectID(subject.SubjectID);
            if (check == null)
            {
                using (var db = new FAMContext())
                {
                    db.Subjects.Add(subject);
                    db.SaveChanges();
                    return subject;
                }
            }
            else
            {
                throw new Exception("Subject already exist!");
            }
        }
        public Subject EditSubject(Subject subject)
        {
            Subject check = GetSubjectID(subject.SubjectID);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.Subjects.Attach(subject);
                    db.Entry(subject).State = EntityState.Modified;
                    db.SaveChanges();
                    return subject;
                }
            }
            else
            {
                throw new Exception("Subject not exist!");
            }
        }
        public Subject DeleteSubject(Subject subject)
        {
            Subject check = GetSubjectID(subject.SubjectID);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.Subjects.Attach(subject);
                    db.Entry(subject).State = EntityState.Deleted;
                    db.SaveChanges();
                    //subject.Status = "Unavailable";
                    //db.Subjects.Attach(subject);
                    //db.Entry(subject).State = EntityState.Modified;
                    //db.SaveChanges();
                    return subject;
                }
            }
            else
            {
                throw new Exception("Subject not exist!");
            }
        }
    }
}