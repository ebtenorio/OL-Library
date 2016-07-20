
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IProductCategory
   {
      int ProductCategoryID { get; set; }
      Int64 SalesOrgID { get; set; }
      string ProductCategoryText { get; set; }
      int ParentCategoryID { get; set; }
      Boolean InActive { get; set; }
   }
} //End namespace
