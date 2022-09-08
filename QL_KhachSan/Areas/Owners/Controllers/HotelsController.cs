using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QL_KhachSan.ModelDbs;

namespace QL_KhachSan.Areas.Owners.Controllers
{
    [Area("Owners")]
    [Route("Owners/Hotels")]
    public class HotelsController : Controller
    {
        private readonly QL_KhachSanContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _appEnvironment;

        [Obsolete]
        public HotelsController(QL_KhachSanContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Owners/Hotels
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Hotels.Include(h => h.Point).Include(h => h.Ward).Where(h => h.OwnerId.ToString() == HttpContext.Session.GetString("Own_ID"));
            return View(await qL_KhachSanContext.ToListAsync());
        }


        // GET: Owners/Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.Point)
                .Include(h => h.Ward)
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        // GET: Owners/Hotels/Create
        [Route("create")]
        public IActionResult Create()
        {

            ViewData["PointId"] = new SelectList(_context.Points, "PointId", "PointId");
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "ProvinceId", "ProvinceName");
            ViewData["WardId"] = new SelectList(_context.Wards, "WardId", "WardName");
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictName");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerName");
            return View();
        }

        // POST: Owners/Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Create([Bind("HotelId,ProvinceId,DistrictId,WardId,PointId,HotelName,HotelAvt")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        //There is an error here
                        var uploads = Path.Combine(_appEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                hotel.HotelAvt = fileName;
                            }

                        }
                    }
                }
                var p = _context.Points.OrderByDescending(x => x.PointId).First();
                hotel.PointId = p.PointId;
                hotel.OwnerId = int.Parse(HttpContext.Session.GetString("Own_ID"));
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));



            }
            ViewData["PointId"] = new SelectList(_context.Points, "PointId", "PointId", hotel.PointId);
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "ProvinceId", "ProvinceName", hotel.ProvinceId);
            ViewData["WardId"] = new SelectList(_context.Wards, "WardId", "WardName", hotel.WardId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "DistrictId", "DistrictName", hotel.DistrictId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "OwnerId", "OwnerName", hotel.OwnerId);
            return View(hotel);
        }

        // GET: Owners/Hotels/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            ViewData["PointId"] = new SelectList(_context.Points, "PointId", "PointId", hotel.PointId);
            ViewData["ProvinceId"] = new SelectList(_context.Wards, "ProvinceId", "ProvinceId", hotel.ProvinceId);
            return View(hotel);
        }

        // POST: Owners/Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("HotelId,ProvinceId,DistrictId,WardId,PointId,HotelName,HotelAvt")] Hotel hotel)
        {
            if (id != hotel.HotelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.HotelId))
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
            ViewData["PointId"] = new SelectList(_context.Points, "PointId", "PointId", hotel.PointId);
            ViewData["ProvinceId"] = new SelectList(_context.Wards, "ProvinceId", "ProvinceId", hotel.ProvinceId);
            return View(hotel);
        }

        // GET: Owners/Hotels/Delete/5
        [Route("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels
                .Include(h => h.Point)
                .Include(h => h.Ward)
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }
        [Route("")]
        [Route("delete")]

        // POST: Owners/Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.HotelId == id);
        }
        [Route("GetDataPro")]
        public JsonResult GetDataPro()
        {
            var pro = _context.Provinces.ToList();
            return Json(pro);
        }
        [Route("GetDataDis")]
        public JsonResult GetDataDis()
        {
            var dis = _context.Districts.ToList();
            return Json(dis);
        }
        [Route("GetDataWar")]
        public JsonResult GetDataWar()
        {
            var war = _context.Wards.ToList();
            return Json(war);
        }

        [Route("GetIdPro")]
        public JsonResult GetIdPro(string name)
        {
            var pro = _context.Provinces.Where(p => p.ProvinceName == name).SingleOrDefault();
            return Json(pro);
        }
        [Route("GetIdDis")]
        public JsonResult GetIdDis(string name)
        {
            var dis = _context.Districts.Where(p => p.DistrictName == name).SingleOrDefault();
            return Json(dis);
        }
        [Route("GetIdWar")]
        public JsonResult GetIdWar(string name)
        {
            var war = _context.Wards.Where(p => p.WardName == name).FirstOrDefault();
            return Json(war);
        }
        [Route("")]
        [Route("CreatePoint")]
        public IActionResult CreatePoint()
        {
            return View();
        }
        [Route("AddPoint")]

        public Point AddPoint(float x, float y)
        {
            Point p = new Point();
            p.PointX = x;
            p.PointY = y;
            try
            {
                _context.Add(p);
                _context.SaveChanges();
                TempData["pop"] = "<script>alert('Chỉ được tải lên 2 hình chi tiết!');</script>";
            }
            catch (Exception ex)
            {

            }
            return p;
        }
        [Route("popup")]
        public IActionResult Popup()
        {
            return View();
        }
    }
}
