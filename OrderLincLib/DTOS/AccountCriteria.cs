using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderLinc.DTOs
{
    public enum AccountSearchType
    {
        ByAccountID = 1,
        ByUserName = 2,
        ByUserNameAndPassword = 3,
        ByUserNameAndPasswordWithDeviceNo = 4,
        ByOrgUnitWithAccountTypeIDSearch = 5,
        ByOrgUnitSearch = 6,

        /// <summary>
        /// Parameters (UserName, BusinessNumber)
        /// </summary>
        //ByBusinessNumber = 7
        BySalesOrgUsernamePassword,
        ByOtherSalesOrgsUsernamePassword

    }

    public class AccountCriteria
    {
        //public long RefID { get; set; }

        public AccountSearchType SearchType { get; set; }

        public long AccountID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string DeviceNo { get; set; }

        public string SearchText { get; set; }

        public AccountType AccountTypeID { get; set; }

        //public string BusinessNumber { get; set; }

        public int OrgUnitID { get; set; }

        public int CurrentPage { get; set; }

        public int PageItemCount { get; set; }

        public long SalesOrgID { get; set; }
    }


}
