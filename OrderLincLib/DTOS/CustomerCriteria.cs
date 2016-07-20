using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderLinc.DTOs
{
    public enum CustomerSearchType
    {
        ByCustomerSalesRepID = 1,
        ByCustomerID = 2,
        ByCustomerAndSalesRep = 3,

        /// <summary>
        /// Parameters ( CustomerName, SalesOrg).
        /// Rule: No duplicate customer within salesorg
        /// </summary>
        ByCheckCustomerName = 4,

        /// <summary>
        /// Parameters (CustomerSearch, CurrentPage, PageItemCount, SalesOrgID, ProviderID, SYSStateID 
        /// </summary>
        BySearch = 5,

        ByCustomerCode = 6,
        BySalesRepID,

        /// <summary>
        /// Parameters (BusinessNumber)
        /// </summary>
        ByBusinessNumber,

        ByCustomerSalesOrgSearch
    }

    public class CustomerCriteria
    {
        public long RefID { get; set; }

        public CustomerSearchType SearchType { get; set; }

        public string CustomerName { get; set; }

        public string CustomerCode { get; set; }

        public long AccountID { get; set; }

        public int OrgUnitID { get; set; }

        public int CurrentPage { get; set; }

        public int PageItemCount { get; set; }

        public long CustomerSalesRepID { get; set; }

        public long CustomerID { get; set; }

        public long SalesOrgID { get; set; }

        public long ProviderID { get; set; }

        public int SYSStateID { get; set; }


        public string BusinessNumber { get; set; }
    }


}
