using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderLinc.DTOs
{
    public class DTODataLoadConfig
    {
        public bool SendLogToSystemAdmin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SendLogToOfficeAdmin { get; set; }

        /// <summary>
        /// Required.
        /// </summary>
        public string DataLoadPath { get; set; }

        /// <summary>
        /// Time in 24-hour format [0-23].
        /// if >=0, the data load will start at the specified time, if -1, data load will depend on each interval.
        /// </summary>
        public int DataLoadTime { get; set; }

        /// <summary>
        /// Required will be used as timer check
        /// </summary>
        public int Interval { get; set; }

        public string Subject { get; set; }

        public string Footer { get; set; }

    }
}
