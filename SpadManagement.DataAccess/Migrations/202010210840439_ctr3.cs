namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ctr3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "CustomerName", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "CustomerName");
        }
    }
}
