using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Domain;

namespace Services
{

    public class CartService : ICartService<Product>
    {
        private Dictionary<Product, int> _cart = new Dictionary<Product, int>();
        private AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public CartService()
        {

        }
        public void Add(Product product)
        {

            if (_cart.ContainsKey(product))
            {
                _cart[product] += 1;
            }
            else
            {

                _cart.Add(product, 1);
            }
        }

        public void SetDict(Dictionary<Product, int> dict)
        {
            _cart = dict;
        }

        public void Del(int id)
        {
            foreach (var item in _cart.Keys)
            {
                if (item.ProductId == id)
                {
                    _cart.Remove(item);
                }
            }
        }



        public Dictionary<Product, int> GetAll()
        {
            return _cart;
        }

        public void CheckOut(User user)
        {


            Order order = new Order { UserId = user.Id, TotalPrice = _cart.Sum(pair => pair.Key.ProductPrice * pair.Value), CreatedAt = DateTime.Now };

            _context.Orders.Add(order);
            _context.SaveChanges();
            _context.UserOrders.Add(new UserOrders { User = user, OrderId = order.OrderId });

            foreach (KeyValuePair<Product, int> item in _cart)
            {
                var product = _context.Products.FirstOrDefault(product1 => product1.ProductId == item.Key.ProductId);
                product.ProductQuantity -= item.Value;
                _context.Products.Update(product);
                var orderProduct = new OrderProducts
                {
                    OrderId = order.OrderId,
                    ProductId = product.ProductId
                    ,
                    Quantity = item.Value,
                    UnitPrice = item.Key.ProductPrice,
                    TotalPrice = item.Key.ProductPrice * item.Value
                };

                _context.OrderProducts.AddRange(orderProduct);
            }

            _context.SaveChanges();

        }
    }
}