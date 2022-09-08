using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Roomimage
    {
        public int RoomimageId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public string RoomimageAvt { get; set; }
        public string RoomimageI1 { get; set; }
        public string RoomimageI2 { get; set; }

        public virtual Room Room { get; set; }
    }
}
