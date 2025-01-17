using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Retail_Store.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Processing, On Transit, Delivered
    }
}