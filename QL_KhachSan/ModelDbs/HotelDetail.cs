using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QL_KhachSan.ModelDbs
{
    public class HotelDetail
    {
        public Hotel hotel { get; set; }
        public virtual ICollection<Room> room { get; set; }
        public virtual ICollection<Roomimage> roomimage { get; set; }
        public virtual ICollection<Typeofroom> typeofroom { get; set; }
    }
}
