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
    [Route("Owners/Owners")]
    public class OwnersController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public OwnersController(QL_KhachSanContext context)
        {
            _context = context;
        }

        // GET: Owners/Owners
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Owners.Include(o => o.AccountUsernameNavigation);
            return View(await qL_KhachSanContext.ToListAsync());
        }

        // GET: Owners/Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Include(o => o.AccountUsernameNavigation)
                .FirstOrDefaultAsync(m => m.OwnerId == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Owners/Create
        [Route("create")]
        public IActionResult Create()
        {
            ViewData["AccountUsername"] = new SelectList(_context.Accounts, "AccountUsername", "AccountUsername");
            return View();
        }

        // POST: Owners/Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerId,AccountUsername,OwnerName,OwnerPhone,OwnerAddress,OwnerEmail,OwnerSex,OwnerDateofbirth,OwnerBankaccount")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(owner);
                owner.AccountUsername = HttpContext.Session.GetString("Own_Phone");
                owner.OwnerPhone = HttpContext.Session.GetString("Own_Phone");
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Own_ID", owner.OwnerId.ToString());
                HttpContext.Session.SetString("Own_Phone", owner.OwnerPhone);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountUsername"] = new SelectList(_context.Accounts, "AccountUsername", "AccountUsername", owner.AccountUsername);
            return View(owner);
        }

        // GET: Owners/Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            ViewData["AccountUsername"] = new SelectList(_context.Accounts, "AccountUsername", "AccountUsername", owner.AccountUsername);
            ViewData["AccountPhone"] = _context.Accounts.Where(a => a.AccountUsername == HttpContext.Session.GetString("Phone")).Select(a=>a.AccountUsername).SingleOrDefault();
            return View(owner);
        }

        // POST: Owners/Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OwnerId,AccountUsername,OwnerName,OwnerPhone,OwnerAddress,OwnerEmail,OwnerSex,OwnerDateofbirth,OwnerBankaccount")] Owner owner)
        {
            if (id != owner.OwnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnerExists(owner.OwnerId))
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
            ViewData["AccountUsername"] = new SelectList(_context.Accounts, "AccountUsername", "AccountUsername", owner.AccountUsername);
            return View(owner);
        }

        // GET: Owners/Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Include(o => o.AccountUsernameNavigation)
                .FirstOrDefaultAsync(m => m.OwnerId == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.OwnerId == id);
        }
    }
}
