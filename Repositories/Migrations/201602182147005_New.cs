namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "User");
            RenameTable(name: "dbo.Widgets", newName: "Widget");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Widget", newName: "Widgets");
            RenameTable(name: "dbo.User", newName: "Users");
        }
    }
}
