using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Models;

namespace ProjectManagement.Interfaces
{
     public interface IUser
    {
        IList<User> GetAllUsers();
        User GetUserById(int id);
        User Create(User user);
        User Update(User user);
        bool IsUserExists(int userId);
    }
}
