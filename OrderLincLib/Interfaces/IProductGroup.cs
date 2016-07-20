
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IProductGroup
   {
      int ProductGroupID { get; set; }
      Int64 SalesOrgID { get; set; }
      int SortPosition { get; set; }
      string ProductGroupText { get; set; }
      Boolean InActive { get; set; }
   }
} //End namespace
