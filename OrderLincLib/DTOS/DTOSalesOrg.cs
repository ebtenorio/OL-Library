
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOSalesOrg : ISalesOrg
    {
        public Int64 SalesOrgID { get; set; }
        public string SalesOrgCode { get; set; }
        public string BusinessNumber { get; set; }
        public string SalesOrgName { get; set; }
        public Int64 AddressID { get; set; }
        public float Longitude { get; set; }
        public Int64 ContactID { get; set; }
        public float Latitude { get; set; }
        public Boolean Deleted { get; set; }
        public Boolean UseGTINExport { get; set; }
        public Boolean InActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public long CreatedByUserID { get; set; }
        public long UpdatedByUserID { get; set; }
        public Int64 LogoID { get; set; }
        public Boolean IsOrderHeld { get; set; }
        public string SalesOrgShortName { get; set; }

    }
    public class DTOSalesOrgList : List<DTOSalesOrg>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
