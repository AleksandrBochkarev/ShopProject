using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
   public  class Category
    {
        public int CategoryId { get; set; }


        [MinLength(4)] [MaxLength(12)]

        public string CategoryName { get; set; }

        public ICollection<CategoryProducts>? CategoryProducts { get; set; }
    }
}
