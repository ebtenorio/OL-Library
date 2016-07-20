
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
    public interface IOrderLine
    {
        Int64 OrderLineID { get; set; }
        Int64 OrderID { get; set; }
        int LineNum { get; set; }
        Int64 ProductID { get; set; }
        float OrderQty { get; set; }
        float DespatchQty { get; set; }
        string UOM { get; set; }
        float OrderPrice { get; set; }
        float DespatchPrice { get; set; }
        string ItemStatus { get; set; }
        string ErrorText { get; set; }
        string ProductCode { get; set; }
        string ProductName { get; set; }
        string GTINCode { get; set; }
        string PrimarySKU { get; set; }

        float Discount { get; set; }

    }

} //End namespace
