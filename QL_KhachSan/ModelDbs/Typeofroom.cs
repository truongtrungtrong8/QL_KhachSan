using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Typeofroom
    {
        public Typeofroom()
        {
            Rooms = new HashSet<Room>();
        }

        public int TorId { get; set; }
        public int OwnerId { get; set; }
        public string TorName { get; set; }
        public int? TorPrice { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
