using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IClassRegisterRepository
    {
        List<UserClass> GetListClass(string SearchText);

        UserClass GetId(int id);

        UserClass GetAccountId(string accountId);

        UserClass Create(UserClass res);

        UserClass Edit(UserClass res);

        void ConformClass(UserClass res);

        List<UserClass> GetInformationOfStudent(string id);

        bool checkSubjectIsEmpty(string subID);

        bool checkClassIsEmpty(string ClassID);

    }
}
