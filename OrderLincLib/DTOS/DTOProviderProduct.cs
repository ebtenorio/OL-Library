
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOProviderProduct : IProviderProduct
    {
        public Int64 ProviderProductID { get; set; }
        public Int64 ProviderID { get; set; }
        public Int64 ProductID { get; set; }
        public string ProviderProductCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProviderName { get; set; }
        public float Discount { get; set; }

    }
    public class DTOProviderProductList : List<DTOProviderProduct>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
