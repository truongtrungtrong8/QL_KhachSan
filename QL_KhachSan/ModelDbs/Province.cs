using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
        }

        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
