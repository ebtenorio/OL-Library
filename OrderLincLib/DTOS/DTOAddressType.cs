
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOAddressType : IAddressType
{
   public int AddressTypeID { get; set; }
   public string AddressTypeText { get; set; }

}
   public class DTOAddressTypeList : List<DTOAddressType>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
