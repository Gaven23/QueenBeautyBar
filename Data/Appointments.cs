using System;
using System.Collections.Generic;

namespace QueenBeautyBar.Data
{
    public partial class Appointments
    {
        public Appointments()
        {
            Users = new HashSet<Users>();
        }

        public int AppointmnetId { get; set; }
        public DateTime Date { get; set; }
        public string ServiceOption { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
