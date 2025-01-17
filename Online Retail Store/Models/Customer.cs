using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Online_Retail_Store.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(500)]
        public string Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}