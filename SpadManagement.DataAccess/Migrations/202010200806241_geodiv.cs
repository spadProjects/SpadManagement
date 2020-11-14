namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class geodiv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoDivisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        LatinTitle = c.String(),
                        GeoDivisionType = c.Int(nullable: false),
                        ParentId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsArchived = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GeoDivisions");
        }
    }
}
