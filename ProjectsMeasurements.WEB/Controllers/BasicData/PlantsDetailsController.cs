using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Models.Operations;

namespace ProjectsMeasurements.WEB.Controllers.BasicData
{
    public class PlantsDetailsController : Controller
    {
        private readonly ProjectsMeasurementsDBContext _context;

        public PlantsDetailsController(ProjectsMeasurementsDBContext context)
        {
            _context = context;
        }

        // GET: PlantsDetails
        public async Task<IActionResult> Index(int PlantID)
        {
            if (PlantID < 1)
                return BadRequest();
            var projectsMeasurementsDBContext = _context.PlantsDetails.Where(x=>x.PlantID == PlantID).Include(p => p.LastUpdateUser).Include(p => p.ParentPlant).Include(p => p.PlantHeightUnit);
            return View(await projectsMeasurementsDBContext.ToListAsync());
        }

        // GET: PlantsDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlantsDetails == null)
            {
                return NotFound();
            }

            var plantsDetail = await _context.PlantsDetails
                .Include(p => p.LastUpdateUser)
                .Include(p => p.ParentPlant)
                .Include(p => p.PlantHeightUnit)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plantsDetail == null)
            {
                return NotFound();
            }

            return View(plantsDetail);
        }

        // GET: PlantsDetails/Create
        public IActionResult Create()
        {
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail");
            ViewData["PlantID"] = new SelectList(_context.Plants, "ID", "PlantCode");
            ViewData["PlantHeihgtUnitID"] = new SelectList(_context.Units, "ID", "UnitNameEn");
            return View();
        }

        // POST: PlantsDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantID,PlantHeight,PlantHeihgtUnitID,PlantTrunkWidth,PlantDefaultPrice,ID,LastUserID,LastUpdateDate")] PlantsDetail plantsDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plantsDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", plantsDetail.LastUserID);
            ViewData["PlantID"] = new SelectList(_context.Plants, "ID", "PlantCode", plantsDetail.PlantID);
            ViewData["PlantHeihgtUnitID"] = new SelectList(_context.Units, "ID", "UnitNameEn", plantsDetail.PlantHeihgtUnitID);
            return View(plantsDetail);
        }

        // GET: PlantsDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlantsDetails == null)
            {
                return NotFound();
            }

            var plantsDetail = await _context.PlantsDetails.FindAsync(id);
            if (plantsDetail == null)
            {
                return NotFound();
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", plantsDetail.LastUserID);
            ViewData["PlantID"] = new SelectList(_context.Plants, "ID", "PlantCode", plantsDetail.PlantID);
            ViewData["PlantHeihgtUnitID"] = new SelectList(_context.Units, "ID", "UnitNameEn", plantsDetail.PlantHeihgtUnitID);
            return View(plantsDetail);
        }

        // POST: PlantsDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlantID,PlantHeight,PlantHeihgtUnitID,PlantTrunkWidth,PlantDefaultPrice,ID,LastUserID,LastUpdateDate")] PlantsDetail plantsDetail)
        {
            if (id != plantsDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantsDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantsDetailExists(plantsDetail.ID))
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
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", plantsDetail.LastUserID);
            ViewData["PlantID"] = new SelectList(_context.Plants, "ID", "PlantCode", plantsDetail.PlantID);
            ViewData["PlantHeihgtUnitID"] = new SelectList(_context.Units, "ID", "UnitNameEn", plantsDetail.PlantHeihgtUnitID);
            return View(plantsDetail);
        }

        // GET: PlantsDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlantsDetails == null)
            {
                return NotFound();
            }

            var plantsDetail = await _context.PlantsDetails
                .Include(p => p.LastUpdateUser)
                .Include(p => p.ParentPlant)
                .Include(p => p.PlantHeightUnit)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (plantsDetail == null)
            {
                return NotFound();
            }

            return View(plantsDetail);
        }

        // POST: PlantsDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlantsDetails == null)
            {
                return Problem("Entity set 'ProjectsMeasurementsDBContext.PlantsDetails'  is null.");
            }
            var plantsDetail = await _context.PlantsDetails.FindAsync(id);
            if (plantsDetail != null)
            {
                _context.PlantsDetails.Remove(plantsDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantsDetailExists(int id)
        {
          return (_context.PlantsDetails?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
