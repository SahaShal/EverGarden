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
    public class PlantActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlantActivities
        /*public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlantActivities.Include(p => p.Plant);
            return View(await applicationDbContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            var activities = from a in _context.PlantActivities
                           select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                activities = activities.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    activities = activities.OrderByDescending(a => a.Title);
                    break;
                default:
                    activities = activities.OrderBy(a => a.Title);
                    break;
            }
            return View(await activities.AsNoTracking().ToListAsync());
        }

        // GET: PlantActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantActivity = await _context.PlantActivities
                .Include(p => p.Plant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantActivity == null)
            {
                return NotFound();
            }

            return View(plantActivity);
        }

        // GET: PlantActivities/Create
        public IActionResult Create()
        {
            ViewData["PlantID"] = new SelectList(_context.Plants, "Id", "Id");
            return View();
        }

        // POST: PlantActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Instructions,NeededTools,PlantID")] PlantActivity plantActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantID"] = new SelectList(_context.Plants, "Id", "Id", plantActivity.PlantID);
            return View(plantActivity);
        }

        // GET: PlantActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantActivity = await _context.PlantActivities.FindAsync(id);
            if (plantActivity == null)
            {
                return NotFound();
            }
            ViewData["PlantID"] = new SelectList(_context.Plants, "Id", "Id", plantActivity.PlantID);
            return View(plantActivity);
        }

        // POST: PlantActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Instructions,NeededTools,PlantID")] PlantActivity plantActivity)
        {
            if (id != plantActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantActivityExists(plantActivity.Id))
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
            ViewData["PlantID"] = new SelectList(_context.Plants, "Id", "Id", plantActivity.PlantID);
            return View(plantActivity);
        }

        // GET: PlantActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantActivity = await _context.PlantActivities
                .Include(p => p.Plant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plantActivity == null)
            {
                return NotFound();
            }

            return View(plantActivity);
        }

        // POST: PlantActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantActivity = await _context.PlantActivities.FindAsync(id);
            _context.PlantActivities.Remove(plantActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantActivityExists(int id)
        {
            return _context.PlantActivities.Any(e => e.Id == id);
        }
    }
}
