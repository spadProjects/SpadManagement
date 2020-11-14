namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prs1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Contracts", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contracts", "PersonId");
            AddForeignKey("dbo.Contracts", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "PersonId", "dbo.People");
            DropIndex("dbo.Contracts", new[] { "PersonId" });
            DropColumn("dbo.Contracts", "PersonId");
            DropTable("dbo.People");
        }
    }
}
