
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOCustomer : ICustomer
    {
        public DTOCustomer()
        {
            DateUpdated = DateTime.Now;
            DateCreated = DateTime.Now;
            StartDate = DateTime.Today;
            EndDate = new DateTime(9999, 09, 09);
        }

        public Int64 CustomerID { get; set; }
        public Int64 SalesOrgID { get; set; }
        public string BusinessNumber { get; set; }
        public string CustomerName { get; set; }
        public Int64 SalesRepAccountID { get; set; }
        public Int64 AddressID { get; set; }
        public Int64 SYSStateID { get; set; }

     
        public string Region { get; set; }

        public int RegionID { get; set; }
        public long ContactID { get; set; }
        public Int64 BillToAddressID { get; set; }
        public Int64 ShipToAddressID { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public Boolean Deleted { get; set; }
        public Boolean InActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public long CreatedByUserID { get; set; }
        public long UpdatedByUserID { get; set; }
        public long CustomerSalesRepID { get; set; }
        public long ProviderID { get; set; }

        public string CustomerCode { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string ProviderCustomerCode { get; set; }
        public Int64 OldID { get; set; }

    }
    public class DTOCustomerList : List<DTOCustomer>, IDisposable
    {
        public int TotalRecords { get; set; }
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
