using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISubjectRepository
    {
        List<Subject> GetItems(string SearchText);
        Subject GetSubject(string SubjectID);
       
       // List<Subject> Search(string SearchText);
        Subject Create(Subject subject);
        Subject Edit(Subject subject);
        Subject Delete(Subject subject);
        bool checkSubjectIsEmpty(string subID);
    }
}
