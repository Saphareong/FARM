using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ClassRegisterRepository : IClassRegisterRepository
    {
        public bool checkClassIsEmpty(string ClassID) => SubjectRegisterDAO.Instance.checkClassIsEmpty(ClassID);

        public bool checkSubjectIsEmpty(string subID) => SubjectRegisterDAO.Instance.checkSubjectIsEmpty(subID);

        public void ConformClass(UserClass res) => SubjectRegisterDAO.Instance.ConformClass(res);

        public UserClass Create(UserClass res) => SubjectRegisterDAO.Instance.CreateRegisterClass(res);

        public UserClass Edit(UserClass res) => SubjectRegisterDAO.Instance.EditRegisterClass(res);

        public UserClass GetAccountId(string accountId) => SubjectRegisterDAO.Instance.GetAccountId(accountId);

        public UserClass GetId(int id) => SubjectRegisterDAO.Instance.GetId(id);

        public List<UserClass> GetInformationOfStudent(string id) => SubjectRegisterDAO.Instance.GetInformationOfStudent(id);

        public List<UserClass> GetListClass(string SearchText) => SubjectRegisterDAO.Instance.GetList(SearchText);
    }
}
