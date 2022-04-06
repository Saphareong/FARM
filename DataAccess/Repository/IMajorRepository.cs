using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMajorRepository
    {
        List<Major> GetItems(string SearchText);
        //List<Major> Search(string SearchText);
        Major GetMajor(string majorCode);
        Major Create(Major major);
        Major Edit(Major major);
        Major Delete(Major major);
    }
}
