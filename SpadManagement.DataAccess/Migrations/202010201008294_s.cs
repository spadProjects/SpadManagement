namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts_Instagram",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        InstagramId = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.Contracts", "InstagramId");
            DropColumn("dbo.Contracts", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contracts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Contracts", "InstagramId", c => c.String(maxLength: 300));
            DropForeignKey("dbo.Contracts_Instagram", "Id", "dbo.Contracts");
            DropIndex("dbo.Contracts_Instagram", new[] { "Id" });
            DropTable("dbo.Contracts_Instagram");
        }
    }
}
