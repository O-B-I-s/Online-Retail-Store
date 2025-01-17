namespace Online_Retail_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImagePath");
        }
    }
}
