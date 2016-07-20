using System;
using System.Data;
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;

namespace OrderLinc.Services
{
    public class AddressService : IAddressService
    {

        IAddressDA _addressDA;
        private ILogService _logService;

        public AddressService(IAddressDA addressDA, ILogService logService)
        {
            _addressDA = addressDA;
            _logService = logService;
        }

        #region ************************tblAddress CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddress AddressListByID(long mRecNo)
        {
            return _addressDA.AddressListByID(mRecNo);
        }

        public DTOAddress AddressSaveRecord(DTOAddress mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("product");

                if (mDTO.AddressID == 0)
                    return _addressDA.AddressCreateRecord(mDTO);
                else
                    return _addressDA.AddressUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Address", string.Format("AddressSaveRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }

        public DTOAddress AddressDeleteRecord(DTOAddress mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("product");

                if (mDTO.AddressID == 0)
                    return _addressDA.AddressCreateRecord(mDTO);
                else
                    return _addressDA.AddressUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Address", ex.Message, mDTO.CreatedByUserID);
                throw;
            }
        }

        public Boolean AddressIsValid(DTOAddress mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.AddressTypeID == 0)
            {
                mValidationResponse = "AddressTypeID cannnot be 0.";
                return false;
            }

            if (mDTO.AddressLine1 == null)
            {
                mValidationResponse = "AddressLine1 cannnot be null.";
                return false;
            }

            if (mDTO.AddressLine2 == null)
            {
                mValidationResponse = "AddressLine2 cannnot be null.";
                return false;
            }

            if (mDTO.CitySuburb == null)
            {
                mValidationResponse = "CitySuburb cannnot be null.";
                return false;
            }

            if (mDTO.SYSStateID == 0)
            {
                mValidationResponse = "SYSStateID cannnot be 0.";
                return false;
            }

            if (mDTO.PostalZipCode == null)
            {
                mValidationResponse = "PostalZipCode cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        #endregion


        #region ************************tblAddressType CRUDS ******************************************

        public DTOAddressTypeList AddressTypeList()
        {
            return _addressDA.AddressTypeList();
        }


        #endregion

        #region ************************tblSYSState CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSStateList SYSStateList()
        {
            return _addressDA.SYSStateList();
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSState SYSStateListByID(int mRecNo)
        {
            return _addressDA.SYSStateListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSState SYSStateCreateRecord(DTOSYSState mDTO)
        {
            return _addressDA.SYSStateCreateRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSState SYSStateUpdateRecord(DTOSYSState mDTO)
        {
            return _addressDA.SYSStateUpdateRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSState SYSStateDeleteRecord(DTOSYSState mDTO)
        {
            return _addressDA.SYSStateDeleteRecord(mDTO);
        }
        public DTOSYSState SYSStateListByCustomerID(long mRecNo)
        {
            return _addressDA.SYSStateListByCustomerID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public Boolean SYSStateIsValid(DTOSYSState mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.StateName == null)
            {
                mValidationResponse = "StateName cannnot be null.";
                return false;
            }

            if (mDTO.StateCode == null)
            {
                mValidationResponse = "StateCode cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        #endregion


        #region ************************tblRegion CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegionList RegionList()
        {

            return _addressDA.RegionList();
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegion RegionListByID(int mRecNo)
        {
            return _addressDA.RegionListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegion RegionCreateRecord(DTORegion mDTO)
        {
            return _addressDA.RegionCreateRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegion RegionUpdateRecord(DTORegion mDTO)
        {
            return _addressDA.RegionUpdateRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegion RegionDeleteRecord(DTORegion mDTO)
        {
            return _addressDA.RegionDeleteRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public Boolean RegionIsValid(DTORegion mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }

            if (mDTO.RegionName == null)
            {
                mValidationResponse = "RegionName cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        #endregion


    } //end class

}
