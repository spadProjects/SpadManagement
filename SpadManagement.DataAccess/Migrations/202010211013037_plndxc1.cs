namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plndxc1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlanDurationPrices", "PlanDescription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlanDurationPrices", "PlanDescription", c => c.String(maxLength: 200));
        }
    }
}
