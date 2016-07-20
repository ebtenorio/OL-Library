
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IProviderCustomer
   {
      Int64 ProviderCustomerID { get; set; }
      Int64 CustomerID { get; set; }
      Int64 ProviderID { get; set; }
      string ProviderCustomerCode { get; set; }
      DateTime StartDate { get; set; }
      DateTime EndDate { get; set; }
   }
} //End namespace
