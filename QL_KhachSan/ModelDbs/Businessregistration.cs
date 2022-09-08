using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Businessregistration
    {
        public int BrId { get; set; }
        public int HotelId { get; set; }
        public int PricelistId { get; set; }
        public int OwnerId { get; set; }
        public bool? BrStatus { get; set; }
        public DateTime? BrDate { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Pricelist Pricelist { get; set; }
    }
}
