using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Account
    {
        public Account()
        {
            Admins = new HashSet<Admin>();
            Customers = new HashSet<Customer>();
            Owners = new HashSet<Owner>();
        }

        public string AccountUsername { get; set; }
        public int ToaId { get; set; }
        public string AccountPassword { get; set; }

        public virtual Typeofaccount Toa { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Owner> Owners { get; set; }
    }
}
