using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    class CategoryProducts
    {
        public int CategoryProductsId { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
