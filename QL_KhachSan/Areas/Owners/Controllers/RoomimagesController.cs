using System;
using System.Collections.Generic;
using System.IO;
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
    [Route("/Owners/Roomimages")]
    public class RoomimagesController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public RoomimagesController(QL_KhachSanContext context)
        {
            _context = context;
        }

        // GET: Owners/Roomimages
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var res = _context.Hotels.Where(r=>r.OwnerId.ToString() == HttpContext.Session.GetString("Own_ID")).Select(r => r.HotelId).ToList();
            var room = _context.Rooms.Where(h => res.Contains(h.HotelId)).Select(r=>r.RoomId).ToList();
            var qL_KhachSanContext = _context.Roomimages.Where(h=> room.Contains(h.RoomId)).Include(r => r.Room.Hotel);
            return View(await qL_KhachSanContext.ToListAsync());
        }

        // GET: Owners/Roomimages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomimage = await _context.Roomimages
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomimageId == id);
            if (roomimage == null)
            {
                return NotFound();
            }

            return View(roomimage);
        }

        // GET: Owners/Roomimages/Create
        [Route("Create")]
        public IActionResult Create()
        {

            return View();
        }
        [Route("GetAllHotel")]
        public JsonResult GetAllHotel()
        {
            var data = _context.Hotels.Where(h=>h.OwnerId.ToString() == HttpContext.Session.GetString("Own_ID")).ToList();
            return Json(data);
        }
        [Route("GetAllRoom")]
        public JsonResult GetAllRoom()
        {
            var data = _context.Rooms.ToList();
            return Json(data);
        }

        [Route("GetIdRoom")]
        public JsonResult GetIdRoom()
        {
            var data = _context.Rooms.ToList();
            return Json(data);
        }

        [Route("GetIdHotel")]
        public JsonResult GetIdHotel(string name)
        {
            var data = _context.Hotels.Where(r => r.HotelName == name).FirstOrDefault();
            return Json(data);
        }
        // POST: Owners/Roomimages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomimageId,HotelId,RoomId,RoomimageAvt,RoomimageI1,RoomimageI2")] Roomimage roomimage, IFormFile myfile, List<IFormFile> myfiles)
        {
            if (myfile != null)
            {
                string fullPath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", myfile.FileName);
                using (var file = new FileStream(fullPath1, FileMode.Create))
                {
                    myfile.CopyTo(file);
                }
                roomimage.RoomimageAvt = myfile.FileName;
            }
            
            if (myfiles.Count > 2)
            {
                TempData["Them"] = "<script>alert('Chỉ được tải lên 2 hình chi tiết!');</script>";
                return RedirectToAction(nameof(Create));
            }
            else if (myfiles.Count > 0)
            {
                int i = 0;
                string[] fileName = new string[myfiles.Count];
                foreach (IFormFile f in myfiles)
                {
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", f.FileName);
                    using (var file = new FileStream(fullPath, FileMode.Create))
                    {
                        f.CopyTo(file);
                    }
                    fileName[i] = f.FileName;
                    i++;
                }
                if (myfiles.Count == 2)
                {
                    roomimage.RoomimageI1 = fileName[0];
                    roomimage.RoomimageI2 = fileName[1];
                }
                else
                    roomimage.RoomimageI1 = fileName[0];

            }

            if (ModelState.IsValid)
            {
                _context.Add(roomimage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.Rooms, "HotelId", "HotelId", roomimage.HotelId);
            return View(roomimage);
        }

        // GET: Owners/Roomimages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomimage = await _context.Roomimages.FindAsync(id);
            if (roomimage == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.Rooms, "HotelId", "HotelId", roomimage.HotelId);
            return View(roomimage);
        }

        // POST: Owners/Roomimages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomimageId,HotelId,RoomId,RoomimageAvt,RoomimageI1,RoomimageI2")] Roomimage roomimage)
        {
            if (id != roomimage.RoomimageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomimage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomimageExists(roomimage.RoomimageId))
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
            ViewData["HotelId"] = new SelectList(_context.Rooms, "HotelId", "HotelId", roomimage.HotelId);
            return View(roomimage);
        }

        // GET: Owners/Roomimages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomimage = await _context.Roomimages
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomimageId == id);
            if (roomimage == null)
            {
                return NotFound();
            }

            return View(roomimage);
        }

        // POST: Owners/Roomimages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomimage = await _context.Roomimages.FindAsync(id);
            _context.Roomimages.Remove(roomimage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomimageExists(int id)
        {
            return _context.Roomimages.Any(e => e.RoomimageId == id);
        }
    }
}
