using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    class Order

    {
        public int OrderId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public decimal TotalPrice { get; set; }
    }
}
