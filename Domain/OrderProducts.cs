
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class OrderProducts
    {
        public int OrderProductsId { get; set; }

        public int OrderId { get; set; }

        public Order? Order { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Unit Price ")]

        public decimal UnitPrice { get; set; }

        [Display(Name = "Total Price ")]

        public decimal TotalPrice { get; set; }

    }
}
