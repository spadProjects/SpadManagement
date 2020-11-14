namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plandscs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "ContractPlanDescriptions", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "ContractPlanDescriptions");
        }
    }
}
