using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QL_KhachSan.ModelDbs;

namespace QL_KhachSan.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Route("Customers/Customers")]
    public class CustomersController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public CustomersController(QL_KhachSanContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("Routing")]
        public IActionResult Routing(double x, double y)
        {
            HttpContext.Session.SetString("X", x.ToString());
            HttpContext.Session.SetString("Y", y.ToString());
            return View();
        }
        // GET: Customers/Customers
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Customers.Include(c => c.AccountUsernameNavigation);
            return View(await qL_KhachSanContext.ToListAsync());
        }

        // GET: Customers/Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.AccountUsernameNavigation)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Customers/Create
        [Route("create")]
        public IActionResult Create()
        {
            ViewData["AccountUsername"] = new SelectList(_context.Accounts, "AccountUsername", "AccountUsername");
            return View();
        }

        // POST: Customers/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("CustomerId,AccountUsername,CustomerName,CustomerPhone,CustomerAddress,CustomerEmail,CustomerSex,CustomerDateofbirth,CustomerBankaccount")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CustomerPhone = customer.AccountUsername;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Cus_ID", customer.CustomerId.ToString());
                return Redirect("/Customers/Home/Index");
            }
            ViewData["AccountUsername"] = new SelectList(_context.Accounts, "AccountUsername", "AccountUsername", customer.AccountUsername);
            
            return View(customer);
        }

        // GET: Customers/Customers/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AccountUsername"] = new SelectList(_context.Accounts, "AccountUsername", "AccountUsername", customer.AccountUsername);
            return View(customer);
        }

        // POST: Customers/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,AccountUsername,CustomerName,CustomerPhone,CustomerAddress,CustomerEmail,CustomerSex,CustomerDateofbirth,CustomerBankaccount")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            ViewData["AccountUsername"] = new SelectList(_context.Accounts, "AccountUsername", "AccountUsername", customer.AccountUsername);
            return View(customer);
        }

        // GET: Customers/Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.AccountUsernameNavigation)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        public IActionResult Businessregistration(int id)
        {
            var Cus = _context.Customers.Find(id);
            Cus.CustomerPhone = "1";
            _context.SaveChanges();
            return Redirect("/Customers/Home/Index");
        }
        [Route("Find_Around")]
        public IActionResult Find_Around()
        {
            return View();
        }


    }
}
