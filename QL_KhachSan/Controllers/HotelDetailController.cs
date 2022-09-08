using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QL_KhachSan.ModelDbs;
using QL_KhachSan.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QL_KhachSan.Controllers
{
    public class HotelDetailController : Controller
    {
        private readonly QL_KhachSanContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _appEnvironment;
        [Obsolete]
        public HotelDetailController(QL_KhachSanContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> Index(int id)
        {
            HttpContext.Session.SetString("Hotel_ID", id.ToString());
            HttpContext.Session.SetString("Hotel", "1");
            var hotelDetail = new HotelDetail();
            hotelDetail.hotel = await _context.Hotels.FindAsync(id);
            hotelDetail.room = await _context.Rooms.Include(t=>t.Tor).Where(r => r.HotelId == id).ToListAsync();
            hotelDetail.roomimage = await _context.Roomimages.Where(r => r.HotelId == id).ToListAsync();
            hotelDetail.typeofroom = await _context.Typeofrooms.Where(r => r.OwnerId == hotelDetail.hotel.OwnerId).ToListAsync();
            HttpContext.Session.SetString("Hotel_ID", id.ToString());
            return View(hotelDetail);
        }

    }
}
