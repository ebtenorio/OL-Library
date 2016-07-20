
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOProductGroup : IProductGroup
{
   public int ProductGroupID { get; set; }
   public Int64 SalesOrgID { get; set; }
   public int SortPosition { get; set; }
   public string ProductGroupText { get; set; }
   public Boolean InActive { get; set; }

}
   public class DTOProductGroupList : List<DTOProductGroup>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
