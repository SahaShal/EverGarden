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
    public class CategoryEdiblesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryEdiblesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CategoryEdibles
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.CategoryEdibles.ToListAsync());
        }*/

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var categories = from c in _context.CategoryEdibles
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

        // GET: CategoryEdibles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEdible = await _context.CategoryEdibles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryEdible == null)
            {
                return NotFound();
            }

            return View(categoryEdible);
        }

        // GET: CategoryEdibles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryEdibles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryEdible categoryEdible)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryEdible);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryEdible);
        }

        // GET: CategoryEdibles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEdible = await _context.CategoryEdibles.FindAsync(id);
            if (categoryEdible == null)
            {
                return NotFound();
            }
            return View(categoryEdible);
        }

        // POST: CategoryEdibles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryEdible categoryEdible)
        {
            if (id != categoryEdible.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryEdible);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryEdibleExists(categoryEdible.Id))
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
            return View(categoryEdible);
        }

        // GET: CategoryEdibles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEdible = await _context.CategoryEdibles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryEdible == null)
            {
                return NotFound();
            }

            return View(categoryEdible);
        }

        // POST: CategoryEdibles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryEdible = await _context.CategoryEdibles.FindAsync(id);
            _context.CategoryEdibles.Remove(categoryEdible);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryEdibleExists(int id)
        {
            return _context.CategoryEdibles.Any(e => e.Id == id);
        }
    }
}
