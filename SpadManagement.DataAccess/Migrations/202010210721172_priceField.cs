namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class priceField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "DiscountTotalPrice", c => c.Long(nullable: false));
            AddColumn("dbo.Contracts", "PaymentTotalPrice", c => c.Long(nullable: false));
            AlterColumn("dbo.Contracts", "TotalPrice", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contracts", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Contracts", "PaymentTotalPrice");
            DropColumn("dbo.Contracts", "DiscountTotalPrice");
        }
    }
}
