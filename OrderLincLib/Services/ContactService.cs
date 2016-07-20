using System;
using System.Data;
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;


namespace OrderLinc.Services
{
    public class ContactService : IContactService
    {

        IContactDA _contactDA;
        private ILogService _logService;

        public ContactService(IContactDA contactDA, ILogService logService)
        {
            _contactDA = contactDA;
            _logService = logService;
        }


        #region ************************tblContact CRUDS ******************************************

        public DTOContact ContactListByID(long mRecNo)
        {
            return _contactDA.ContactListByID(mRecNo);
        }

        public DTOContact ContactSaveRecord(DTOContact mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("product");

                if (mDTO.ContactID == 0)
                    return _contactDA.ContactCreateRecord(mDTO);
                else
                    return _contactDA.ContactUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Contact", ex.Message, mDTO.CreatedByUserID);
                throw;
            }
        }


        public bool ContactDeleteRecord(long contactID, long userID)
        {
            try
            {

                _contactDA.ContactDeleteRecord(new DTOContact() { ContactID = contactID });

                return true;
            }
            catch (Exception ex)
            {
                _logService.LogSave("Contact", ex.Message, userID);
                throw;
            }
        }


        #endregion


        public DTOContact ContactListByAccountID(long accountID)
        {
            return _contactDA.ContactListByAccountID(accountID);
        }

        public DTOContact ContactListBySalesOrgID(long salesOrgID)
        {
            return _contactDA.ContactListBySalesOrgID(salesOrgID);
        }

        public DTOContactList ContactListByAccountTypeSalesOrgID(long salesOrgID, AccountType accountType)
        {
            return _contactDA.ContactListBySalesOrgID(salesOrgID, accountType);
        }
    } //end class
}