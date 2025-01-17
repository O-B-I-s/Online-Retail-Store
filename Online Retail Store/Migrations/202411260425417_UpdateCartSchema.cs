namespace Online_Retail_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCartSchema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CartItem_CartItemId", "dbo.CartItems");
            DropIndex("dbo.Products", new[] { "CartItem_CartItemId" });
            AddColumn("dbo.CartItems", "UserId", c => c.String());
            CreateIndex("dbo.CartItems", "ProductId");
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            DropColumn("dbo.Products", "CartItem_CartItemId");
            DropColumn("dbo.CartItems", "ProductName");
            DropColumn("dbo.CartItems", "UnitPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItems", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CartItems", "ProductName", c => c.String());
            AddColumn("dbo.Products", "CartItem_CartItemId", c => c.Int());
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.Products");
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropColumn("dbo.CartItems", "UserId");
            CreateIndex("dbo.Products", "CartItem_CartItemId");
            AddForeignKey("dbo.Products", "CartItem_CartItemId", "dbo.CartItems", "CartItemId");
        }
    }
}
