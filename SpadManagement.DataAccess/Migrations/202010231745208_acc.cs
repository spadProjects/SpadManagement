namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts_WebsiteContract", "AccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contracts_WebsiteContract", "AccountId");
            AddForeignKey("dbo.Contracts_WebsiteContract", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts_WebsiteContract", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Contracts_WebsiteContract", new[] { "AccountId" });
            DropColumn("dbo.Contracts_WebsiteContract", "AccountId");
        }
    }
}
