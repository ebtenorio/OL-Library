
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOProductUOM : IProductUOM
{
   public int ProductUOMID { get; set; }
   public string ProductUOM { get; set; }

}
   public class DTOProductUOMList : List<DTOProductUOM>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
