
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IProductData
   {
      Int64 ProductDataID { get; set; }
      Int64 ProductID { get; set; }
      string Width { get; set; }
      string Height { get; set; }
      string Length { get; set; }
      Byte[] FileBin { get; set; }
      string FileName { get; set; }
      string OriginPath { get; set; }
   }
} //End namespace
