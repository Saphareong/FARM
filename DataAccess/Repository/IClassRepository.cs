using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IClassRepository
    {
        List<Class> GetAllClass();
        List<Class> GetClassesByName(String name);
        Class GetClassByName(String name);
        Class GetClassID(string id);
        Class CreateClass(Class addedClass);
        Class EditClass(Class addedClass);
        Class DeleteClass(Class addedClass);
        String validate(String name);
    }
}
