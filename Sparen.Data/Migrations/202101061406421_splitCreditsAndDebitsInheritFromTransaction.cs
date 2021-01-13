namespace Sparen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class splitCreditsAndDebitsInheritFromTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        AccountNumber = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Balance = c.Double(nullable: false),
                        StoreName = c.String(),
                        TransactionType = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.BankAccount", t => t.AccountNumber, cascadeDelete: true)
                .Index(t => t.AccountNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "AccountNumber", "dbo.BankAccount");
            DropIndex("dbo.Transaction", new[] { "AccountNumber" });
            DropTable("dbo.Transaction");
        }
    }
}
