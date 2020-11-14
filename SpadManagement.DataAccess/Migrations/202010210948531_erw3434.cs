namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class erw3434 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contracts", "City_Id", "dbo.GeoDivisions");
            DropIndex("dbo.Contracts", new[] { "City_Id" });
            DropColumn("dbo.Contracts", "City_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contracts", "City_Id", c => c.Int());
            CreateIndex("dbo.Contracts", "City_Id");
            AddForeignKey("dbo.Contracts", "City_Id", "dbo.GeoDivisions", "Id");
        }
    }
}
