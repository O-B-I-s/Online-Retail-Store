namespace Online_Retail_Store.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice
        {
            get { return Quantity * UnitPrice; }
        }

        // Navigation properties
        public virtual Product Product { get; set; }
    }

}
