namespace Online_Retail_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdatesToDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItems", "UnitPrice");
        }
    }
}
