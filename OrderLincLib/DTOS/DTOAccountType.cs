
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOAccountType : IAccountType
    {
        public int AccountTypeID { get; set; }
        public string AccountTypeCode { get; set; }
        public string AccountTypeText { get; set; }

    }
    public class DTOAccountTypeList : List<DTOAccountType>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
