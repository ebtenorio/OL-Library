
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IAddress
   {
      Int64 AddressID { get; set; }
      int AddressTypeID { get; set; }
      string AddressLine1 { get; set; }
      string AddressLine2 { get; set; }
      string CitySuburb { get; set; }
      int SYSStateID { get; set; }
      string PostalZipCode { get; set; }
   }
} //End namespace
