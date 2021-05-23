using EverGardenNew.Data;
using EverGardenNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EverGardenNew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchPlant(string searchString, string categoryEd, string categoryPl)
        {
            ViewData["CurrentFilter"] = searchString;
            //ViewData["CatEdID"] = categoryEd;
            //ViewData["CatPlID"] = categoryPl;

            var catEdible = from c in _context.CategoryEdibles select c;
            ViewData["CategoryEdible"] = catEdible;
            var catPlace = from c in _context.CategoryPlaces select c;
            ViewData["CategoryPlace"] = catPlace;

            var plants = from p in _context.Plants
                         .Include(p => p.CategoryEdible)
                         .Include(p => p.CategoryPlace)
                         select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                plants = plants.Where(s => s.Name.Contains(searchString)
                                       || s.ScientificName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(categoryEd)) {
                if (!categoryEd.Equals("0"))
                {
                    plants = plants.Where(s => s.CategoryEdibleID.ToString().Equals(categoryEd));
                }
            }

            if (!String.IsNullOrEmpty(categoryPl)) {
                if (!categoryPl.Equals("0"))
                {
                    plants = plants.Where(s => s.CategoryPlaceID.ToString().Equals(categoryPl));
                }
            }

            return View(await plants.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> PlantDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants
                .Include(p => p.CategoryEdible)
                .Include(p => p.CategoryPlace)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
