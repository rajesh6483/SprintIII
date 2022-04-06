using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.DataAccessLayer;
using ProjectManagement.Models;
using ProjectManagement.Interfaces;


namespace ProjectManagement.BusinessLayer
{
    public class UserBO :IUser
    {
        private readonly UserDAO _dao;

        public UserBO(UserDAO userDAO)
        {
            _dao = userDAO;
        }
        public User Create(User user)
        {
           return _dao.Create(user);
        }

        public IList<User> GetAllUsers()
        {
            return _dao.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _dao.GetAllUsers().FirstOrDefault(x => x.Id == id);
        }

        public User Update(User user)
        {
            return _dao.Update(user);
        }

        public bool IsUserExists(int userId)
        {
            return _dao.IsUserExists(userId);
        }

    }
}
