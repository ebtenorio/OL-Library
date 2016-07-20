
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
    public interface IProduct
    {
        Int64 ProductID { get; set; }
        long SalesOrgID { get; set; }
        string GTINCode { get; set; }
        Int64 ProductBrandID { get; set; }
        int ProductCategoryID { get; set; }
        Int64 PrimarySKU { get; set; }
        string ProductDescription { get; set; }
        Int64 UOMID { get; set; }
        Boolean Inactive { get; set; }

        long ProviderID { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string ProductCode { get; set; }
    }
} //End namespace
