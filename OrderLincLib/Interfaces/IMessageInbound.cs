
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
   public interface IMessageInbound
   {
      Int64 MessageinboundID { get; set; }
      Int64 OrderID { get; set; }
      Int64 CustomerID { get; set; }
      Boolean SentFlag { get; set; }
      Boolean Error { get; set; }
      DateTime? DateSent { get; set; }
   }
} //End namespace
