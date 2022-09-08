using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Hotel
    {
        public Hotel()
        {
            Businessregistrations = new HashSet<Businessregistration>();
            Rooms = new HashSet<Room>();
        }

        public int HotelId { get; set; }
        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public int OwnerId { get; set; }
        public int? PointId { get; set; }
        public string HotelName { get; set; }
        public string HotelAvt { get; set; }
        public string HotelAdd { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual Point Point { get; set; }
        public virtual Ward Ward { get; set; }
        public virtual ICollection<Businessregistration> Businessregistrations { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
