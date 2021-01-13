namespace Sparen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oldBalanceAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "OldBalance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transaction", "OldBalance");
        }
    }
}
