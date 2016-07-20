
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

public class DTORole : IRole
{
   public int RoleId { get; set; }
   public string RoleName { get; set; }
   public Boolean IsSystem { get; set; }
   public DateTime CreatedDate { get; set; }
   public DateTime UpdatedDate { get; set; }
   public Int64 CreateByUserID { get; set; }

}
   public class DTORoleList : List<DTORole>, IDisposable
   {
      public void Dispose()
      {
      }
   }

} //End namespace DTO's
