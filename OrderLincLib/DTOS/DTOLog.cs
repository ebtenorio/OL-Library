using System;
using System.Collections.Generic;
using IDataContracts;

namespace OrderLinc.DTOs
{

    public class DTOLog : ILog
    {
        public Int64 LogID { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public long CreatedByUserID { get; set; }

    }

    public class DTOLogList : List<DTOLog>, IDisposable
    {
        public void Dispose()
        {
        }
    }
} 
