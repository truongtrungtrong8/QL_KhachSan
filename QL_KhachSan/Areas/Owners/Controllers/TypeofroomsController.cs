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
    [Route("Owners/Typeofrooms")]
    public class TypeofroomsController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public TypeofroomsController(QL_KhachSanContext context)
        {
            _context = context;
        }

        // GET: Owners/Typeofrooms
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Typeofrooms.Include(t => t.Owner).Where(t=>t.OwnerId.ToString() == HttpContext.Session.GetString("Own_ID"));
            return View(await qL_KhachSanContext.ToListAsync());
        }

        // GET: Owners/Typeofrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeofroom = await _context.Typeofrooms
                .Include(t => t.Owner)
                .FirstOrDefaultAsync(m => m.TorId == id);
            if (typeofroom == null)
            {
                return NotFound();
            }

            return View(typeofroom);
        }

        // GET: Owners/Typeofrooms/Create
        [Route("Create")]
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId");
            return View();
        }

        // POST: Owners/Typeofrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("TorId,OwnerId,TorName,TorPrice")] Typeofroom typeofroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeofroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", typeofroom.OwnerId);
            return View(typeofroom);
        }

        // GET: Owners/Typeofrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeofroom = await _context.Typeofrooms.FindAsync(id);
            if (typeofroom == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", typeofroom.OwnerId);
            return View(typeofroom);
        }

        // POST: Owners/Typeofrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TorId,OwnerId,TorName,TorPrice")] Typeofroom typeofroom)
        {
            if (id != typeofroom.TorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeofroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeofroomExists(typeofroom.TorId))
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
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerId", typeofroom.OwnerId);
            return View(typeofroom);
        }

        // GET: Owners/Typeofrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeofroom = await _context.Typeofrooms
                .Include(t => t.Owner)
                .FirstOrDefaultAsync(m => m.TorId == id);
            if (typeofroom == null)
            {
                return NotFound();
            }

            return View(typeofroom);
        }

        // POST: Owners/Typeofrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeofroom = await _context.Typeofrooms.FindAsync(id);
            _context.Typeofrooms.Remove(typeofroom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeofroomExists(int id)
        {
            return _context.Typeofrooms.Any(e => e.TorId == id);
        }
    }
}
