
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOSYSState : ISYSState
    {
        public int SYSStateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }

    }
    public class DTOSYSStateList : List<DTOSYSState>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
