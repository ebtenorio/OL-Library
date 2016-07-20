
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOMessageInbound : IMessageInbound
    {
        public Int64 MessageinboundID { get; set; }
        public Int64 OrderID { get; set; }
        public Int64 CustomerID { get; set; }
        public Boolean SentFlag { get; set; }
        public Boolean Error { get; set; }
        public DateTime? DateSent { get; set; }
        public string MessageInboundType { get; set; }

    }
    public class DTOMessageInboundList : List<DTOMessageInbound>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
