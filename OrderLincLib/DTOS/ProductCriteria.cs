
using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{
    public enum ProductSearchType
    {
        ByProductGroupID = 1,
        ByProductCode = 2,
        ByGTINCode = 3,
        ByProductID = 4,

        ByProductName,
    }

    public class ProductCriteria
    {
        public ProductSearchType SearchType { get; set; }

        public int ProductGroupID { get; set; }

        public long ProductID { get; set; }

        public long ProviderID { get; set; }

        public string GTINCode { get; set; }

        public string ProductCode { get; set; }

        public long SalesOrgID { get; set; }

        public int CurrentPage { get; set; }

        public int PageItemCount { get; set; }

        public string ProductName { get; set; }
    }

}