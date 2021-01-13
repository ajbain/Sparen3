namespace Sparen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDateAsProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "DateOfTransaction", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transaction", "DateOfTransaction");
        }
    }
}
