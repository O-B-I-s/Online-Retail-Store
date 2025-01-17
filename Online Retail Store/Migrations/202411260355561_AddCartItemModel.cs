namespace Online_Retail_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartItemModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        CartItemId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CartItemId);
            
            AddColumn("dbo.Products", "CartItem_CartItemId", c => c.Int());
            CreateIndex("dbo.Products", "CartItem_CartItemId");
            AddForeignKey("dbo.Products", "CartItem_CartItemId", "dbo.CartItems", "CartItemId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CartItem_CartItemId", "dbo.CartItems");
            DropIndex("dbo.Products", new[] { "CartItem_CartItemId" });
            DropColumn("dbo.Products", "CartItem_CartItemId");
            DropTable("dbo.CartItems");
        }
    }
}
