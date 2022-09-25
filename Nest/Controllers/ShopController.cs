using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.DAL;
using Nest.Models;
using Nest.ViewModels;
using Z.EntityFramework.Plus;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nest.Controllers
{
    public class ShopController : Controller
    {
        private NestContext _context { get; set; }

        public ShopController(NestContext context)
        {
            _context = context;
        }

        public IActionResult Index(string minPrice, string maxPrice)
        {
            ShopAndFilterVM shopAndFilter = new ShopAndFilterVM
            {
                Products = _context.Products.IncludeOptimized(p => p.ProductImages).IncludeOptimized(p => p.Vendor)
                .IncludeOptimized(p => p.Badge).IncludeOptimized(p => p.Category),
                Categories = _context.Categories.Include(c => c.Products).Where(c => !c.IsDeleted),
                Vendors = _context.Vendors.Include(v => v.Products).Where(v => !v.IsDeleted),
            };
            
            if(!string.IsNullOrWhiteSpace(minPrice))
            {
                var min = int.Parse(minPrice);
                shopAndFilter.Products = shopAndFilter.Products.Where(p => p.SellPrice >= min);
            }
            if (!string.IsNullOrWhiteSpace(maxPrice))
            {
                var max = int.Parse(maxPrice);
                shopAndFilter.Products = shopAndFilter.Products.Where(p => p.SellPrice <= max);
            }

            return View(shopAndFilter);
        }

        public IActionResult Product()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Compare()
        {
            return View();
        }


    }
}

