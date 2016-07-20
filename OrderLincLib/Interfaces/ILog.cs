
//CASE Generated Code 5/7/2014 9:29:21 PM

using System;

namespace IDataContracts
{
   public interface ILog
   {
      Int64 LogID { get; set; }
      string Source { get; set; }
      string Description { get; set; }
      DateTime DateCreated { get; set; }
      long CreatedByUserID { get; set; }
   }
} //End namespace
