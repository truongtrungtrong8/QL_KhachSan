using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Point
    {
        public Point()
        {
            Hotels = new HashSet<Hotel>();
        }

        public int PointId { get; set; }
        public double? PointX { get; set; }
        public double? PointY { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
