using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowerShop.Data;
using FlowerShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FlowerShop.ViewModels;

namespace FlowerShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<User> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder)
        {
            List<Order> orders;

            if (User.IsInRole("Manager"))
            {
                IQueryable<Order> ordersIQ = from o in _context.Orders
                                                 select o;

                if (sortOrder == "date_desc")
                {
                    ordersIQ = ordersIQ.OrderByDescending(s => s.DeliverAt);
                } else
                {
                    ordersIQ = ordersIQ.OrderBy(s => s.DeliverAt);
                }

                orders = await ordersIQ
                    .Include(o => o.Status)
                    .Include(o => o.Bouquet)
                    .Include(o => o.User)
                    .AsNoTracking()
                    .ToListAsync();
            } 
            else
            {
                var user = await _userManager.GetUserAsync(User);

                orders = await _context.Orders
                    .Where(o => o.UserId == user.Id)
                    .Include(o => o.Status)
                    .Include(o => o.Bouquet)
                    .ToListAsync();
            }

            ViewData["SortOrder"] = sortOrder;

            return View(orders);
        }

        // GET: Orders/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Bouquet)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(o => o.Id == id);

            var user = await _userManager.GetUserAsync(User);

            if (order == null || (user.Id != order.UserId && !User.IsInRole("Manager")))
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(int BouquetId)
        {
            var bouquet = await _context.Bouquets
                .Include(b => b.Flowers)
                .SingleOrDefaultAsync(b => b.Id == BouquetId);

            ViewData["Bouquet"] = bouquet;

            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeliverAt,Address")] OrderCreate orderViewModel, int BouquetId)
        {
            var bouquet = await _context.Bouquets.FindAsync(BouquetId);

            if (bouquet == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var orderStatus = _context.OrderStatuses.Where(os => os.Name == "Принят").First();

                var order = new Order()
                {
                    UserId = user.Id,
                    StatusId = orderStatus.Id,
                    CreatedAt = DateTime.Now,
                    BouquetId = bouquet.Id,
                    Address = orderViewModel.Address,
                    DeliverAt = orderViewModel.DeliverAt
                };

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Roles = "Manager")]
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Bouquet)
                .SingleOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            ViewData["OrderStatuses"] = await _context.OrderStatuses.ToListAsync();

            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StatusId,DeliverAt,Address")] OrderUpdate orderViewModel)
        {
            if (id != orderViewModel.Id)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);

            if (!OrderExists(order.Id))
            {
                return NotFound();
            }

            order.StatusId = orderViewModel.StatusId; 
            order.DeliverAt = orderViewModel.DeliverAt;
            order.Address = orderViewModel.Address;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(orderViewModel.Id))
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

            ViewData["OrderStatuses"] = await _context.OrderStatuses.ToListAsync();

            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Bouquet)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager")]
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
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
