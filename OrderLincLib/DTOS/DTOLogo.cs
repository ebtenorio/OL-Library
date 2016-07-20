using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOLogo : ILogo
{
   public Int64 LogoID { get; set; }
   public int SalesOrgID { get; set; }
   public string LogoURL { get; set; }
   public string LogoDescription { get; set; }

}
   public class DTOLogoList : List<DTOLogo>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
