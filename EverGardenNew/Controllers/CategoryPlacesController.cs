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
    public class CategoryPlacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryPlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*// GET: CategoryPlaces
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoryPlaces.ToListAsync());
        }*/

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var categories = from c in _context.CategoryPlaces
                             select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(c => c.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    categories = categories.OrderByDescending(c => c.Name);
                    break;
                default:
                    categories = categories.OrderBy(c => c.Name);
                    break;
            }
            return View(await categories.AsNoTracking().ToListAsync());
        }

        // GET: CategoryPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPlace = await _context.CategoryPlaces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryPlace == null)
            {
                return NotFound();
            }

            return View(categoryPlace);
        }

        // GET: CategoryPlaces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryPlaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryPlace categoryPlace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryPlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryPlace);
        }

        // GET: CategoryPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPlace = await _context.CategoryPlaces.FindAsync(id);
            if (categoryPlace == null)
            {
                return NotFound();
            }
            return View(categoryPlace);
        }

        // POST: CategoryPlaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryPlace categoryPlace)
        {
            if (id != categoryPlace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryPlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryPlaceExists(categoryPlace.Id))
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
            return View(categoryPlace);
        }

        // GET: CategoryPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPlace = await _context.CategoryPlaces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryPlace == null)
            {
                return NotFound();
            }

            return View(categoryPlace);
        }

        // POST: CategoryPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryPlace = await _context.CategoryPlaces.FindAsync(id);
            _context.CategoryPlaces.Remove(categoryPlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryPlaceExists(int id)
        {
            return _context.CategoryPlaces.Any(e => e.Id == id);
        }
    }
}
