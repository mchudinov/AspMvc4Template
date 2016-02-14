using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public interface IUserRepository
    {
        IList<User> GetAllUsers();

        IList<User> GetUsers(string filter);
        
        User GetUser(IFormattable id);

        Task SaveUser(User user);

        Task DeleteUser(IFormattable id);
    }
}
