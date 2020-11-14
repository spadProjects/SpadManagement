namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class websiteContract : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ContractPlans", newName: "InstagramContractPlans");
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNo = c.String(nullable: false, maxLength: 100),
                        ShebaNo = c.String(nullable: false, maxLength: 100),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebsiteContractItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsPrePayment = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Contract_Id = c.Int(),
                        WebsiteContract_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.Contract_Id)
                .ForeignKey("dbo.Contracts", t => t.WebsiteContract_Id)
                .Index(t => t.Contract_Id)
                .Index(t => t.WebsiteContract_Id);
            
            AddColumn("dbo.InstagramContractPlans", "InstagramContract_Id", c => c.Int());
            AddColumn("dbo.Contracts", "DomainId", c => c.String(maxLength: 300));
            AddColumn("dbo.Contracts", "CustomerAddress", c => c.String(maxLength: 300));
            AddColumn("dbo.Contracts", "CustomerPhone", c => c.String(maxLength: 300));
            AddColumn("dbo.Contracts", "ExecuteDuration", c => c.Int());
            AddColumn("dbo.Contracts", "DomainRegistrationCost", c => c.Long());
            AddColumn("dbo.Contracts", "HostRegistrationCost", c => c.Long());
            AddColumn("dbo.Contracts", "AccountId", c => c.Int());
            AddColumn("dbo.Contracts", "Discriminator", c => c.String(maxLength: 128));
            CreateIndex("dbo.Contracts", "AccountId");
            CreateIndex("dbo.InstagramContractPlans", "InstagramContract_Id");
            AddForeignKey("dbo.InstagramContractPlans", "InstagramContract_Id", "dbo.Contracts_Instagram", "Id");
            AddForeignKey("dbo.Contracts", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebsiteContractItems", "WebsiteContract_Id", "dbo.Contracts");
            DropForeignKey("dbo.WebsiteContractItems", "Contract_Id", "dbo.Contracts");
            DropForeignKey("dbo.Contracts", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.InstagramContractPlans", "InstagramContract_Id", "dbo.Contracts_Instagram");
            DropIndex("dbo.WebsiteContractItems", new[] { "WebsiteContract_Id" });
            DropIndex("dbo.WebsiteContractItems", new[] { "Contract_Id" });
            DropIndex("dbo.InstagramContractPlans", new[] { "InstagramContract_Id" });
            DropIndex("dbo.Contracts", new[] { "AccountId" });
            DropColumn("dbo.Contracts", "Discriminator");
            DropColumn("dbo.Contracts", "AccountId");
            DropColumn("dbo.Contracts", "HostRegistrationCost");
            DropColumn("dbo.Contracts", "DomainRegistrationCost");
            DropColumn("dbo.Contracts", "ExecuteDuration");
            DropColumn("dbo.Contracts", "CustomerPhone");
            DropColumn("dbo.Contracts", "CustomerAddress");
            DropColumn("dbo.Contracts", "DomainId");
            DropColumn("dbo.InstagramContractPlans", "InstagramContract_Id");
            DropTable("dbo.WebsiteContractItems");
            DropTable("dbo.Accounts");
            RenameTable(name: "dbo.InstagramContractPlans", newName: "ContractPlans");
        }
    }
}
