using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Models.Operations;

namespace ProjectsMeasurements.WEB.Controllers
{
    public class MeasurementsDetailsController : Controller
    {
        private readonly ProjectsMeasurementsDBContext _context;

        public MeasurementsDetailsController(ProjectsMeasurementsDBContext context)
        {
            _context = context;
        }

        // GET: MeasurementsDetails
        public async Task<IActionResult> Index()
        {
            var projectsMeasurementsDBContext = _context.MeasurementsDetails.Include(m => m.LastUpdateUser).Include(m => m.MeasurementHeader).Include(m => m.MeasurmentPlantDetail);
            return View(await projectsMeasurementsDBContext.ToListAsync());
        }

        // GET: MeasurementsDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeasurementsDetails == null)
            {
                return NotFound();
            }

            var measurementsDetail = await _context.MeasurementsDetails
                .Include(m => m.LastUpdateUser)
                .Include(m => m.MeasurementHeader)
                .Include(m => m.MeasurmentPlantDetail)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (measurementsDetail == null)
            {
                return NotFound();
            }

            return View(measurementsDetail);
        }

        // GET: MeasurementsDetails/Create
        public IActionResult Create()
        {
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail");
            ViewData["MeasurementHeaderID"] = new SelectList(_context.MeasurementsHeaders, "ID", "ID");
            ViewData["PlantDetailID"] = new SelectList(_context.PlantsDetails, "ID", "ID");
            return View();
        }

        // POST: MeasurementsDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeasurementHeaderID,PlantDetailID,PlantQuantity,PlantUnitPrice,PlantTotalPrice,Remarks,ID,LastUserID,LastUpdateDate")] MeasurementsDetail measurementsDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(measurementsDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", measurementsDetail.LastUserID);
            ViewData["MeasurementHeaderID"] = new SelectList(_context.MeasurementsHeaders, "ID", "ID", measurementsDetail.MeasurementHeaderID);
            ViewData["PlantDetailID"] = new SelectList(_context.PlantsDetails, "ID", "ID", measurementsDetail.PlantDetailID);
            return View(measurementsDetail);
        }

        // GET: MeasurementsDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeasurementsDetails == null)
            {
                return NotFound();
            }

            var measurementsDetail = await _context.MeasurementsDetails.FindAsync(id);
            if (measurementsDetail == null)
            {
                return NotFound();
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", measurementsDetail.LastUserID);
            ViewData["MeasurementHeaderID"] = new SelectList(_context.MeasurementsHeaders, "ID", "ID", measurementsDetail.MeasurementHeaderID);
            ViewData["PlantDetailID"] = new SelectList(_context.PlantsDetails, "ID", "ID", measurementsDetail.PlantDetailID);
            return View(measurementsDetail);
        }

        // POST: MeasurementsDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeasurementHeaderID,PlantDetailID,PlantQuantity,PlantUnitPrice,PlantTotalPrice,Remarks,ID,LastUserID,LastUpdateDate")] MeasurementsDetail measurementsDetail)
        {
            if (id != measurementsDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(measurementsDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasurementsDetailExists(measurementsDetail.ID))
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
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", measurementsDetail.LastUserID);
            ViewData["MeasurementHeaderID"] = new SelectList(_context.MeasurementsHeaders, "ID", "ID", measurementsDetail.MeasurementHeaderID);
            ViewData["PlantDetailID"] = new SelectList(_context.PlantsDetails, "ID", "ID", measurementsDetail.PlantDetailID);
            return View(measurementsDetail);
        }

        // GET: MeasurementsDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeasurementsDetails == null)
            {
                return NotFound();
            }

            var measurementsDetail = await _context.MeasurementsDetails
                .Include(m => m.LastUpdateUser)
                .Include(m => m.MeasurementHeader)
                .Include(m => m.MeasurmentPlantDetail)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (measurementsDetail == null)
            {
                return NotFound();
            }

            return View(measurementsDetail);
        }

        // POST: MeasurementsDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeasurementsDetails == null)
            {
                return Problem("Entity set 'ProjectsMeasurementsDBContext.MeasurementsDetails'  is null.");
            }
            var measurementsDetail = await _context.MeasurementsDetails.FindAsync(id);
            if (measurementsDetail != null)
            {
                _context.MeasurementsDetails.Remove(measurementsDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasurementsDetailExists(int id)
        {
          return (_context.MeasurementsDetails?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
