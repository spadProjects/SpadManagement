namespace SpadManagement.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accEdit1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "BankName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Accounts", "CardNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.Accounts", "AccountNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.Accounts", "ShebaNo", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "ShebaNo", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Accounts", "AccountNo", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Accounts", "CardNo");
            DropColumn("dbo.Accounts", "BankName");
        }
    }
}
