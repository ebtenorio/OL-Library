
//CASE Generated Code 5/7/2014 3:15:09 PM

using System;

namespace OrderLinc.IDataContracts
{
    public interface IOrder
    {
        Int64 OrderID { get; set; }
        Int64 SalesOrgID { get; set; }
        Int64 CustomerID { get; set; }
        Int64 SalesRepAccountID { get; set; }
        Int64 ProviderID { get; set; }
        int ProviderWarehouseID { get; set; }
        DateTime OrderDate { get; set; }
        DateTime? DeliveryDate { get; set; }
        DateTime? InvoiceDate { get; set; }
        int SYSOrderStatusID { get; set; }
        string OrderNumber { get; set; }
        DateTime? ReceivedDate { get; set; }
        DateTime? ReleaseDate { get; set; }
        Boolean IsSent { get; set; }
        Boolean IsHeld { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateUpdated { get; set; }
        long CreatedByUserID { get; set; }
        long UpdatedByUserID { get; set; }
        DateTime? HoldUntilDate { get; set; }
        Boolean IsMobile { get; set; }
        string OrderGUID { get; set; }

        string ProviderCustomerCode { get; set; }

        // Additional Fields for PepsiCo Pre-sell and Future-dated orders
        Boolean? IsRegularOrder { get; set; }
        DateTime? RequestedReleaseDate { get; set; }

        // Pepsi Distributor use
        string PONumber { get; set; }
        
    }
} //End namespace
