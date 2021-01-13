namespace Sparen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTransactionType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transaction", "NewBalance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transaction", "NewBalance", c => c.Double(nullable: false));
        }
    }
}
