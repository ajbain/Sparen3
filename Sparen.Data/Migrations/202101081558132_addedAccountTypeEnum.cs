namespace Sparen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAccountTypeEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "AccountType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transaction", "AccountType");
        }
    }
}
