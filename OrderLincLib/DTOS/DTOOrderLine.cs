
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOOrderLine : IOrderLine
    {
        public Int64 OrderLineID { get; set; }
        public Int64 OrderID { get; set; }
        public int LineNum { get; set; }
        public Int64 ProductID { get; set; }
        public float OrderQty { get; set; }
        public float DespatchQty { get; set; }
        public string UOM { get; set; }
        public float OrderPrice { get; set; }
        public float DespatchPrice { get; set; }
        public string ItemStatus { get; set; }
        public string ErrorText { get; set; }
        public float Discount { get; set; }

        public string ProductCode { get; set; }

        public string PrimarySKU { get; set; }

        public string ProductName { get; set; }

        public string GTINCode { get; set; }
    }
    public class DTOOrderLineList : List<DTOOrderLine>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
