
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOAccount : IAccount
    {
        public DTOAccount()
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
        public Boolean Deleted { get; set; }
        public Boolean InActive { get; set; }
        public Boolean Lockout { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime? DateLockout { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public long CreatedByUserID { get; set; }
        public long UpdatedByUserID { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? DateActivated { get; set; }
        public Int64 ServerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderLincVersion { get; set; }
        public string iOSVersion { get; set; }


        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string AccountTypeText { get; set; }

        public Int64 OldID { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0}  {1}", FirstName, LastName);
            }
        }
    }
    public class DTOAccountList : List<DTOAccount>, IDisposable
    {
        public int TotalRecords { get; set; }

        public void Dispose()
        {
        }
    }

} //End namespace DTO's
