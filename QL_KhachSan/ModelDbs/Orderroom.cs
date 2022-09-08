using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Orderroom
    {
        public int OrderId { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool? OrderStatus { get; set; }
        public DateTime? OrderDaystart { get; set; }
        public DateTime? OrderDayend { get; set; }
        public int? OrderPrice { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
