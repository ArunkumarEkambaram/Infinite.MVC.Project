namespace Infinite.MVC.Day1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ApplyUniqueToEmailId : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Users", "EmailId", true);
        }

        public override void Down()
        {
        }
    }
}
