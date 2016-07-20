
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTOOrgUnit : IOrgUnit
{
   public int OrgUnitID { get; set; }
   public int SalesOrgID { get; set; }
   public string OrgUnitName { get; set; }

}
   public class DTOOrgUnitList : List<DTOOrgUnit>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
