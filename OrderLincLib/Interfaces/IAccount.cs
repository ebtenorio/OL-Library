using System;

namespace OrderLinc.IDataContracts
{
    public interface IAccount
    {
        Int64 AccountID { get; set; }
        Int64 RefID { get; set; }
        int AccountTypeID { get; set; }
        int OrgUnitID { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string DeviceNo { get; set; }
        int RoleID { get; set; }
        Int64 AddressID { get; set; }
        Int64 ContactID { get; set; }
        Boolean Deleted { get; set; }
        Boolean InActive { get; set; }
        Boolean Lockout { get; set; }
        string LastIpAddress { get; set; }
        DateTime? DateLockout { get; set; }
        DateTime? LastLoginDate { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        long CreatedByUserID { get; set; }
        long UpdatedByUserID { get; set; }
        DateTime? ExpiryDate { get; set; }
        DateTime? DateActivated { get; set; }
        Int64 ServerID { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string OrderLincVersion { get; set; }
        string iOSVersion { get; set; }
    }
} //End namespace
