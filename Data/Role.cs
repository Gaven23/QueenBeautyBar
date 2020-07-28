using System;
using System.Collections.Generic;

namespace QueenBeautyBar.Data
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<Users>();
        }

        public int RoleId { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
