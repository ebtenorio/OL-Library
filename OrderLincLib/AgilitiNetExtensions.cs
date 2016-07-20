using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OrderLinc
{
    public static class AgilitiNetExtensions
    {
        /// <summary>
        /// Returns DBNull.Value if datetime is null
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static object ToDataRowValue(this DateTime? datetime)
        {
            if (datetime.HasValue) return datetime.Value;
            return DBNull.Value;
        }

        public static bool IsExists(this DataRow dr, string colName)
        {
            if (dr.Table.Columns.Contains(colName)) return true;
            return false;
        }
    }
}
