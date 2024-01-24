using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Models.BasicData;
using ProjectsMeasurements.Repositories.Interfaces;
using ProjectsMeasuremnts.Services.Interfaces;

namespace ProjectsMeasurements.WEB.Controllers.BasicData
{
    public class PlantsCategoriesController : Controller
    {
        private readonly ProjectsMeasurementsDBContext _context;

        public PlantsCategoriesController(ProjectsMeasurementsDBContext context)
        {
            _context = context;
        }

        // GET: PlantsCategories
        public async Task<IActionResult> Index(int? CategoryID)
        {
            List<PlantsCategory> PlantsCategories = new();
            if (CategoryID == null)
                PlantsCategories = await _context.PlantsCategories.Include(p => p.LastUpdateUser).Where(x => x.ParentID == null).ToListAsync();
            else
                PlantsCategories = await _context.PlantsCategories.Include(p => p.LastUpdateUser).Include(p => p.ParentCategory).Where(x => x.ParentID == CategoryID).ToListAsync();
            return View(PlantsCategories);
        }

        // GET: PlantsCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlantsCategories == null)
            {
                return NotFound();
            }

            var plantsCategory = await _context.PlantsCategories
                .Include(p => p.LastUpdateUser)
                .Include(p => p.ParentCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plantsCategory == null)
            {
                return NotFound();
            }

            return View(plantsCategory);
        }

        // GET: PlantsCategories/Create
        public IActionResult Create()
        {
            //ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail");
            ViewData["ParentID"] = new SelectList(_context.PlantsCategories, "ID", "CategoryNameEn");
            return View();
        }

        // POST: PlantsCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryNameEn,CategoryNameAr,CategoryDescription,Code,ParentID,FullCode,ID,LastUserID,LastUpdateDate")] PlantsCategory plantsCategory)
        {
            if (_context.PlantsCategories.Where(x => x.Code == plantsCategory.Code).Any())
                ModelState.AddModelError("Code", "This code is used by another plant category.");
            else
                plantsCategory.FullCode = plantsCategory.Code;
            if (ModelState.IsValid)
            {
                _context.Add(plantsCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", plantsCategory.LastUserID);
            ViewData["ParentID"] = new SelectList(_context.PlantsCategories, "ID", "CategoryNameEn", plantsCategory.ParentID);
            return View(plantsCategory);
        }

        // GET: PlantsCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlantsCategories == null)
            {
                return NotFound();
            }

            var plantsCategory = await _context.PlantsCategories.FindAsync(id);
            if (plantsCategory == null)
            {
                return NotFound();
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", plantsCategory.LastUserID);
            ViewData["ParentID"] = new SelectList(_context.PlantsCategories, "ID", "CategoryNameEn", plantsCategory.ParentID);
            return View(plantsCategory);
        }

        // POST: PlantsCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryNameEn,CategoryNameAr,CategoryDescription,Code,ParentID,FullCode,ID,LastUserID,LastUpdateDate")] PlantsCategory plantsCategory)
        {
            if (id != plantsCategory.ID)
            {
                return NotFound();
            }
            if (_context.PlantsCategories.Where(x => x.ID != plantsCategory.ID && x.Code == plantsCategory.Code).Any())
                ModelState.AddModelError("Code", "This code is used by another plant category.");
            else
                plantsCategory.FullCode = plantsCategory.Code;

            if (ModelState.IsValid)
            {
                try
                {
                    plantsCategory.LastUpdateDate = DateTime.Now;
                    _context.Update(plantsCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantsCategoryExists(plantsCategory.ID))
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
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", plantsCategory.LastUserID);
            ViewData["ParentID"] = new SelectList(_context.PlantsCategories, "ID", "CategoryNameEn", plantsCategory.ParentID);
            return View(plantsCategory);
        }

        // GET: PlantsCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlantsCategories == null)
            {
                return NotFound();
            }

            var plantsCategory = await _context.PlantsCategories
                .Include(p => p.LastUpdateUser)
                .Include(p => p.ParentCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plantsCategory == null)
            {
                return NotFound();
            }

            return View(plantsCategory);
        }

        // POST: PlantsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlantsCategories == null)
            {
                return Problem("Entity set 'ProjectsMeasurementsDBContext.PlantsCategories'  is null.");
            }
            var plantsCategory = await _context.PlantsCategories.FindAsync(id);
            if (plantsCategory != null)
            {
                _context.PlantsCategories.Remove(plantsCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantsCategoryExists(int id)
        {
            return (_context.PlantsCategories?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
