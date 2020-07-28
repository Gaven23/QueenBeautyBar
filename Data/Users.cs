using System;
using System.Collections.Generic;

namespace QueenBeautyBar.Data
{
    public partial class Users
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int AppointmnetId { get; set; }
        public Guid UsersToken { get; set; }
        public string Loginname { get; set; }
        public string Password { get; set; }
        public int GenderId { get; set; }
        public int RaceId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PhysicalAddress { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Appointments Appointmnet { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Race Race { get; set; }
        public virtual Role Role { get; set; }
    }
}
