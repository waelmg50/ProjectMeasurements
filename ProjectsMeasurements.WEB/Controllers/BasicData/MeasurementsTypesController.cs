using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Models.BasicData;

namespace ProjectsMeasurements.WEB.Controllers.BasicData
{
    public class MeasurementsTypesController : Controller
    {
        private readonly ProjectsMeasurementsDBContext _context;

        public MeasurementsTypesController(ProjectsMeasurementsDBContext context)
        {
            _context = context;
        }

        // GET: MeasurementsTypes
        public async Task<IActionResult> Index()
        {
            var projectsMeasurementsDBContext = _context.MeasurementsTypes.Include(m => m.LastUpdateUser);
            return View(await projectsMeasurementsDBContext.ToListAsync());
        }

        // GET: MeasurementsTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeasurementsTypes == null)
            {
                return NotFound();
            }

            var measurementsType = await _context.MeasurementsTypes
                .Include(m => m.LastUpdateUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (measurementsType == null)
            {
                return NotFound();
            }

            return View(measurementsType);
        }

        // GET: MeasurementsTypes/Create
        public IActionResult Create()
        {
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail");
            return View();
        }

        // POST: MeasurementsTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeNameEn,TypeNameAr,ID,LastUserID,LastUpdateDate")] MeasurementsType measurementsType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(measurementsType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", measurementsType.LastUserID);
            return View(measurementsType);
        }

        // GET: MeasurementsTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeasurementsTypes == null)
            {
                return NotFound();
            }

            var measurementsType = await _context.MeasurementsTypes.FindAsync(id);
            if (measurementsType == null)
            {
                return NotFound();
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", measurementsType.LastUserID);
            return View(measurementsType);
        }

        // POST: MeasurementsTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeNameEn,TypeNameAr,ID,LastUserID,LastUpdateDate")] MeasurementsType measurementsType)
        {
            if (id != measurementsType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(measurementsType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasurementsTypeExists(measurementsType.ID))
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
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", measurementsType.LastUserID);
            return View(measurementsType);
        }

        // GET: MeasurementsTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeasurementsTypes == null)
            {
                return NotFound();
            }

            var measurementsType = await _context.MeasurementsTypes
                .Include(m => m.LastUpdateUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (measurementsType == null)
            {
                return NotFound();
            }

            return View(measurementsType);
        }

        // POST: MeasurementsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeasurementsTypes == null)
            {
                return Problem("Entity set 'ProjectsMeasurementsDBContext.MeasurementsTypes'  is null.");
            }
            var measurementsType = await _context.MeasurementsTypes.FindAsync(id);
            if (measurementsType != null)
            {
                _context.MeasurementsTypes.Remove(measurementsType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasurementsTypeExists(int id)
        {
          return (_context.MeasurementsTypes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
