using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Product
    {
        protected bool Equals(Product other)
        {
            return ProductId == other.ProductId;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product)obj);
        }

        public override int GetHashCode()
        {
            return ProductId;
        }


        public int ProductId { get; set; }

        [MinLength(4)]
        [MaxLength(12)]

        [Display(Name = "Product Name")]

        public string ProductName { get; set; }
        [Display(Name = "Product Price")]

        public decimal ProductPrice { get; set; }

        [Display(Name = "Quantity")]


        public int ProductQuantity { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<OrderProducts>? OrderProduct { get; set; }

        public override string ToString()
        {
            return $"ProductId: {ProductId}, ProductName: {ProductName}, ProductPrice: {ProductPrice}, ProductQuantity: {ProductQuantity}, CategoryId: {CategoryId}";
        }
    }
}