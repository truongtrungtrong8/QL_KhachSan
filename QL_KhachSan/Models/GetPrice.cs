using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QL_KhachSan.Models
{
    public class GetPrice
    {
        public int hotelid { get; set; }
        public int price { get; set; }
        public string name { get; set; }
        public double pointx { get; set; }
        public double pointy { get; set; }
    }
}
