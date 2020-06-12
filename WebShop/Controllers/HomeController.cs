using System.Diagnostics;
using System.Linq;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
 

        public HomeController( ApplicationDbContext context)
        {
            _context = context;
            
        }
   
        public IActionResult Index()
        {
            var products = _context.Products.Include(product => product.Category);
            return View(products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}