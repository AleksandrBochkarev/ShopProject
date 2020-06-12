using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;
using Utils;

namespace WebShop.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private  ICartService<Product> _cart ;
        
        public CartController(ApplicationDbContext context, ICartService<Product> cart)
        {
            _context = context;
             _cart = cart;
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey(HttpContext.User.Identity.Name))
            {
               LoadProductFromCookies();
            }
            else
            {
                HttpContext.Response.Cookies.Append(HttpContext.User.Identity.Name,JsonConvert.SerializeObject(_cart.GetAll(),new CustomDictionaryConverter<Product,int>()));

            }
 
             ViewBag.cartItems = _cart.GetAll();
             ViewData["cartTotal"] = _cart.GetAll().Sum(x => x.Key.ProductPrice * x.Value);
             return View();
        }

       
        
        
        public IActionResult Add(int? id)
        {
            Product p = _context.Products.First(product => product.ProductId == id);

            // if (_cart.GetAll().ContainsKey(p))
            // {
            //     _cart.GetAll().
            // }
            _cart.Add(p);
            
             if (HttpContext.Request.Cookies.ContainsKey(HttpContext.User.Identity.Name))
            {
                var c  = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<Product, int>>>(HttpContext.Request.Cookies[HttpContext.User.Identity.Name]);
                
                var dictionary = c.ToDictionary(x => x.Key, x => x.Value);
                
                foreach (var Item in _cart.GetAll())
                {
                    if (dictionary.ContainsKey(Item.Key))
                    {
                        
                        dictionary[Item.Key] += Item.Value;

                    }
                    else
                    {
                        dictionary.Add(Item.Key,Item.Value);
                    }
                }
                _cart.SetDict(dictionary);
               

                  var s = JsonConvert.SerializeObject(dictionary,new CustomDictionaryConverter<Product,int>());
                             HttpContext.Response.Cookies.Append(HttpContext.User.Identity.Name, s);
              
            }
            else
            {
                 HttpContext.Response.Cookies.Append(HttpContext.User.Identity.Name, JsonConvert.SerializeObject(_cart.GetAll(),new CustomDictionaryConverter<Product,int>()));
            }

           
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Checkout()
        {
            LoadProductFromCookies();
            var user =  _context.User.First(user => user.UserName == HttpContext.User.Identity.Name);
           
         
            _cart.CheckOut(user);
            HttpContext.Response.Cookies.Delete(HttpContext.User.Identity.Name);
           return RedirectToAction(nameof(Index));
        }

        private void LoadProductFromCookies()
        {
            var c = JsonConvert.DeserializeObject<IEnumerable<KeyValuePair<Product, int>>>(
                HttpContext.Request.Cookies[HttpContext.User.Identity.Name]);

            var dictionary = c.ToDictionary(x => x.Key, x => x.Value);


            _cart.SetDict(dictionary);
        }


        public IActionResult Delete(int id)
        {
            LoadProductFromCookies();
             _cart.Del(id);
             var s = JsonConvert.SerializeObject(_cart.GetAll(),new CustomDictionaryConverter<Product,int>());
             HttpContext.Response.Cookies.Append(HttpContext.User.Identity.Name, s);
            return RedirectToAction(nameof(Index));
        }
    }
}