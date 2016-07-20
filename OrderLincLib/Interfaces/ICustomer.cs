
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
    public interface ICustomer
    {
        Int64 CustomerID { get; set; }
        Int64 SalesOrgID { get; set; }
        string BusinessNumber { get; set; }
        string CustomerName { get; set; }
        Int64 SalesRepAccountID { get; set; }
        Int64 AddressID { get; set; }
        Int64 SYSStateID { get; set; }
        int RegionID { get; set; }
        long ContactID { get; set; }
        Int64 BillToAddressID { get; set; }
        Int64 ShipToAddressID { get; set; }
        float Longitude { get; set; }
        float Latitude { get; set; }
        Boolean Deleted { get; set; }
        Boolean InActive { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        long CreatedByUserID { get; set; }
        long UpdatedByUserID { get; set; }

        string ProviderCustomerCode { get; set; }

        string StateName { get; set; }
        string StateCode { get; set; }
    }
} //End namespace
