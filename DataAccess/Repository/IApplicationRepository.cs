using BusinessObj.Models;

namespace DataAccess.Repository
{
    public interface IApplicationRepository
    {
        List<Application> GetApplications(string SearchText);
        List<Application> GetApplicationsStudent(string SearchText, string accountID);
        Application GetApplication(int id);
        Application Create(Application application, string accountID);
        Application Edit(Application application);
        Application Delete(Application application);
        Application DeleteV2(Application application);
    }
}
