using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    class Product
    {
        public int ProductId { get; set; }

        [MinLength(4)] [MaxLength(12)]

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<OrderProducts>? OrderProduct { get; set; }
    }
}
