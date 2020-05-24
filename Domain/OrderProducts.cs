
using System.Collections.Generic;

namespace Domain
{
    public class OrderProducts
    {
        public int OrderProductsId { get; set; }

        public int OrderId { get; set; }

        public Order? Order { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }
   
    }
}
