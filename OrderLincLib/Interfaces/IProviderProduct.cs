
//CASE Generated Code 5/7/2014 3:15:10 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IProviderProduct
   {
      Int64 ProviderProductID { get; set; }
      Int64 ProviderID { get; set; }
      Int64 ProductID { get; set; }
      string ProviderProductCode { get; set; }
      DateTime StartDate { get; set; }
      DateTime EndDate { get; set; }
      string ProviderName { get; set; }
      float Discount { get; set; }
   }
} //End namespace
