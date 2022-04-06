using BusinessObj.Models;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetList(string searchText) => UserDAO.Instance.GetList(searchText);

        //public List<User> SearchUser(string searchText) => UserDAO.Instance.searchUser(searchText);

        public User GetUserID(string AccId) => UserDAO.Instance.GetUserID(AccId);

        public User Create(User user) => UserDAO.Instance.CreateUser(user);

        public User Delete(User user) => UserDAO.Instance.DeleteUser(user);

        public User Edit(User user) => UserDAO.Instance.EditUser(user);

        public User DeleteV2(User user) => UserDAO.Instance.DeleteUserV2(user);

        public User DeleteV3(User user) => UserDAO.Instance.DeleteUserV3(user);
        public User CheckLogin(string username, string password) => UserDAO.Instance.CheckLogin(username, password);
        public bool IsItemExists(string Id) => UserDAO.Instance.IsItemExists(Id);

    }
}
