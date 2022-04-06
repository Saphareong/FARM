using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        public List<Subject> GetItems(string SearchText) => SubjectDAO.Instance.GetList(SearchText);
        public Subject GetSubject(string subjectID) => SubjectDAO.Instance.GetSubjectID(subjectID);
        //public List<Subject> Search(string SearchText) => SubjectDAO.Instance.Search(SearchText);
        public Subject Create(Subject subject) => SubjectDAO.Instance.CreateSubject(subject);
        public Subject Edit(Subject subject) => SubjectDAO.Instance.EditSubject(subject);
        public Subject Delete(Subject subject) => SubjectDAO.Instance.DeleteSubject(subject);
        public bool checkSubjectIsEmpty(string subjectID) => SubjectRegisterDAO.Instance.checkSubjectIsEmpty(subjectID);

        public IEnumerable<Class> GetSubjectCode(string subjectCode) => MajorRegisterDAO.Instance.GetClassFormSubject(subjectCode);
    }
}
