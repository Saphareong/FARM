using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MajorRepository : IMajorRepository
    {
        public List<Major> GetItems(string SearchText) => MajorDAO.Instance.GetList(SearchText);
        //public List<Major> Search(string SearchText) => MajorDAO.Instance.Search(SearchText);
        public Major GetMajor(string majorCode) => MajorDAO.Instance.GetMajorCode(majorCode);
        public Major Create(Major major) => MajorDAO.Instance.CreateMajor(major);
        public Major Edit(Major major) => MajorDAO.Instance.EditMajor(major);
        public Major Delete(Major major) => MajorDAO.Instance.DeleteMajor(major);
    }
}
