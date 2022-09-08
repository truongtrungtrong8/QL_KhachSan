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
    public class OwnersController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public OwnersController(QL_KhachSanContext context)
        {
            _context = context;
        }

        // GET: Admin/Owners
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Owners.Include(o => o.AccountUsernameNavigation);
            return View(await qL_KhachSanContext.ToListAsync());
        }
        public int CountHotels(int idOwner)
        {
            var data = _context.Hotels.Where(h => h.OwnerId == idOwner).ToList();
            return data.Count;

        }
    }
}
