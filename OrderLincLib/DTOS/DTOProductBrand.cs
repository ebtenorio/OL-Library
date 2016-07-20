
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOProductBrand : IProductBrand
{
   public Int64 ProductBrandID { get; set; }
   public int SalesOrgID { get; set; }
   public string ProductBrandText { get; set; }

}
   public class DTOProductBrandList : List<DTOProductBrand>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
