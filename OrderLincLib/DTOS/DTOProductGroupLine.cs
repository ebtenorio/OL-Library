
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOProductGroupLine : IProductGroupLine
{
   public int ProductGroupLineID { get; set; }
   public int ProductGroupID { get; set; }
   public int SortPosition { get; set; }
   public Int64 ProductID { get; set; }
   public float DefaultQty { get; set; }

}
   public class DTOProductGroupLineList : List<DTOProductGroupLine>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
