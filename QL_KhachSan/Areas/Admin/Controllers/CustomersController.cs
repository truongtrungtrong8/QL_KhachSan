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
    public class CustomersController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public CustomersController(QL_KhachSanContext context)
        {
            _context = context;
        }

        // GET: Admin/Customers
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Customers.Include(c => c.AccountUsernameNavigation);
            return View(await qL_KhachSanContext.ToListAsync());
        }

    }
}
