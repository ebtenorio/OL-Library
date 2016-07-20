
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOProvider : IProvider
    {
        public DTOProvider()
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            Deleted = false;
            InActive = false;
        }

        public Int64 ProviderID { get; set; }
        public string ProviderCode { get; set; }
        public string BusinessNumber { get; set; }
        public string ProviderName { get; set; }
        public Int64 SalesOrgID { get; set; }
        public Int64 AddressID { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public Boolean Deleted { get; set; }
        public Boolean InActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public long CreatedByUserID { get; set; }
        public long UpdatedByUserID { get; set; }

        public Boolean? IsPepsiDistributor { get; set; }

    }
    public class DTOProviderList : List<DTOProvider>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
