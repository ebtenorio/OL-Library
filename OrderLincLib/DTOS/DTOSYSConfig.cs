
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOSYSConfig : ISYSConfig
{
   public Int64 SYSConfigID { get; set; }
   public string ConfigKey { get; set; }
   public string ConfigValue { get; set; }
   public string ConfigDescription { get; set; }

}
   public class DTOSYSConfigList : List<DTOSYSConfig>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
