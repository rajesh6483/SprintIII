using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Models;
using ProjectManagement.Interfaces;

namespace ProjectManagement.DataAccessLayer
{
    public class UserDAO
    {
        private readonly ProjectManagementDBContext _context;
        private int _count;

        public UserDAO(ProjectManagementDBContext context)
        {
            _context = context;
            _count = _context.Users.Count() ;
        }
        public User Create(User user)
        {
            user.Id = ++_count;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public IList<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Update(User user)
        {
            if (Delete(user.Id))
            {
                Create(user);
                _context.SaveChanges();
                return user;
            }
            throw new Exception($"No Tasjk Exists with Id {user.Id}");
        }

        public bool Delete(int userId)
        {
            var userToDelete = _context.Users.Where(user => user.Id == userId).FirstOrDefault();
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool IsUserExists(int userId)
        {
            return _context.Users.Any(user => user.Id == userId);
        }
    }
}
