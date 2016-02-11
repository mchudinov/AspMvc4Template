using System;
using Repositories;

namespace UseCases
{
    public class User
    {
        private readonly IUserRepository _repo;

        public User(IUserRepository repo)
        {
            _repo = repo;
        }

        public Guid CreateUser(Models.User user)
        {
            _repo.SaveUser(user);
            return Guid.NewGuid();
        }
    }
}
