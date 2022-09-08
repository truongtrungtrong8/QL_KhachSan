using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QL_KhachSan.ModelDbs;
using OfficeOpenXml;
using System.Globalization;

namespace QL_KhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public HomeController(QL_KhachSanContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            int res = 0;
            var data = _context.Businessregistrations.Where(b => b.BrStatus == true).ToList();
            foreach (var item in data)
            {
                if (item.PricelistId == 1)
                {
                    res += 500000;
                }
                if (item.PricelistId == 2)
                {
                    res += 2700000;
                }
                if (item.PricelistId == 3)
                {
                    res += 6500000;
                }
            }
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            ViewBag.TotalPrice = res.ToString("#,###", cul.NumberFormat);

            return View();
        }

        public void CountTurnover()
        {

        }
    }
}
