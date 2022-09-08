using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QL_KhachSan.ModelDbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QL_KhachSan.Areas.Customers
{
    [Area("Customers")]
    public class HomeController : Controller
    {
        public QL_KhachSanContext _context = new QL_KhachSanContext();
        public IActionResult Index()
        {
            var data = _context.Customers.Where(c => c.CustomerId.ToString() == HttpContext.Session.GetString("Cus_ID")).SingleOrDefault();
            return View(data);
        }
    } 
 
}
