using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QL_KhachSan.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QL_KhachSan.ModelDbs;
using Microsoft.AspNetCore.Http;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using EASendMail;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace QL_KhachSan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;
        private IHttpContextAccessor _accessor;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _logger = logger;
            Configuration = configuration;
            _accessor = accessor;
        }
        public QL_KhachSanContext _context = new QL_KhachSanContext();

        public IActionResult Index()
        {
            var res = _context.Businessregistrations.Where(r=>r.BrStatus == true).Select(r=>r.HotelId).ToList();
            var hotel = _context.Hotels.Where(h => res.Contains(h.HotelId)).ToList();
            return View(hotel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account acc)
        {

            if (ModelState.IsValid)
            {
                var data = _context.Accounts.Where(a=>a.AccountUsername == acc.AccountUsername & a.AccountPassword == acc.AccountPassword).SingleOrDefault();
                if (data != null && data.ToaId == 1)
                {
                    var user = _context.Admins.Where(u=>u.AccountUsername == data.AccountUsername).SingleOrDefault();
                    HttpContext.Session.SetString("Adm_Name", user.AdminName);
                    HttpContext.Session.SetString("Adm_ID", user.AdminId);
                    return Redirect("/Admin/Home/Index");
                }
                else if (data != null && data.ToaId == 2)
                {
                    HttpContext.Session.SetString("Own_Phone", acc.AccountUsername);
                    var user = _context.Owners.Where(u => u.AccountUsername == data.AccountUsername).SingleOrDefault();

                    if (user != null)
                    {
                        HttpContext.Session.SetString("Own_Name", user.OwnerName);
                        HttpContext.Session.SetString("Own_ID", user.OwnerId.ToString());

                    }
                    else
                    {
                        return Redirect("/Owners/Owners/Create");
                    }
                    return Redirect("/Owners/Owners/Index");
                }
                else if (data != null && data.ToaId == 3)
                {
                    var user = _context.Customers.Where(u => u.AccountUsername == data.AccountUsername).SingleOrDefault();
                    HttpContext.Session.SetString("Cus_Phone", acc.AccountUsername);
                    
                        if(user != null)
                        {
                            HttpContext.Session.SetString("Cus_Name", user.CustomerName);
                            HttpContext.Session.SetString("Cus_ID", user.CustomerId.ToString());
                            try
                            {
                                if (HttpContext.Session.GetString("Hotel_ID") != null && HttpContext.Session.GetString("Hotel") == "1")
                                {
                                    string temp = HttpContext.Session.GetString("Hotel_ID");
                                    HttpContext.Session.Remove("Hotel_ID");
                                    return Redirect("/HotelDetail/Index/" + temp);
                                }
                            }
                            catch (Exception ex) { }
                        }
                        
                        
                    
                        else 
                         {
                            return Redirect("/Customers/Customers/Create");
                         }
                    return Redirect("/Home/Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(Account acc, int type)
        {
            try
            {
                acc.ToaId = type;
                _context.Accounts.Add(acc);
                _context.SaveChanges();
                return Redirect("/Home/Login");
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
            return View();
        }
        public int CheckUserName(string username)
        {
            var acc = _context.Accounts.Find(username);
            if (acc != null)
                return 1;
            return 0;
        }

        public string GetNamePro(string id)
        {
            string idnew = "";
            int temp = Int32.Parse(id);
            if (temp < 10)
            {
                idnew = "0" + id;
            }
            else idnew = id;
            var data = _context.Provinces.Where(p => p.ProvinceId == idnew).SingleOrDefault();
            return data.ProvinceName;
        }

        public string GetNameDis(string proid, string disid)
        {
            string idnew = "";
            int temp = Int32.Parse(disid);
            if (Int32.Parse(proid) < 10)
            {
                proid = "0" + proid;
            }
            if (temp < 10)
            {
                idnew = "00" + disid;
            }
            else if (temp > 10 & temp < 100)
            {
                idnew = "0" + disid;
            }
            else idnew = disid;
            var data = _context.Districts.Where(d => d.ProvinceId == proid & d.DistrictId == idnew).SingleOrDefault();
            return data.DistrictName;
        }
        public async Task<string> GetAddressHotel(string proid, string disid, string warid)
        {
            string proidnew = "";
            string disidnew = "";
            string waridnew = "";
            int temp = 0;
            temp = Int32.Parse(disid);
            if (Int32.Parse(proid) < 10)
            {
                proidnew = "0" + proid;
            }
            else proidnew = proid;
            if (temp < 10)
            {
                disidnew = "00" + disid;
            }
            else if (temp > 10 & temp < 100)
            {
                disidnew = "0" + disid;
            }
            else disidnew = disid;
            temp = Int32.Parse(warid);
            if (temp < 10)
            {
                waridnew = "0000" + warid;
            }
            else if (temp > 10 & temp < 100)
            {
                waridnew = "000" + warid;
            }
            else if (temp > 100 & temp < 1000)
            {
                waridnew = "00" + warid;
            }
            else if (temp > 1000 & temp < 10000)
            {
                waridnew = "0" + warid;
            }
            else waridnew = warid;
            var pro = await _context.Provinces.Where(d => d.ProvinceId == proidnew).SingleOrDefaultAsync();
            var dis = await _context.Districts.Where(d => d.ProvinceId == proidnew & d.DistrictId == disidnew).SingleOrDefaultAsync();
            var war = await _context.Wards.Where(d => d.ProvinceId == proidnew & d.DistrictId == disidnew & d.WardId == waridnew).SingleOrDefaultAsync();

            return war.WardName + ", " + dis.DistrictName + ", " + pro.ProvinceName;
        }

        public int GetMinPriceHotel(int id)
        {
            int value = 0;
            var data = (from r in _context.Rooms
                        join h in _context.Hotels on r.HotelId equals id
                        join t in _context.Typeofrooms on r.TorId equals t.TorId
                        select new
                        {
                            price = t.TorPrice
                        });
            value = int.Parse(data.Min(p=>p.price).ToString());
            return value;
        }
        public JsonResult GetImagesHotel(int id)
        {
            var hotel = _context.Hotels.Where(h=>h.HotelId == id).Select(h=>h.HotelId).ToList();
            var room = _context.Rooms.Where(r => hotel.Contains(r.HotelId)).Select(r=>r.RoomId).ToList();
            var image = _context.Roomimages.Where(h => room.Contains(h.RoomId)).ToList();
            return Json(image.ToList());
        }

        public JsonResult GetPointMap()
        {
            var hotels = _context.Hotels.Select(h => new {
                id = h.HotelId,
                price = 1,
                name = h.HotelName,
                pointx = h.Point.PointX,
                pointy = h.Point.PointY
            }).ToList();

            List<GetPrice> tempList = new List<GetPrice>();

            foreach (var item in hotels)
            {
                var temp = new GetPrice();
                temp.hotelid = item.id;
                try
                {
                    temp.price = GetMinPriceHotel(temp.hotelid);
                }
                catch (Exception ex)
                {
                    temp.price = 0;
                }
                temp.name = item.name;
                temp.pointx = (double)item.pointx;
                temp.pointy = (double)item.pointy;

                tempList.Add(temp);
            }
            return Json(tempList);
        }
        public IActionResult HotelsLoadPoint()
        {
            return View();
        }

        public JsonResult GetCustomer(int id)
        {
            var user = _context.Customers.Where(c => c.CustomerId == id).SingleOrDefault();
            return Json(user);
        }

        public async Task<IActionResult> AddOrderRoom(int roomId, DateTime dateStart, DateTime dateEnd)
        {
            var orderroom = new Orderroom();
            orderroom.HotelId = int.Parse(HttpContext.Session.GetString("Hotel_ID")); ;
            orderroom.RoomId = roomId;
            orderroom.CustomerId = int.Parse(HttpContext.Session.GetString("Cus_ID"));
            orderroom.OrderDaystart = dateStart;
            orderroom.OrderDayend = dateEnd;

            var user = _context.Customers.Where(u => u.CustomerId == orderroom.CustomerId).FirstOrDefault();
            var hotel = _context.Hotels.Where(h => h.HotelId == orderroom.HotelId).FirstOrDefault();
            var owner = _context.Owners.Where(o=>o.OwnerId == hotel.OwnerId).FirstOrDefault();


            orderroom.OrderDate = DateTime.Today;
            TimeSpan day = ((TimeSpan)(orderroom.OrderDayend - orderroom.OrderDaystart));
            var room = _context.Rooms.Where(h => h.HotelId == orderroom.HotelId & h.RoomId == orderroom.RoomId).SingleOrDefault();
            
            var tor = _context.Typeofrooms.Where(t => t.TorId == room.TorId).Single();
            int price = ((int)(day.TotalDays * tor.TorPrice));
            int pr = price * 100;

            var point = _context.Points.Where(p => p.PointId == hotel.PointId).SingleOrDefault();
            HttpContext.Session.SetString("X", point.PointX.ToString());
            HttpContext.Session.SetString("Y", point.PointY.ToString());
            HttpContext.Session.SetString("PricePay", pr.ToString());
            orderroom.OrderPrice = price;
            try
            {
                _context.Add(orderroom);
                room.RoomStatus = true;
                _context.SaveChanges();


                SmtpMail oMail = new SmtpMail("TryIt");

                oMail.From = "truongtrungtrong@outlook.com.vn";

                // Set recipient email address
                oMail.To = user.CustomerEmail;


                // Set email subject
                oMail.Subject = "Đặt phòng thành công";

                var address = await GetAddressHotel(hotel.ProvinceId, hotel.DistrictId, hotel.WardId);
                
                

                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                string temp = price.ToString("#,###", cul.NumberFormat);
                // Set email body
                oMail.TextBody = "Chào khách hàng: " + user.CustomerName + "\n"
                    + "\nĐơn đặt phòng của bạn ngày " + orderroom.OrderDate.ToString().Substring(0,10) + " đã được xác nhận"
                    + "\nKhách sạn: " + hotel.HotelName
                    + "\nĐịa chỉ: " + hotel.HotelAdd+", "  +address
                    + "\nThời gian lưu trú: " + day.TotalDays +" ngày"
                    + "\nSố điện thoại: " + user.CustomerPhone
                    + "\nSố tiền phải trả: " + temp +" VNĐ"
                    + "\n\nCảm ơn bạn đã đặt phòng.";

                // Hotmail/Outlook SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.office365.com");

                // If your account is office 365, please change to Office 365 SMTP server
                // SmtpServer oServer = new SmtpServer("smtp.office365.com");

                // User authentication should use your
                // email address as the user name.
                oServer.User = "truongtrungtrong@outlook.com.vn";

                // If you got authentication error, try to create an app password instead of your user password.
                // https://support.microsoft.com/en-us/account-billing/using-app-passwords-with-apps-that-don-t-support-two-step-verification-5896ed9b-4263-e681-128a-a6f2979a7944
                oServer.Password = "m&VqK8Vt";

                // use 587 TLS port
                oServer.Port = 587;

                // detect SSL/TLS connection automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient oSmtp = new SmtpClient();
                oSmtp.SendMail(oServer, oMail);


                //var message = new Message(new string[] { user.CustomerEmail }, "Đặt phòng thành công",
                //    "Chào khách hàng: " + user.CustomerName + "\n"
                //    + "\nĐơn đặt phòng của bạn ngày"+orderroom.OrderDate.ToString()+" đã được xác nhận"
                //    + "\nKhách sạn: " + hotel.HotelName
                //    + "\nĐịa chỉ: " + GetAddressHotel(hotel.ProvinceId,hotel.DistrictId, hotel.WardId)
                //    + "\nThời gian lưu trú: " + day.TotalDays
                //    + "\nSố điện thoại: " + owner.OwnerPhone
                //    + "\nSố tiền phải trả: " + price.ToString()
                //    + "\n\nCảm ơn bạn đã đặt phòng.");
                //_emailSender.SendEmail(message);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            return Redirect("/Home/Payment");
        }

        public ActionResult Payment()
        {
            string url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string returnUrl = "https://localhost:44378/Home/PaymentConfirm";
            string tmnCode = "M4SI0GQK";
            string hashSecret = "TEPHMKQSDCNVKDFCAMUICRAJVMMXNQFF";

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.0.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", HttpContext.Session.GetString("PricePay")); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", GetIp()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }
        public string GetIp()
        {
            string ipAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return ipAddress;
        }
        public ActionResult PaymentConfirm()
        {



            string hashSecret = Configuration.GetSection("AppConfiguration")["HashSecret"]; //Chuỗi bí mật
            var vnpayData = HttpContext.Request.QueryString.ToString();
            PayLib pay = new PayLib();


            //lấy toàn bộ dữ liệu được trả về
            int i = 1;
            while (true)
            {
                string key = "";
                string valuetemp = "";
                if (vnpayData[i] == 'v' && vnpayData[i + 1] == 'n' && vnpayData[i + 2] == 'p' && vnpayData[i + 3] == '_')
                {
                    key = "";
                    // lấy key
                    for (int j = i; j < vnpayData.Length; j++)
                    {
                        if (vnpayData[j] == '=')
                            // lấy value
                            for (int e = j + 1; e < vnpayData.Length; e++)
                            {
                                if (vnpayData[e] == '&')
                                {
                                    i = e + 1;
                                    j = e;
                                    break;
                                }
                                valuetemp += vnpayData[e];


                            }
                        if (vnpayData[j] == '&')
                        {
                            break;
                        }
                        key += vnpayData[j];


                    }
                }
                if (i != vnpayData.Length - 1)
                {
                    if (key.StartsWith("vnp_SecureHash"))
                    {
                        key = key.Substring(0, 14);
                        pay.AddResponseData(key, valuetemp);
                        PayLib p = new PayLib();
                        p = pay;
                        break;
                    }

                    else if (key.StartsWith("vnp_BankTranNo"))
                    {
                        string temp = valuetemp.Substring(3);
                        pay.AddResponseData(key, temp);
                    }
                    else
                    {
                        pay.AddResponseData(key, valuetemp);
                    }

                }
            }

            string orderId = pay.GetResponseData("vnp_TxnRef"); //mã hóa đơn
            string vnpayTranId = pay.GetResponseData("vnp_TransactionNo"); //mã giao dịch tại hệ thống VNPAY
            string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
            string vnp_SecureHash = Request.Query["vnp_SecureHash"]; //hash của dữ liệu trả về

            bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

            if (!checkSignature)
            {
                if (vnp_ResponseCode == "00")
                {
                    //Thanh toán thành công
                    ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                }
                else
                {
                    //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                }
            }
            else
            {
                ViewBag.Message = "Không thành công mã là: " + vnp_ResponseCode;

            }


            return Redirect("/Customers/Customers/Routing?X=" + HttpContext.Session.GetString("X") + "&Y=" + HttpContext.Session.GetString("Y"));
        }

    }
}
