using Models;

namespace Repositories.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Repositories.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Repositories.AppDbContext context)
        {
            context.Users.AddOrUpdate(new User { Nickname = "Admin", Email = "admin@chudinov.net"}, new User { Nickname = "Mikey", Email = "mikael@chudinov.net" });
        }
    }
}
