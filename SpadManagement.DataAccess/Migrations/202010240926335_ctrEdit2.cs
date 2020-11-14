namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ctrEdit2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contracts", "PersonId", "dbo.People");
            DropIndex("dbo.Contracts", new[] { "PersonId" });
            AlterColumn("dbo.Contracts", "PersonId", c => c.Int());
            CreateIndex("dbo.Contracts", "PersonId");
            AddForeignKey("dbo.Contracts", "PersonId", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "PersonId", "dbo.People");
            DropIndex("dbo.Contracts", new[] { "PersonId" });
            AlterColumn("dbo.Contracts", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contracts", "PersonId");
            AddForeignKey("dbo.Contracts", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
    }
}
