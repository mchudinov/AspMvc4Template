using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetAllUsers()
        {
            using (var db = new AppDbContext())
            {
                return db.Users;
            }
        }

        public User GetUser(Guid guid)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.First(u => u.Id == guid);
            }
        }

        public IEnumerable<User> GetUsers(string filter)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.Where(u => u.Nickname.ToLower().Contains(filter.ToLower()));
            }
        }

        public void SaveUser(User user)
        {
            using (var db = new AppDbContext())
            {
                db.Users.Add(user);
                db.SaveChangesAsync();
            }
        }
    }
}
