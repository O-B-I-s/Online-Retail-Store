namespace Online_Retail_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Status", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Status");
        }
    }
}
