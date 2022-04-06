using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        public List<Application> GetApplications(string SearchText) => ApplicationDAO.Instance.GetApplications(SearchText);
        public List<Application> GetApplicationsStudent(string SearchText, string accountID) => ApplicationDAO.Instance.GetApplicationsStudent(SearchText, accountID);
        public Application GetApplication(int id) => ApplicationDAO.Instance.GetApplication(id);
        public Application Create(Application application, string accountID) => ApplicationDAO.Instance.CreateApplication(application, accountID);
        public Application Edit(Application application) => ApplicationDAO.Instance.EditApplication(application);
        public Application Delete(Application application) => ApplicationDAO.Instance.DeleteApplication(application);
        public Application DeleteV2(Application application) => ApplicationDAO.Instance.DeleteApplicationV2(application);
    }
}
