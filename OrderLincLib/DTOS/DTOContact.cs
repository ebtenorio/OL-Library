
//CASE Generated Code 5/7/2014 3:15:13 PM

using System;
using System.Collections.Generic;
using OrderLinc.IDataContracts;
namespace OrderLinc.DTOs
{

    public class DTOContact : IContact
    {
        public long ContactID { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public long CreatedByUserID { get; set; }

        public Int64 OldID { get; set; }

        public bool IsEmpty
        {
            get
            {
                bool isEmpty = true;

                if (!string.IsNullOrEmpty(Phone))
                    isEmpty = false;
                if (!string.IsNullOrEmpty(Fax))
                    isEmpty = false;
                if (!string.IsNullOrEmpty(Mobile))
                    isEmpty = false;
                if (!string.IsNullOrEmpty(Email))
                    isEmpty = false;
                if (!string.IsNullOrEmpty(LastName))
                    isEmpty = false;
                if (!string.IsNullOrEmpty(FirstName))
                    isEmpty = false;
                return isEmpty;
            }
        }
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
    }
    public class DTOContactList : List<DTOContact>, IDisposable
    {
        public void Dispose()
        {
        }
    }

} //End namespace DTO's
