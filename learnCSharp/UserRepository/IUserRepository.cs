using learnCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp.UserRepository
{
    public interface IUserRepository
    {
        Task<User> NewUser(User user);
        Task DeleteUser(int ID);
        Task<User> GetUser(int ID);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByName(String name);
        Task UpdateUser(User user);
        Task Initialize();
    }
}
