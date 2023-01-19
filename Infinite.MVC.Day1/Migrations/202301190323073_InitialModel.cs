namespace Infinite.MVC.Day1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Addresses", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "State", c => c.String());
        }
    }
}
