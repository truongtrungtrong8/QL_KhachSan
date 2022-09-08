using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }

        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
