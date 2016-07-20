
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
using System.Text;
namespace OrderLinc.DTOs
{

    public class DTOAddress : IAddress
    {
        public DTOAddress()
        {

        }
        public Int64 AddressID { get; set; }
        public int AddressTypeID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CitySuburb { get; set; }
        public int SYSStateID { get; set; }
        public string PostalZipCode { get; set; }
        public long CreatedByUserID { get; set; }


        public bool IsEmpty
        {
            get
            {
                bool isEmpty = true;
                if (AddressTypeID > 0)
                    isEmpty = false;
                if (!string.IsNullOrEmpty(AddressLine1))
                    isEmpty = false;
                if (!string.IsNullOrEmpty(AddressLine2))
                    isEmpty = false;
                if (!string.IsNullOrEmpty(CitySuburb))
                    isEmpty = false;
                if (SYSStateID > 0)
                    isEmpty = false;
                if (!string.IsNullOrEmpty(PostalZipCode))
                    isEmpty = false;

                return isEmpty;
            }
        }
        /// <summary>
        /// For Display use only
        /// </summary>
        public string StateCode { get; set; }
        /// <summary>
        /// For Display use only
        /// </summary>
        public string StateName { get; set; }
    }
    public class DTOAddressList : List<DTOAddress>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
