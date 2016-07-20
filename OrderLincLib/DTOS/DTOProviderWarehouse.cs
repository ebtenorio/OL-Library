using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOProviderWarehouse : IProviderWarehouse
    {
        public DTOProviderWarehouse()
        {
            DateUpdated = DateTime.Now;
            DateCreated = DateTime.Now;
            StartDate = DateTime.Today;
            EndDate = new DateTime(9999, 09, 09);
        }

        public int ProviderWarehouseID { get; set; }
        public Int64 ProviderID { get; set; }
        public string ProviderWarehouseCode { get; set; }
        public string ProviderWarehouseName { get; set; }
        public Int64 AddressID { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public Boolean Deleted { get; set; }
        public Boolean InActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public long CreatedByUserID { get; set; }
        public long UpdatedByUserID { get; set; }
        public long ContactID { get; set; }

    }
    public class DTOProviderWarehouseList : List<DTOProviderWarehouse>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
