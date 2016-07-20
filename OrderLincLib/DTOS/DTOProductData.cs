
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOProductData : IProductData
{
   public Int64 ProductDataID { get; set; }
   public Int64 ProductID { get; set; }
   public string Width { get; set; }
   public string Height { get; set; }
   public string Length { get; set; }
   public Byte[] FileBin { get; set; }
   public string FileName { get; set; }
   public string OriginPath { get; set; }

}
   public class DTOProductDataList : List<DTOProductData>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
