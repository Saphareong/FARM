using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MajorRegisterRepository : IMajorRegisterRepository
    {
        public UserMajor Create(UserMajor res) => MajorRegisterDAO.Instance.CreateRegisterMajor(res);
        public UserMajor Edit(UserMajor res) => MajorRegisterDAO.Instance.EditRegisterMajor(res);
        public UserMajor GetAccountId(string accountId) => MajorRegisterDAO.Instance.GetAccountId(accountId);
        public List<UserMajor> GetItems(string SearchText) => MajorRegisterDAO.Instance.GetList(SearchText);
        public UserMajor GetId(int id) => MajorRegisterDAO.Instance.GetId(id);
        public bool IsItemExists(string majorCode) => MajorRegisterDAO.Instance.IsItemExists(majorCode);

        public IEnumerable<Subject> GetMajorCode(string majorCode) => MajorRegisterDAO.Instance.GetSubjectFormMajorCode(majorCode);

        public IEnumerable<Class> GetSubjectCode(string subjectCode) => MajorRegisterDAO.Instance.GetClassFormSubject(subjectCode);

        public UserClass GetIdClass(int Id) => MajorRegisterDAO.Instance.GetIdClass(Id);
    }
}
