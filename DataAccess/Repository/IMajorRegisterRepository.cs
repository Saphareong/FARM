using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMajorRegisterRepository
    {
        List<UserMajor> GetItems(string SearchText);
        UserMajor GetId(int id);
        UserMajor GetAccountId(string accountId);

        UserClass GetIdClass(int Id);
        UserMajor Create(UserMajor res);
        UserMajor Edit(UserMajor res);
        bool IsItemExists(string majorCode);

        IEnumerable<Subject> GetMajorCode(string majorCode);

        IEnumerable<Class> GetSubjectCode(string subjectCode);
    }
}
