
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOSYSOrderStatus : ISYSOrderStatus
{
   public int SYSOrderStatusID { get; set; }
   public string SYSOrderStatusCode { get; set; }
   public string SYSOrderStatusText { get; set; }

}
   public class DTOSYSOrderStatusList : List<DTOSYSOrderStatus>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
