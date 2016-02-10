using System.Data.Entity;
using Models;

namespace Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDb") { }

        public DbSet<Widget> Widgets { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
