
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOOrder : IOrder
    {
        public DTOOrder()
        {
            OrderLine = new DTOOrderLineList();
            IsRegularOrder = true;
            RequestedReleaseDate = DateTime.Now.ToLocalTime();
        }

        public Int64 OrderID { get; set; }
        public Int64 SalesOrgID { get; set; }
        public Int64 CustomerID { get; set; }
        public Int64 SalesRepAccountID { get; set; }
        public Int64 ProviderID { get; set; }
        public int ProviderWarehouseID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int SYSOrderStatusID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public Boolean IsSent { get; set; }
        public Boolean IsHeld { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public long CreatedByUserID { get; set; }
        public long UpdatedByUserID { get; set; }
        public DateTime? HoldUntilDate { get; set; }
        public Boolean IsMobile { get; set; }
        public string OrderGUID { get; set; }

        public string ProviderCustomerCode { get; set; }

        public DTOOrderLineList OrderLine { get; set; }


        public string SYSOrderStatusText { get; set; }

        public string CustomerName { get; set; }

        public string ProviderName { get; set; }

        public string CreatedByName { get; set; }

        // Additional properties for PepsiCo Pre-sell and Future-dated Order
        public bool? IsRegularOrder { get; set; }

        public DateTime? RequestedReleaseDate { get; set; }

        public string PONumber { get; set; }
    }
    public class DTOOrderList : List<DTOOrder>, IDisposable
    {
        public void Dispose()
        {
        }

        public int TotalRows { get; set; }
    }

} //End namespace DTO's
