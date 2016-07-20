
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTORegion : IRegion
{
   public int RegionID { get; set; }
   public Int64 SalesOrgID { get; set; }
   public byte[] RegionName { get; set; }

}
   public class DTORegionList : List<DTORegion>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
