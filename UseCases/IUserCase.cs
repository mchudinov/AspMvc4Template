using System;
using System.Collections.Generic;
using Models;

namespace UseCases
{
    public interface IUserCase
    {
        IList<User> GetUsers();

        IList<User> GetUsers(string searchString);

        User GetUser(Guid id);

        Guid CreateUser(string nickaname, string email);

        void UpdateUser(User user);

        void DeleteUser(Guid id);
    }
}
