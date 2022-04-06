using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        public UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public List<User> GetList(string searchText)
        {
            List<User> users;
            using (var db = new FAMContext())
            {
                if (searchText != null)
                {

                    users = db.Users.Where(m => m.Ower.Contains(searchText) || m.Email.Contains(searchText))
                        .Include(m => m.Role)
                        .ToList();
                }
                else
                {
                    users = db.Users
                        .Include(m => m.Role)
                        .ToList();
                }
            }
            return users;
        }
        public User GetUserID(string AccountId)
        {
            User user = null;
            using (var db = new FAMContext())
            {
                user = db.Users.Where(m => m.AccountId == AccountId).FirstOrDefault();
            }
            return user;
        }

        public List<User> searchUser(string searchText)
        {
            var user = new List<User>();
            using(var db = new FAMContext())
            {
                user = db.Users.Where(m => m.Email.Contains(searchText) || m.Ower.Contains(searchText)).ToList();
            }
            return user;
        }

        public User CreateUser(User user)
        {
            User check = GetUserID(user.AccountId);
            if (check == null)
            {
                using (var db = new FAMContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return user;
                }
            }
            else
            {
                throw new Exception("User already exist!");
            }
        }
        public User EditUser(User user)
        {
            User check = GetUserID(user.AccountId);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.Users.Attach(user);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return user;
                }
            }
            else
            {
                throw new Exception("User not exist!");
            }
        }
        public User DeleteUser(User user)
        {
            User check = GetUserID(user.AccountId);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    db.Users.Attach(user);
                    db.Entry(user).State = EntityState.Deleted;
                    db.SaveChanges();
                    return user;
                }
            }
            else
            {
                throw new Exception("User not exist!");
            }
        }
        public User DeleteUserV2(User user)
        {
            User check = GetUserID(user.AccountId);
            if(check != null)
            {
                using (var db = new FAMContext())
                {
                    user.Status = "OFF";
                    db.Users.Attach(user);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return user;
                }
            }
            else
            {
                throw new Exception("User not exits!");
            }
        }
        public User DeleteUserV3(User user)
        {
            User check = GetUserID(user.AccountId);
            if (check != null)
            {
                using (var db = new FAMContext())
                {
                    user.Status = "ON";
                    db.Users.Attach(user);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return user;
                }
            }
            else
            {
                throw new Exception("User not exits!");
            }
        }
        public bool IsItemExists(string Id)
        {
            using (var db = new FAMContext())
            {
                int ct = db.Users.Where(u => u.AccountId.ToLower() == Id.ToLower()).Count();
                if (ct > 0)
                    return true;
                else
                    return false;
            }
        }

        public User CheckLogin(string username, string password)
        {
            User user = null;
            using (var context = new FAMContext())
            {
                //user = context.Users.SingleOrDefault(u => u.AccountName == username && u.Password == password);
                user = context.Users.FirstOrDefault(u => u.AccountName == username && u.Password == password);
                //using first or default because the database is very weird. You can't even know the constraint.
            }
            return user;
        }//end of function
    }
}
