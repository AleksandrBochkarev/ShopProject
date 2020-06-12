using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty] public IEnumerable<int> Products { get; set; }
        private readonly UserManager<User> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var userId =   _userManager.GetUserId(HttpContext.User);
 
            var appDbContext = _context.Orders.Include(o => o.User)
                .Include(op => op.ProductsOrdered)
                .Include(order => order.ProductsOrdered).ThenInclude(products => products.Product);
             return View(await appDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var order = _context.Orders
                .Include(o => o.User).Include(order => order.ProductsOrdered).ThenInclude(products => products.Product)
                .FirstOrDefault(order1 => order1.OrderId == id);
            ViewData["Products"] = new SelectList(_context.Products.ToList(), "ProductId", "ProductName");

            if (order == null)
            {
                return NotFound();
            }

            return View( order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
             ViewBag.Products = Products;
             
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId, ProductId, TotalPrice")] Order order)
        {
            
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
           
                order.UserId = int.Parse(userId);
                 
                
                Console.WriteLine(userId);
                await _context.Orders.AddAsync(order);

                await _context.SaveChangesAsync();
                
                await  _context.UserOrders.AddAsync(new UserOrders(){OrderId = order.OrderId, Id =  order.UserId});
                
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["Products"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var order =  _context.Orders
                .Include(order1 => order1.ProductsOrdered)
                .ThenInclude(p => p.Product)
                .FirstOrDefault(o => o.OrderId == id);
            
            
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Products"] = new SelectList(_context.Products,"ProductsId","ProductName", order.ProductsOrdered);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CreatedAt,UserId,TotalPrice")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
