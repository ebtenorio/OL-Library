
//CASE Generated Code 5/7/2014 3:15:10 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IRole
   {
      int RoleId { get; set; }
      string RoleName { get; set; }
      Boolean IsSystem { get; set; }
      DateTime CreatedDate { get; set; }
      DateTime UpdatedDate { get; set; }
      Int64 CreateByUserID { get; set; }
   }
} //End namespace
