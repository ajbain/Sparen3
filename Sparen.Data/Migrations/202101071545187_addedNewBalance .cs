namespace Sparen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNewBalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "NewBalance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transaction", "NewBalance");
        }
    }
}
