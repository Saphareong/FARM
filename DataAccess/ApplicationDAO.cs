using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDAO
    {
        private static ApplicationDAO instance = null;
        private static readonly object instanceLock = new object();
        public ApplicationDAO() { }
        public static ApplicationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ApplicationDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Application> GetApplications(string SearchText)
        {
            List<Application> applications;
            using (var db = new FAMContext())
            {
                if (SearchText != null)
                {
                    applications = db.Applications.Where(m => m.ApplicationContent.ToLower().Contains(SearchText.ToLower())).ToList();
                }
                else
                {
                    applications = db.Applications.ToList();
                }
            }

            return applications;
        }

        public List<Application> GetApplicationsStudent(string SearchText, string accountID)
        {
            List<Application> applications;
            using (var db = new FAMContext())
            {
                if (SearchText != null)
                {
                    applications = db.Applications.Where(m => m.ApplicationContent.ToLower().Contains(SearchText.ToLower()) && m.AccountId == accountID).ToList();
                }
                else
                {
                    applications = db.Applications.ToList();
                }
            }

            return applications;
        }

        public Application GetApplication(int id)
        {
            Application application = null;
            using (var db = new FAMContext())
            {
                application = db.Applications.Where(m => m.ApplicationID == id).FirstOrDefault();
            }
            return application;
        }
        public Application CreateApplication(Application application, string accountID)
        {
            Application check = GetApplication(application.ApplicationID);
            if (check == null)
            {
                using (var db = new FAMContext())
                {
                    application.ApplicationStatus = "Pending";
                    application.CreateDay = DateTime.Now;
                    application.AccountId = accountID;
                    db.Applications.Add(application);
                    db.SaveChanges();
                    return application;
                }
            }
            else
            {
                throw new Exception("Application already exist!");
            }
        }
        public Application EditApplication(Application application)
        {
            Application check = GetApplication(application.ApplicationID);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.Applications.Attach(application);
                    db.Entry(application).State = EntityState.Modified;
                    db.SaveChanges();
                    return application;
                }
            }
            else
            {
                throw new Exception("Application not exist!");
            }
        }
        public Application DeleteApplication(Application application)
        {
            Application check = GetApplication(application.ApplicationID);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.Applications.Attach(application);
                    db.Entry(application).State = EntityState.Deleted;
                    db.SaveChanges();
                    return application;
                }
            }
            else
            {
                throw new Exception("Application not exist!");
            }
        }
        public Application DeleteApplicationV2(Application application)
        {
            Application check = GetApplication(application.ApplicationID);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    application.ApplicationStatus = "Deleted";
                    db.Applications.Attach(application);
                    db.Entry(application).State = EntityState.Modified;
                    db.SaveChanges();
                    return application;
                }
            }
            else
            {
                throw new Exception("Application not exits!");
            }
        }
    }
}