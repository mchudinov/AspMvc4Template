using System;
using System.Collections.Generic;
using Common;
using Models;
using Repositories;

namespace UseCases
{
    [Log]
    [LogException]
    public class UserCase : IUserCase
    {
        private readonly IUserRepository _repo;

        public UserCase(IUserRepository repo)
        {
            _repo = repo;
        }

        public IList<User> GetUsers()
        {
            return _repo.GetUsers();
        }

        public User GetUser(Guid id)
        {
            return _repo.GetUser(id);
        }

        public IList<User> GetUsers(string searchString)
        {
            return _repo.GetUsers(searchString);
        }

        public Guid CreateUser(string nickname, string email)
        {
            var user = new User()
            {
                Nickname = nickname,
                Email = email
            };

            _repo.SaveUser(user);
            return user.Id;
        }

        public void UpdateUser(User user)
        {
            _repo.SaveUser(user);
        }

        public void DeleteUser(Guid id)
        {
            _repo.DeleteUser(id);
        }
    }
}
