using BusinessObj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IUserRepository
    {
        List<User> GetList(string searchText);
        //List<User> SearchUser(string searchText);
        User GetUserID(string AccId);
        User Create(User user);
        User Edit(User user);
        User Delete(User user);

        User DeleteV2(User user);

        User DeleteV3(User user);
        User CheckLogin(string username, string password);
        bool IsItemExists(string Id);
    }
}
