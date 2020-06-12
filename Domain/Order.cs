using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Order
    {
        private readonly DateTime _createdAt = DateTime.Now;
        public int OrderId { get; set; }

        [Display(Name = "Order date")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User? User { get; set; }

        [Display(Name = "Order total price")]

        public decimal TotalPrice { get; set; }
        [Display(Name = "Products ordered ")]
        public ICollection<OrderProducts>? ProductsOrdered { get; set; }
    }
}
