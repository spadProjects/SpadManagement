namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init324 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LoginProvider);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        IsLocked = c.Boolean(),
                        Description = c.String(),
                        Avatar = c.Binary(),
                        CenterId = c.Int(),
                        GroupId = c.Int(),
                        LastLoginDate = c.DateTime(),
                        LastLoginDatePreview = c.DateTime(),
                        LastPasswordChangedDate = c.DateTime(),
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
                "dbo.AuthorizationItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        FarsiTitle = c.String(),
                        ItemType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthorizationItemsRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstItemId = c.Int(nullable: false),
                        SecondItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.Contract_Id)
                .Index(t => t.Contract_Id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractNo = c.String(nullable: false, maxLength: 50),
                        PassengerInsurancePrice = c.Long(nullable: false),
                        ServiceType = c.String(nullable: false, maxLength: 50),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        PassengerAddress = c.String(maxLength: 500),
                        PassengerPhone = c.String(maxLength: 50),
                        PassengerWorkAddress = c.String(maxLength: 500),
                        PassengerMobile = c.String(maxLength: 50),
                        ContractDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContractContext = c.String(),
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
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 300),
                        LogType = c.Int(nullable: false),
                        Event = c.String(maxLength: 300),
                        IpAddress = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
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
                "dbo.SystemParameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParameterName = c.String(),
                        Value = c.String(),
                        PersianTitle = c.String(),
                        Description = c.String(),
                        IsSystemic = c.Boolean(nullable: false),
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
                "dbo.UserAuthorizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        AuthorizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractItems", "Contract_Id", "dbo.Contracts");
            DropIndex("dbo.ContractItems", new[] { "Contract_Id" });
            DropTable("dbo.UserAuthorizations");
            DropTable("dbo.SystemParameters");
            DropTable("dbo.Logs");
            DropTable("dbo.Contracts");
            DropTable("dbo.ContractItems");
            DropTable("dbo.AuthorizationItemsRelations");
            DropTable("dbo.AuthorizationItems");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetRoles");
        }
    }
}
