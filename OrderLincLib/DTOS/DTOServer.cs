
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOServer : IServer
{
   public Int64 ServerID { get; set; }
   public string ServerName { get; set; }
   public string ServerURL { get; set; }

}
   public class DTOServerList : List<DTOServer>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
