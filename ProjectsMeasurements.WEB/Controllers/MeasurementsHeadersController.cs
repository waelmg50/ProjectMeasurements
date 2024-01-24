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
    public class MeasurementsHeadersController : Controller
    {
        private readonly ProjectsMeasurementsDBContext _context;

        public MeasurementsHeadersController(ProjectsMeasurementsDBContext context)
        {
            _context = context;
        }

        // GET: MeasurementsHeaders
        public async Task<IActionResult> Index(int MeasurementTypeID)
        {
            var projectsMeasurementsDBContext = _context.MeasurementsHeaders.Include(m => m.LastUpdateUser).Include(m => m.MeasurementContractor).Include(m => m.MeasurementOwner).Include(m => m.MeasurementProject).Where(x=>x.MeasurementTypeID == MeasurementTypeID);
            ViewBag.MeasurementTypeID = MeasurementTypeID;
            return View(await projectsMeasurementsDBContext.ToListAsync());
        }

        // GET: MeasurementsHeaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeasurementsHeaders == null)
            {
                return NotFound();
            }

            var measurementsHeader = await _context.MeasurementsHeaders
                .Include(m => m.LastUpdateUser)
                .Include(m => m.MeasurementContractor)
                .Include(m => m.MeasurementOwner)
                .Include(m => m.MeasurementProject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (measurementsHeader == null)
            {
                return NotFound();
            }

            return View(measurementsHeader);
        }

        // GET: MeasurementsHeaders/Create
        public IActionResult Create(int MeasurementTypeID)
        {
            //ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail");
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ContractorNameEn");
            ViewData["OwnerID"] = new SelectList(_context.Owners, "ID", "OwnerNameEn");
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Code");
            var list = _context.MeasurementsHeaders.Where(x => x.MeasurementDate.Year == DateTime.Now.Year && x.MeasurementTypeID == MeasurementTypeID);
            int iCurrentCode = 0;
            if (list.Any())
                iCurrentCode = list.Max(x => x.MeasurementCode);
            MeasurementsHeader measurementHeader = new MeasurementsHeader();
            measurementHeader.MeasurementCode = ++iCurrentCode;
            measurementHeader.MeasurementDate = DateTime.Now;
            measurementHeader.MeasurementTypeID = MeasurementTypeID;
            return View(measurementHeader);
        }

        // POST: MeasurementsHeaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeasurementCode,MeasurementDate,ProjectID,OwnerID,ContractorID,MeasurementTypeID,MeasurementTotalPrice,Remarks,ID,LastUserID,LastUpdateDate")] MeasurementsHeader measurementsHeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(measurementsHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { MeasurementTypeID = measurementsHeader.MeasurementTypeID });
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", measurementsHeader.LastUserID);
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ContractorNameEn", measurementsHeader.ContractorID);
            ViewData["OwnerID"] = new SelectList(_context.Owners, "ID", "OwnerNameEn", measurementsHeader.OwnerID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Code", measurementsHeader.ProjectID);
            return View(measurementsHeader);
        }

        // GET: MeasurementsHeaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeasurementsHeaders == null)
            {
                return NotFound();
            }

            var measurementsHeader = await _context.MeasurementsHeaders.FindAsync(id);
            if (measurementsHeader == null)
            {
                return NotFound();
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", measurementsHeader.LastUserID);
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ContractorNameEn", measurementsHeader.ContractorID);
            ViewData["OwnerID"] = new SelectList(_context.Owners, "ID", "OwnerNameEn", measurementsHeader.OwnerID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Code", measurementsHeader.ProjectID);
            return View(measurementsHeader);
        }

        // POST: MeasurementsHeaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeasurementCode,MeasurementDate,ProjectID,OwnerID,ContractorID,MeasurementTotalPrice,Remarks,ID,LastUserID,LastUpdateDate")] MeasurementsHeader measurementsHeader)
        {
            if (id != measurementsHeader.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(measurementsHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasurementsHeaderExists(measurementsHeader.ID))
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
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", measurementsHeader.LastUserID);
            ViewData["ContractorID"] = new SelectList(_context.Contractors, "ID", "ContractorNameEn", measurementsHeader.ContractorID);
            ViewData["OwnerID"] = new SelectList(_context.Owners, "ID", "OwnerNameEn", measurementsHeader.OwnerID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Code", measurementsHeader.ProjectID);
            return View(measurementsHeader);
        }

        // GET: MeasurementsHeaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeasurementsHeaders == null)
            {
                return NotFound();
            }

            var measurementsHeader = await _context.MeasurementsHeaders
                .Include(m => m.LastUpdateUser)
                .Include(m => m.MeasurementContractor)
                .Include(m => m.MeasurementOwner)
                .Include(m => m.MeasurementProject)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (measurementsHeader == null)
            {
                return NotFound();
            }

            return View(measurementsHeader);
        }

        // POST: MeasurementsHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeasurementsHeaders == null)
            {
                return Problem("Entity set 'ProjectsMeasurementsDBContext.MeasurementsHeaders'  is null.");
            }
            var measurementsHeader = await _context.MeasurementsHeaders.FindAsync(id);
            if (measurementsHeader != null)
            {
                _context.MeasurementsHeaders.Remove(measurementsHeader);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasurementsHeaderExists(int id)
        {
            return (_context.MeasurementsHeaders?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
