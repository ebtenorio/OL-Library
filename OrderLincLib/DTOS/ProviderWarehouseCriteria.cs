using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderLinc.DTOs
{
    public enum ProviderWarehouseSearchType
    {
        /// <summary>
        /// Parameters (ProviderID, WarehouseCode)
        /// </summary>
        ByProviderWarehouseCode = 1,

    }

    public class ProviderWarehouseCriteria
    {
      
        public ProviderWarehouseSearchType SearchType { get; set; }

        public string ProviderWarehouseCode { get; set; }

        public long ProviderID { get; set; }
    }


}
