using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Ward
    {
        public Ward()
        {
            Hotels = new HashSet<Hotel>();
        }

        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public string WardName { get; set; }
        public int? WardLevel { get; set; }

        public virtual District District { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
