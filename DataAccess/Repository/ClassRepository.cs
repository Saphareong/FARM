using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ClassRepository : IClassRepository
    {
        public Class CreateClass(Class addedClass) => ClassDAO.Instance.CreateClass(addedClass);

        public List<Class> GetClassesByName(String name) => ClassDAO.Instance.GetClassesByName(name);

        public Class DeleteClass(Class addedClass) => ClassDAO.Instance.DeleteClass(addedClass);

        public Class EditClass(Class addedClass) => ClassDAO.Instance.EditClass(addedClass);

        public List<Class> GetAllClass() => ClassDAO.Instance.GetAllClass();

        public Class GetClassID(string id) => ClassDAO.Instance.GetClassID(id);

        public string validate(string name) => ClassDAO.Instance.validate(name);

        public Class GetClassByName(string name) => ClassDAO.Instance.GetClassByName(name);
    }
}
