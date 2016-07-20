
//CASE Generated Code 5/7/2014 3:15:10 PM

using System;

namespace OrderLinc.IDataContracts
{
    public interface IProviderWarehouse
    {
        int ProviderWarehouseID { get; set; }
        Int64 ProviderID { get; set; }
        string ProviderWarehouseCode { get; set; }
        string ProviderWarehouseName { get; set; }
        Int64 AddressID { get; set; }
        float Longitude { get; set; }
        float Latitude { get; set; }
        Boolean Deleted { get; set; }
        Boolean InActive { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        long CreatedByUserID { get; set; }
        long UpdatedByUserID { get; set; }
        long ContactID { get; set; }
    }
} //End namespace
