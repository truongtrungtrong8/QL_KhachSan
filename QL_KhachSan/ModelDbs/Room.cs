using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Room
    {
        public Room()
        {
            Customerreviews = new HashSet<Customerreview>();
            Orderrooms = new HashSet<Orderroom>();
            Roomimages = new HashSet<Roomimage>();
        }

        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int TorId { get; set; }
        public string RoomName { get; set; }
        public bool? RoomStatus { get; set; }
        public string RoomDescript { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual Typeofroom Tor { get; set; }
        public virtual ICollection<Customerreview> Customerreviews { get; set; }
        public virtual ICollection<Orderroom> Orderrooms { get; set; }
        public virtual ICollection<Roomimage> Roomimages { get; set; }
    }
}
