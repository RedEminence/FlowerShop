using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowerShop.Data;
using FlowerShop.Models;

namespace FlowerShop.Controllers
{
    public class BouquetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BouquetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bouquets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bouquets
                .Include(bouquet => bouquet.Flowers)
                .ToListAsync());
        }
        
    }
}
