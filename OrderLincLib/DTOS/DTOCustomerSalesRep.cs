
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOCustomerSalesRep : ICustomerSalesRep
    {
        public Int64 CustomerSalesRepID { get; set; }
        public Int64 CustomerID { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string StateName { get; set; }
        public Int64 SalesRepAccountID { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    public class DTOCustomerSalesRepList : List<DTOCustomerSalesRep>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
