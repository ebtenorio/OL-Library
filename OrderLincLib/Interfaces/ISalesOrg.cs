
//CASE Generated Code 5/7/2014 3:15:10 PM

using System;

namespace OrderLinc.IDataContracts
{
    public interface ISalesOrg
    {
        string SalesOrgShortName { get; set; }
        Int64 SalesOrgID { get; set; }
        string SalesOrgCode { get; set; }
        string BusinessNumber { get; set; }
        string SalesOrgName { get; set; }
        Int64 AddressID { get; set; }
        float Longitude { get; set; }
        Int64 ContactID { get; set; }
        float Latitude { get; set; }
        Boolean Deleted { get; set; }
        Boolean UseGTINExport { get; set; }
        Boolean InActive { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        long CreatedByUserID { get; set; }
        long UpdatedByUserID { get; set; }
        Int64 LogoID { get; set; }
        Boolean IsOrderHeld { get; set; }
    }
} //End namespace
