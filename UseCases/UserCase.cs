using System;
using Models;
using Repositories;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;

namespace UseCases
{
    public class UserCase : IUserCase
    {
        private readonly IUserRepository _repo;

        public UserCase(IUserRepository repo)
        {
            _repo = repo;
        }

        [LogException]
        public IFormattable CreateUser(string nickname, string email)
        {
            var user = new User()
            {
                Nickname = nickname,
                Email = email
            };
            
            _repo.SaveUser(user);
            return user.Id;
        }
    }
}
