
using System;

namespace OrderLinc.IDataContracts
{
    public interface IAccountType
    {
        int AccountTypeID { get; set; }
        string AccountTypeCode { get; set; }
        string AccountTypeText { get; set; }
    }
} 
