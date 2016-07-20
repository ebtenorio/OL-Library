
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IProductGroupLine
   {
      int ProductGroupLineID { get; set; }
      int ProductGroupID { get; set; }
      int SortPosition { get; set; }
      Int64 ProductID { get; set; }
      float DefaultQty { get; set; }
   }
} //End namespace
