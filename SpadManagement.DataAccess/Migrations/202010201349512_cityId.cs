namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cityId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "City_Id", "dbo.GeoDivisions");
            DropIndex("dbo.Customers", new[] { "City_Id" });
            RenameColumn(table: "dbo.Customers", name: "City_Id", newName: "CityId");
            AlterColumn("dbo.Customers", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "CityId");
            AddForeignKey("dbo.Customers", "CityId", "dbo.GeoDivisions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CityId", "dbo.GeoDivisions");
            DropIndex("dbo.Customers", new[] { "CityId" });
            AlterColumn("dbo.Customers", "CityId", c => c.Int());
            RenameColumn(table: "dbo.Customers", name: "CityId", newName: "City_Id");
            CreateIndex("dbo.Customers", "City_Id");
            AddForeignKey("dbo.Customers", "City_Id", "dbo.GeoDivisions", "Id");
        }
    }
}
