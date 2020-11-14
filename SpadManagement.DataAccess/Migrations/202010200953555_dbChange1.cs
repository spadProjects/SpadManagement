namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChange1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContractItems", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.ContractItems", new[] { "Contract_Id" });
            CreateTable(
                "dbo.ContractPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanDurationPriceId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Price = c.Long(nullable: false),
                        Discount = c.Long(nullable: false),
                        TotalPrice = c.Long(nullable: false),
                        PlanTitle = c.String(maxLength: 100),
                        DurationTitle = c.String(maxLength: 100),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Contract_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.Contract_Id)
                .ForeignKey("dbo.PlanDurationPrices", t => t.PlanDurationPriceId, cascadeDelete: true)
                .Index(t => t.PlanDurationPriceId)
                .Index(t => t.Contract_Id);
            
            CreateTable(
                "dbo.PlanDurationPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Duration = c.String(nullable: false, maxLength: 200),
                        Price = c.Long(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        PlanTypeId = c.Int(nullable: false),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlanTypes", t => t.PlanTypeId, cascadeDelete: true)
                .Index(t => t.PlanTypeId);
            
            CreateTable(
                "dbo.PlanTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanTitle = c.String(nullable: false, maxLength: 200),
                        DisplayOrder = c.Int(nullable: false),
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
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        ManagerName = c.String(nullable: false, maxLength: 100),
                        Mobile = c.String(nullable: false, maxLength: 50),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        City_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeoDivisions", t => t.City_Id)
                .Index(t => t.City_Id);
            
            AddColumn("dbo.Contracts", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Contracts", "CustomerCityId", c => c.Int(nullable: false));
            AddColumn("dbo.Contracts", "CustomerMobile", c => c.String(maxLength: 50));
            AddColumn("dbo.Contracts", "CustomerManagerName", c => c.String(maxLength: 500));
            AddColumn("dbo.Contracts", "InstagramId", c => c.String(maxLength: 300));
            AddColumn("dbo.Contracts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Contracts", "CustomerId");
            AddForeignKey("dbo.Contracts", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.Contracts", "PassengerInsurancePrice");
            DropColumn("dbo.Contracts", "ServiceType");
            DropColumn("dbo.Contracts", "FromDate");
            DropColumn("dbo.Contracts", "ToDate");
            DropColumn("dbo.Contracts", "PassengerAddress");
            DropColumn("dbo.Contracts", "PassengerPhone");
            DropColumn("dbo.Contracts", "PassengerWorkAddress");
            DropColumn("dbo.Contracts", "PassengerMobile");
            DropTable("dbo.ContractItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContractItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersianFullName = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BirthDate = c.String(nullable: false, maxLength: 50),
                        Duration = c.String(nullable: false),
                        HotelName = c.String(nullable: false, maxLength: 100),
                        AirlineTitle = c.String(nullable: false, maxLength: 100),
                        DestinationCountry = c.String(nullable: false, maxLength: 100),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Contract_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Contracts", "PassengerMobile", c => c.String(maxLength: 50));
            AddColumn("dbo.Contracts", "PassengerWorkAddress", c => c.String(maxLength: 500));
            AddColumn("dbo.Contracts", "PassengerPhone", c => c.String(maxLength: 50));
            AddColumn("dbo.Contracts", "PassengerAddress", c => c.String(maxLength: 500));
            AddColumn("dbo.Contracts", "ToDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contracts", "FromDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contracts", "ServiceType", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Contracts", "PassengerInsurancePrice", c => c.Long(nullable: false));
            DropForeignKey("dbo.Contracts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "City_Id", "dbo.GeoDivisions");
            DropForeignKey("dbo.PlanDurationPrices", "PlanTypeId", "dbo.PlanTypes");
            DropForeignKey("dbo.ContractPlans", "PlanDurationPriceId", "dbo.PlanDurationPrices");
            DropForeignKey("dbo.ContractPlans", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.Customers", new[] { "City_Id" });
            DropIndex("dbo.PlanDurationPrices", new[] { "PlanTypeId" });
            DropIndex("dbo.Contracts", new[] { "CustomerId" });
            DropIndex("dbo.ContractPlans", new[] { "Contract_Id" });
            DropIndex("dbo.ContractPlans", new[] { "PlanDurationPriceId" });
            DropColumn("dbo.Contracts", "Discriminator");
            DropColumn("dbo.Contracts", "InstagramId");
            DropColumn("dbo.Contracts", "CustomerManagerName");
            DropColumn("dbo.Contracts", "CustomerMobile");
            DropColumn("dbo.Contracts", "CustomerCityId");
            DropColumn("dbo.Contracts", "CustomerId");
            DropTable("dbo.Customers");
            DropTable("dbo.PlanTypes");
            DropTable("dbo.PlanDurationPrices");
            DropTable("dbo.ContractPlans");
            CreateIndex("dbo.ContractItems", "Contract_Id");
            AddForeignKey("dbo.ContractItems", "Contract_Id", "dbo.Contracts", "Id");
        }
    }
}
