
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IProvider
   {
      Int64 ProviderID { get; set; }
      string ProviderCode { get; set; }
      string BusinessNumber { get; set; }
      string ProviderName { get; set; }
      Int64 SalesOrgID { get; set; }
      Int64 AddressID { get; set; }
      float Longitude { get; set; }
      float Latitude { get; set; }
      Boolean Deleted { get; set; }
      Boolean InActive { get; set; }
      DateTime DateCreated { get; set; }
      DateTime DateUpdated { get; set; }
      long CreatedByUserID { get; set; }
      long UpdatedByUserID { get; set; }

      Boolean? IsPepsiDistributor { get; set; }
   }
} //End namespace
