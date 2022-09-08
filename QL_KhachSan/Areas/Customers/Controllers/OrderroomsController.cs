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
    public class OrderroomsController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public OrderroomsController(QL_KhachSanContext context)
        {
            _context = context;
        }

        // GET: Customers/Orderrooms
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Orderrooms.Include(o => o.Customer).Include(o => o.Room).Include(o=>o.Hotel).Include(o=>o.Hotel.Point).Where(o=>o.CustomerId.ToString() == HttpContext.Session.GetString("Cus_ID"));
            return View(await qL_KhachSanContext.ToListAsync());
        }

        // GET: Customers/Orderrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderroom = await _context.Orderrooms
                .Include(o => o.Customer)
                .Include(o => o.Room)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderroom == null)
            {
                return NotFound();
            }

            return View(orderroom);
        }
        private bool OrderroomExists(int id)
        {
            return _context.Orderrooms.Any(e => e.OrderId == id);
        }
    }
}
