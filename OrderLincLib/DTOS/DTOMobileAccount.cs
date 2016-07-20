using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderLinc.DTOs
{
    public class DTOMobileAccount 
    {
        public DTOMobileAccount()
        {
            DateUpdated = DateTime.Now;
            DateCreated = DateTime.Now;
            StartDate = DateTime.Today;
            EndDate = new DateTime(9999, 09, 09);
        }

        public Int64 AccountID { get; set; }
        public Int64 RefID { get; set; }
        public int AccountTypeID { get; set; }
        public int OrgUnitID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceNo { get; set; }
        public int RoleID { get; set; }
        public Int64 AddressID { get; set; }
        public Int64 ContactID { get; set; }
        public Boolean Lockout { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime? DateLockout { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? DateActivated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string AccountTypeText { get; set; }

        public string ServerUrl { get; set; }

        public string Logo { get; set; }
    }
}
