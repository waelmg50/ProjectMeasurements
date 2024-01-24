using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Models.BasicData;
using ProjectsMeasurements.Models.Operations;

namespace ProjectsMeasurements.WEB.Controllers.BasicData
{
    public class PlantsController : Controller
    {
        private readonly ProjectsMeasurementsDBContext _context;

        public PlantsController(ProjectsMeasurementsDBContext context)
        {
            _context = context;
        }

        // GET: Plants
        public async Task<IActionResult> Index(int? CategoryID)
        {
            List<Plant> Plants = new();
            if (CategoryID == null)
                Plants = await _context.Plants.Include(p => p.CategoryOfPlant).ToListAsync();
            else
            {
                ViewBag.CategoryID = CategoryID;
                Plants = await _context.Plants.Include(p => p.CategoryOfPlant).Where(x => x.PlantCategoryID == CategoryID).ToListAsync();
            }
            return View(Plants);
        }

        // GET: Plants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plants == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants
                .Include(p => p.CategoryOfPlant)
                //.Include(p => p.LastUpdateUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // GET: Plants/Create
        public async Task<IActionResult> Create(int CategoryID)
        {
            if (CategoryID < 1)
                return BadRequest();
            PlantsCategory? plantCategory = new PlantsCategory();
            if (await _context.PlantsCategories.AnyAsync(x => x.ID == CategoryID))
                plantCategory = await _context.PlantsCategories.Where(predicate: x => x.ID == CategoryID).FirstOrDefaultAsync();
            else
                return NotFound();
            ViewData[nameof(CategoryID)] = CategoryID;
            ViewData["PlantCategoryID"] = new SelectList(_context.PlantsCategories, "ID", "CategoryNameEn");
            //ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail");
            Plant plant = new Plant();
            plant.PlantCategoryID = CategoryID;
            int PlantNo = 0;
            if (await _context.Plants.AnyAsync(x => x.PlantCategoryID == CategoryID))
                PlantNo = _context.Plants.Where(x => x.PlantCategoryID == CategoryID).Max(x => x.PlantNo);
            plant.PlantNo = ++PlantNo;
            plant.PlantCode = $"{plantCategory?.Code}{PlantNo}";
            plant.PlantFullCode = $"{plantCategory?.FullCode}{PlantNo}";
            return View(plant);
        }

        // POST: Plants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantNo,PlantCode,PlantNameEn,PlantNameAr,PlantDescription,PlantCategoryID,PlantFullCode,ID,LastUserID,LastUpdateDate")] Plant plant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantCategoryID"] = new SelectList(_context.PlantsCategories, "ID", "CategoryNameEn", plant.PlantCategoryID);
            //ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", plant.LastUserID);
            return View(plant);
        }

        // GET: Plants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plants == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            ViewData["PlantCategoryID"] = new SelectList(_context.PlantsCategories, "ID", "CategoryNameEn", plant.PlantCategoryID);
            //ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", plant.LastUserID);
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlantNo,PlantCode,PlantNameEn,PlantNameAr,PlantDescription,PlantCategoryID,PlantFullCode,ID,LastUserID,LastUpdateDate")] Plant plant)
        {
            if (id != plant.ID)
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
                    if (!PlantExists(plant.ID))
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
            ViewData["PlantCategoryID"] = new SelectList(_context.PlantsCategories, "ID", "CategoryNameEn", plant.PlantCategoryID);
            //ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", plant.LastUserID);
            return View(plant);
        }

        // GET: Plants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Plants == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants
                .Include(p => p.CategoryOfPlant)
                //.Include(p => p.LastUpdateUser)
                .FirstOrDefaultAsync(m => m.ID == id);
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
            if (_context.Plants == null)
            {
                return Problem("Entity set 'ProjectsMeasurementsDBContext.Plants'  is null.");
            }
            var plant = await _context.Plants.FindAsync(id);
            if (plant != null)
            {
                _context.Plants.Remove(plant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(int id)
        {
            return (_context.Plants?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
