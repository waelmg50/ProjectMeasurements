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
    public class ContractorsController : Controller
    {
        private readonly ProjectsMeasurementsDBContext _context;

        public ContractorsController(ProjectsMeasurementsDBContext context)
        {
            _context = context;
        }

        // GET: Contractors
        public async Task<IActionResult> Index()
        {
            var projectsMeasurementsDBContext = _context.Contractors.Include(c => c.LastUpdateUser);
            return View(await projectsMeasurementsDBContext.ToListAsync());
        }

        // GET: Contractors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contractors == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors
                .Include(c => c.LastUpdateUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // GET: Contractors/Create
        public IActionResult Create()
        {
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail");
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractorNameEn,ContractorNameAr,ID,LastUserID,LastUpdateDate")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", contractor.LastUserID);
            return View(contractor);
        }

        // GET: Contractors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contractors == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", contractor.LastUserID);
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractorNameEn,ContractorNameAr,ID,LastUserID,LastUpdateDate")] Contractor contractor)
        {
            if (id != contractor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractorExists(contractor.ID))
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
            ViewData["LastUserID"] = new SelectList(_context.Users, "ID", "UserEmail", contractor.LastUserID);
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contractors == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors
                .Include(c => c.LastUpdateUser)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contractors == null)
            {
                return Problem("Entity set 'ProjectsMeasurementsDBContext.Contractors'  is null.");
            }
            var contractor = await _context.Contractors.FindAsync(id);
            if (contractor != null)
            {
                _context.Contractors.Remove(contractor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractorExists(int id)
        {
          return (_context.Contractors?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
