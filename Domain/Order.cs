using System;
using System.Collections.Generic;

namespace Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }

        public ICollection<OrderProducts>? ProductsOrdered { get; set; }
    }
}
