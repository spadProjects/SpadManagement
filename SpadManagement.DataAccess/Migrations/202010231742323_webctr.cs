namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class webctr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contracts", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Contracts", new[] { "AccountId" });
            CreateTable(
                "dbo.Contracts_WebsiteContract",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DomainId = c.String(nullable: false, maxLength: 300),
                        CustomerAddress = c.String(maxLength: 300),
                        CustomerPhone = c.String(maxLength: 300),
                        ExecuteDuration = c.Int(nullable: false),
                        DomainRegistrationCost = c.Long(nullable: false),
                        HostRegistrationCost = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.Contracts", "DomainId");
            DropColumn("dbo.Contracts", "CustomerAddress");
            DropColumn("dbo.Contracts", "CustomerPhone");
            DropColumn("dbo.Contracts", "ExecuteDuration");
            DropColumn("dbo.Contracts", "DomainRegistrationCost");
            DropColumn("dbo.Contracts", "HostRegistrationCost");
            DropColumn("dbo.Contracts", "AccountId");
            DropColumn("dbo.Contracts", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contracts", "Discriminator", c => c.String(maxLength: 128));
            AddColumn("dbo.Contracts", "AccountId", c => c.Int());
            AddColumn("dbo.Contracts", "HostRegistrationCost", c => c.Long());
            AddColumn("dbo.Contracts", "DomainRegistrationCost", c => c.Long());
            AddColumn("dbo.Contracts", "ExecuteDuration", c => c.Int());
            AddColumn("dbo.Contracts", "CustomerPhone", c => c.String(maxLength: 300));
            AddColumn("dbo.Contracts", "CustomerAddress", c => c.String(maxLength: 300));
            AddColumn("dbo.Contracts", "DomainId", c => c.String(maxLength: 300));
            DropForeignKey("dbo.Contracts_WebsiteContract", "Id", "dbo.Contracts");
            DropIndex("dbo.Contracts_WebsiteContract", new[] { "Id" });
            DropTable("dbo.Contracts_WebsiteContract");
            CreateIndex("dbo.Contracts", "AccountId");
            AddForeignKey("dbo.Contracts", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
    }
}
