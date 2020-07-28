using System;
using System.Collections.Generic;

namespace QueenBeautyBar.Data
{
    public partial class Race
    {
        public Race()
        {
            Users = new HashSet<Users>();
        }

        public int RaceId { get; set; }
        public string RaceDescription { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
