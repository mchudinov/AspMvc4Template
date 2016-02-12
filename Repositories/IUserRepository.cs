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
        
        User GetUser(Guid guid);

        Task SaveUser(User user);
    }
}
