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
    [Route("Owners/Rooms")]
    public class RoomsController : Controller
    {
        private readonly QL_KhachSanContext _context;

        public RoomsController(QL_KhachSanContext context)
        {
            _context = context;
        }

        // GET: Owners/Rooms
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var qL_KhachSanContext = _context.Rooms.Include(r => r.Hotel).Include(r => r.Tor).Where(r=>r.Hotel.OwnerId.ToString() == HttpContext.Session.GetString("Own_ID"));
            return View(await qL_KhachSanContext.ToListAsync());
        }

        // GET: Owners/Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.Tor)
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Owners/Rooms/Create
        [Route("create")]
        public IActionResult Create()
        {
            ViewData["HotelId"] = new SelectList(_context.Hotels.Where(r=>r.OwnerId.ToString() == HttpContext.Session.GetString("Own_ID")), "HotelId", "HotelName");
            ViewData["TorId"] = new SelectList(_context.Typeofrooms.Where(r => r.OwnerId.ToString() == HttpContext.Session.GetString("Own_ID")), "TorId", "TorName");
            return View();
        }

        // POST: Owners/Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelId,RoomId,TorId,RoomName,RoomStatus,RoomDescript")] Room room, IFormFile myfile, List<IFormFile> myfiles)
        {

            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                if (myfile != null || myfiles != null)
                {
                    string imageAvt = "", image1 = "", image2 = "";
                    if (myfile != null)
                    {
                        string fullPath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", myfile.FileName);
                        using (var file = new FileStream(fullPath1, FileMode.Create))
                        {
                            myfile.CopyTo(file);
                        }
                        imageAvt = myfile.FileName;
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
                            image1 = fileName[0];
                            image2 = fileName[1];
                        }
                        else
                            image1 = fileName[0];

                    }
                    var image = new Roomimage()
                    {
                        HotelId = room.HotelId,
                        RoomId = room.RoomId,
                        RoomimageAvt = imageAvt,
                        RoomimageI1 = image1,
                        RoomimageI2 = image2,
                    };
                    _context.Add(image);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }

           
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "DistrictId", room.HotelId);
            ViewData["TorId"] = new SelectList(_context.Typeofrooms, "TorId", "TorId", room.TorId);
            return View(room);
        }

        // GET: Owners/Rooms/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id1, int? id2)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id1,id2);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelName", room.HotelId);
            ViewData["TorId"] = new SelectList(_context.Typeofrooms, "TorId", "TorName", room.TorId);
            return View(room);
        }

        // POST: Owners/Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("HotelId,RoomId,TorId,RoomName,RoomStatus,RoomDescript")] Room room)
        {
            if (id != room.HotelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.HotelId))
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
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "DistrictId", room.HotelId);
            ViewData["TorId"] = new SelectList(_context.Typeofrooms, "TorId", "TorId", room.TorId);
            return View(room);
        }

        // GET: Owners/Rooms/Delete/5
        [Route("Delete")]
        public async Task<IActionResult> Delete(int? id1, int? id2)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id1, id2);
            var roomimg = _context.Roomimages.Where(r=>r.RoomId == room.RoomId).SingleOrDefault();
            _context.Roomimages.Remove(roomimg);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.HotelId == id);
        }
    }
}
