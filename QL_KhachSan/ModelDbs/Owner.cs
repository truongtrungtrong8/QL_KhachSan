using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Owner
    {
        public Owner()
        {
            Businessregistrations = new HashSet<Businessregistration>();
            Hotels = new HashSet<Hotel>();
            Typeofrooms = new HashSet<Typeofroom>();
        }

        public int OwnerId { get; set; }
        public string AccountUsername { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerAddress { get; set; }
        public string OwnerEmail { get; set; }
        public bool? OwnerSex { get; set; }
        public DateTime? OwnerDateofbirth { get; set; }
        public string OwnerBankaccount { get; set; }

        public virtual Account AccountUsernameNavigation { get; set; }
        public virtual ICollection<Businessregistration> Businessregistrations { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Typeofroom> Typeofrooms { get; set; }
    }
}
