
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOProductCategory : IProductCategory
{
   public int ProductCategoryID { get; set; }
   public Int64 SalesOrgID { get; set; }
   public string ProductCategoryText { get; set; }
   public int ParentCategoryID { get; set; }
   public Boolean InActive { get; set; }

}
   public class DTOProductCategoryList : List<DTOProductCategory>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
