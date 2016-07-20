
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOProviderCustomer : IProviderCustomer
    {
        public Int64 ProviderCustomerID { get; set; }
        public Int64 CustomerID { get; set; }
        public Int64 ProviderID { get; set; }
        public string ProviderCustomerCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String CustomerName { get; set; }
        public String ProviderName { get; set; }

    }
    public class DTOProviderCustomerList : List<DTOProviderCustomer>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
