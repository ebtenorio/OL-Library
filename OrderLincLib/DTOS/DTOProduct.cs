using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOProduct : IProduct
    {
        public DTOProduct()
        {
            StartDate = DateTime.Today;
            EndDate = new DateTime(9999, 09, 09);
        }

        public Int64 ProductID { get; set; }
        public long SalesOrgID { get; set; }
        public string GTINCode { get; set; }
        public Int64 ProductBrandID { get; set; }
        public int ProductCategoryID { get; set; }
        public Int64 PrimarySKU { get; set; }
        public string ProductDescription { get; set; }
        public Int64 UOMID { get; set; }
        public Boolean Inactive { get; set; }

        public long ProviderID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProductCode { get; set; }
        public float Discount { get; set; }


     
    }
    public class DTOProductList : List<DTOProduct>, IDisposable
    {
        public int TotalRecords { get; set; }

        public void Dispose()
        {
        }
    }

} 