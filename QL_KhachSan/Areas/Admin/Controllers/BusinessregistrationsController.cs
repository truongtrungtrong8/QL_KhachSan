using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QL_KhachSan.ModelDbs;

namespace QL_KhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BusinessregistrationsController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public BusinessregistrationsController(QL_KhachSanContext context)
        {
            _context = context;
        }

        // GET: Admin/Businessregistrations
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Businessregistrations.Include(b => b.Hotel).Include(b => b.Owner).Include(b => b.Pricelist);
            return View(await qL_KhachSanContext.ToListAsync());
        }

        // GET: Admin/Businessregistrations/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessregistration = await _context.Businessregistrations.FindAsync(id);
            if (businessregistration == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "DistrictId", businessregistration.HotelId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", businessregistration.OwnerId);
            ViewData["PricelistId"] = new SelectList(_context.Pricelists, "PricelistId", "PricelistId", businessregistration.PricelistId);
            return View(businessregistration);
        }

        // POST: Admin/Businessregistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("BrId,HotelId,PricelistId,OwnerId,BrStatus,BrDate")] Businessregistration businessregistration)
        {
            if (id != businessregistration.BrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessregistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessregistrationExists(businessregistration.BrId))
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
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "DistrictId", businessregistration.HotelId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", businessregistration.OwnerId);
            ViewData["PricelistId"] = new SelectList(_context.Pricelists, "PricelistId", "PricelistId", businessregistration.PricelistId);
            return View(businessregistration);
        }

        // GET: Admin/Businessregistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessregistration = await _context.Businessregistrations
                .Include(b => b.Hotel)
                .Include(b => b.Owner)
                .Include(b => b.Pricelist)
                .FirstOrDefaultAsync(m => m.BrId == id);
            if (businessregistration == null)
            {
                return NotFound();
            }

            return View(businessregistration);
        }

        // POST: Admin/Businessregistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessregistration = await _context.Businessregistrations.FindAsync(id);
            _context.Businessregistrations.Remove(businessregistration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessregistrationExists(int id)
        {
            return _context.Businessregistrations.Any(e => e.BrId == id);
        }
    }
}
