using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderLinc.DTOs
{
    public enum OrderSearchType
    {
        /// <summary>
        /// Parameter(OrderID)
        /// </summary>
        ByOrderID = 1,

        /// <summary>
        /// Parameter (OrderID, ReceiverCode, SenderCode)
        /// </summary>
        BySenderReceiver = 2,

        /// <summary>
        /// Parameters (RefID, AccountTypeID, AccountID, OrgUnitID)
        /// </summary>
        ByStatOrderCount = 3,

        /// <summary>
        /// Parameters (IsHeld, IsSent)
        /// </summary>
        ByStatus  = 4,

    }

    public class OrderCriteria
    {

        public OrderSearchType SearchType
        {
            get;
            set;
        }
        public long OrderID { get; set; }

        public string OrderNumber { get; set; }

        public string ReceiverCode { get; set; }

        public string SenderCode { get; set; }

        public int OrgUnitID { get; set; }

        public int CurrentPage { get; set; }

        public int PageItemCount { get; set; }

        public long AccountID { get; set; }

        public long RefID { get; set; }

        public int AccountTypeID { get; set; }

        public bool IsSent { get; set; }

        public bool IsHeld { get; set; }
    }


}
