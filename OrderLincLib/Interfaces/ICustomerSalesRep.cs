
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
    public interface ICustomerSalesRep
    {
        Int64 CustomerSalesRepID { get; set; }
        Int64 CustomerID { get; set; }
        Int64 SalesRepAccountID { get; set; }
        DateTime DateCreated { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
} //End namespace
