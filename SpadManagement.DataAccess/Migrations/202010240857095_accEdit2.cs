namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accEdit2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "OwnerName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "OwnerName");
        }
    }
}
