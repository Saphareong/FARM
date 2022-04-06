using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    internal class ClassDAO
    {

        private static ClassDAO instance = null;
        private static readonly object instanceLock = new object();
        public ClassDAO() { }
        public static ClassDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ClassDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Class> GetAllClass()
        {
            var list = new List<Class>();
            using (var db = new FAMContext())
            {
                list = db.Classes.Include(c => c.Subject).ToList();
            }
            //foreach (var c in list)
            //{
            //    c.Subject = SubjectDAO.Instance.GetSubjectID(c.SubjectID);
            //}
            return list;
        }

        public List<Class> GetClassesByName(String name)
        {
            if (name == null)
            {
                return null;
            }
            String nameUP = name.ToUpper();
            var list = new List<Class>();
            using (var db = new FAMContext())
            {
                list = db.Classes.Include(c => c.Subject).ToList();
            }
            List<Class> classByName = new List<Class>();
            foreach (var c in list)
            {
                if (c.Name.Contains(nameUP))
                {
                    classByName.Add(c);
                }

            }
            return classByName;
        }

        public Class GetClassByName(String name)
        {
            if (name == null)
            {
                return null;
            }
            String nameUP = name.ToUpper();
            Class classFound = null;
            using (var db = new FAMContext())
            {
                classFound = db.Classes.Where(m => m.Name == nameUP).FirstOrDefault();
            }
           
            return classFound;
        }
        public Class GetClassID(string ClassId)
        {
            Class classFound = null;
            using (var db = new FAMContext())
            {
                classFound = db.Classes.Where(m => m.ClassID == ClassId).Include(c => c.Subject).FirstOrDefault();
            }
            return classFound;
        }
        public Class CreateClass(Class addedClass)
        {
            if (addedClass.Name == null)
            {
                return null;
            }
            Class check = GetClassByName(addedClass.Name);
            if (check == null)
            {
                using (var db = new FAMContext())
                {
                    var list = db.Classes.ToList();
                    addedClass.ClassID = list.Count + 1 + "";
                    addedClass.Name = addedClass.Name.ToUpper();
                    //addedClass.Subject = SubjectDAO.Instance.GetSubjectID(addedClass.SubjectID);
                    db.Classes.Add(addedClass);
                    db.SaveChanges();
                    return addedClass;
                }
            }
            else
            {
                return null;
            }
        }
        public Class EditClass(Class addedClass)
        {
            Class check = GetClassID(addedClass.ClassID);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    addedClass.Name = addedClass.Name.ToUpper();
                    db.Classes.Attach(addedClass);
                    db.Entry(addedClass).State = EntityState.Modified;
                    db.SaveChanges();
                    return addedClass;
                }
            }
            else
            {
                throw new Exception("Class not exist!");
            }
        }
        public Class DeleteClass(Class addedClass)
        {
            Class check = GetClassID(addedClass.ClassID);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    //db.Classes.Attach(addedClass);
                    //db.Entry(addedClass).State = EntityState.Deleted;
                    //db.SaveChanges();
                    addedClass.Status = "Unavailable";
                    db.Classes.Attach(addedClass);
                    db.Entry(addedClass).State = EntityState.Modified;
                    db.SaveChanges();
                    return addedClass;
                }
            }
            else
            {
                throw new Exception("Class not exist!");
            }
        }

        public String validate(String inputName)
        {
            if (!(inputName.All(c => char.IsLetterOrDigit(c))))
            {
                return "Class name must have letter and digit!";
            }


            if (inputName.All(char.IsDigit))
            {
                return "Class name must have letter and digit!";
            }
            else if (inputName.All(char.IsLetter))
            {
                return "Class name must have letter and digit!";
            }

            return null;
        }


    }
}
