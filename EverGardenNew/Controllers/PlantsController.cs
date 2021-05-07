using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EverGardenNew.Data;
using EverGardenNew.Models;

namespace EverGardenNew.Controllers
{
    public class PlantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plants
        /*public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Plants.Include(p => p.CategoryEdible).Include(p => p.CategoryPlace);
            return View(await applicationDbContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ScientificNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "sc_name_desc" : "Sc_name";
            ViewData["CurrentFilter"] = searchString;

            var plants = from p in _context.Plants
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                plants = plants.Where(s => s.Name.Contains(searchString)
                                       || s.ScientificName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    plants = plants.OrderByDescending(s => s.Name);
                    break;
                case "sc_name_desc":
                    plants = plants.OrderBy(s => s.ScientificName);
                    break;
                case "Sc_name":
                    plants = plants.OrderByDescending(s => s.ScientificName);
                    break;
                default:
                    plants = plants.OrderBy(s => s.Name);
                    break;
            }
            return View(await plants.AsNoTracking().ToListAsync());
        }

        // GET: Plants/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Plants/Create
        public IActionResult Create()
        {
            ViewData["CategoryEdibleID"] = new SelectList(_context.CategoryEdibles, "Id", "Id");
            ViewData["CategoryPlaceID"] = new SelectList(_context.CategoryPlaces, "Id", "Id");
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ScientificName,CategoryEdibleID,CategoryPlaceID,Climate,Watering,ShortDescription,BioDescription,SpreadingArea,SmallImage,LargeImage,Tools")] Plant plant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryEdibleID"] = new SelectList(_context.CategoryEdibles, "Id", "Id", plant.CategoryEdibleID);
            ViewData["CategoryPlaceID"] = new SelectList(_context.CategoryPlaces, "Id", "Id", plant.CategoryPlaceID);
            return View(plant);
        }

        // GET: Plants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            ViewData["CategoryEdibleID"] = new SelectList(_context.CategoryEdibles, "Id", "Id", plant.CategoryEdibleID);
            ViewData["CategoryPlaceID"] = new SelectList(_context.CategoryPlaces, "Id", "Id", plant.CategoryPlaceID);
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ScientificName,CategoryEdibleID,CategoryPlaceID,Climate,Watering,ShortDescription,BioDescription,SpreadingArea,SmallImage,LargeImage,Tools")] Plant plant)
        {
            if (id != plant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(plant.Id))
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
            ViewData["CategoryEdibleID"] = new SelectList(_context.CategoryEdibles, "Id", "Id", plant.CategoryEdibleID);
            ViewData["CategoryPlaceID"] = new SelectList(_context.CategoryPlaces, "Id", "Id", plant.CategoryPlaceID);
            return View(plant);
        }

        // GET: Plants/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(int id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}
