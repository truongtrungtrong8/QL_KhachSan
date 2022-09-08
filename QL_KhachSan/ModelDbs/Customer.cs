using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Customer
    {
        public Customer()
        {
            Customerreviews = new HashSet<Customerreview>();
            Orderrooms = new HashSet<Orderroom>();
        }

        public int CustomerId { get; set; }
        public string AccountUsername { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public bool? CustomerSex { get; set; }
        public DateTime? CustomerDateofbirth { get; set; }
        public string CustomerBankaccount { get; set; }

        public virtual Account AccountUsernameNavigation { get; set; }
        public virtual ICollection<Customerreview> Customerreviews { get; set; }
        public virtual ICollection<Orderroom> Orderrooms { get; set; }
    }
}
