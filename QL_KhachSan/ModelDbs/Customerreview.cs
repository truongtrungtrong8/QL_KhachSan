using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Customerreview
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public int? CrStar { get; set; }
        public string CrComment { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
    }
}
