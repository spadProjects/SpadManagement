namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cityctr2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "City_Id", c => c.Int());
            CreateIndex("dbo.Contracts", "City_Id");
            AddForeignKey("dbo.Contracts", "City_Id", "dbo.GeoDivisions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "City_Id", "dbo.GeoDivisions");
            DropIndex("dbo.Contracts", new[] { "City_Id" });
            DropColumn("dbo.Contracts", "City_Id");
        }
    }
}
