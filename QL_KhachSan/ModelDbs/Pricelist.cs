using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Pricelist
    {
        public Pricelist()
        {
            Businessregistrations = new HashSet<Businessregistration>();
        }

        public int PricelistId { get; set; }
        public string PricelistName { get; set; }
        public int? PricelistPrice { get; set; }

        public virtual ICollection<Businessregistration> Businessregistrations { get; set; }
    }
}
