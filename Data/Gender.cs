using System;
using System.Collections.Generic;

namespace QueenBeautyBar.Data
{
    public partial class Gender
    {
        public Gender()
        {
            Users = new HashSet<Users>();
        }

        public int GenderId { get; set; }
        public string GenderDecription { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
