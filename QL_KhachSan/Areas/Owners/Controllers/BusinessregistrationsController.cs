using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QL_KhachSan.ModelDbs;

namespace QL_KhachSan.Areas.Owners.Controllers
{
    [Area("Owners")]
    [Route("Owners/Businessregistrations")]
    public class BusinessregistrationsController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public BusinessregistrationsController(QL_KhachSanContext context)
        {
            _context = context;
        }

        // GET: Owners/Businessregistrations
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Businessregistrations.Include(b => b.Hotel).Include(b => b.Owner).Include(b => b.Pricelist);
            return View(await qL_KhachSanContext.ToListAsync());
        }

        // GET: Owners/Businessregistrations/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Owners/Businessregistrations/Create
        [Route("create")]
        public IActionResult Create()
        {
            var res = _context.Businessregistrations.Select(r => r.HotelId).ToList();
            ViewData["HotelId"] = new SelectList(_context.Hotels.Where(h=> !res.Contains(h.HotelId) & h.OwnerId.ToString()==HttpContext.Session.GetString("Own_ID")), "HotelId", "HotelName");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId");
            ViewData["PricelistId"] = new SelectList(_context.Pricelists, "PricelistId", "PricelistName");
            return View();
        }

        // POST: Owners/Businessregistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrId,HotelId,PricelistId,OwnerId,BrStatus,BrDate")] Businessregistration businessregistration)
        {
            if (ModelState.IsValid)
            {
                businessregistration.BrDate = DateTime.Now;
                businessregistration.BrStatus = true;
                _context.Add(businessregistration);
                await _context.SaveChangesAsync();
                return Redirect("/Owners/Businessregistrations/Index");
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "DistrictId", businessregistration.HotelId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", businessregistration.OwnerId);
            ViewData["PricelistId"] = new SelectList(_context.Pricelists, "PricelistId", "PricelistId", businessregistration.PricelistId);
            return Redirect("/Owners/Businessregistrations/Index");
        }

        // GET: Owners/Businessregistrations/Edit/5
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

        // POST: Owners/Businessregistrations/Edit/5
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

        // GET: Owners/Businessregistrations/Delete/5
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

        // POST: Owners/Businessregistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var businessregistration = await _context.Businessregistrations.FindAsync(id);
            _context.Businessregistrations.Remove(businessregistration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Route("BusinessregistrationExists")]
        private bool BusinessregistrationExists(int id)
        {
            return _context.Businessregistrations.Any(e => e.BrId == id);
        }
    }
}
