namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plandsc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanDurationPrices", "PlanDescription", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanDurationPrices", "PlanDescription");
        }
    }
}
