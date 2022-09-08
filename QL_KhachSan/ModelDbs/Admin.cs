using System;
using System.Collections.Generic;

#nullable disable

namespace QL_KhachSan.ModelDbs
{
    public partial class Admin
    {
        public string AdminId { get; set; }
        public string AccountUsername { get; set; }
        public string AdminName { get; set; }
        public string AdminPhone { get; set; }
        public string AdminAddress { get; set; }
        public bool? AdminSex { get; set; }
        public string AdminBankaccount { get; set; }

        public virtual Account AccountUsernameNavigation { get; set; }
    }
}
