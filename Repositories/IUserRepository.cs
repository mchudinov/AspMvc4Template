using System;
using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface IUserRepository
    {
        IList<User> GetAllUsers();

        IList<User> GetUsers(string filter);
        
        User GetUser(Guid guid);

        void SaveUser(User user);
    }
}
