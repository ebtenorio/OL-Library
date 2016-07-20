
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOOrderSignature : IOrderSignature
{
   public Int64 OrderID { get; set; }
   public string Path { get; set; }
   public DateTime? DateCreated { get; set; }

}
   public class DTOOrderSignatureList : List<DTOOrderSignature>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
