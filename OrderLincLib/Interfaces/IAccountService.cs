using System;
using OrderLinc.DTOs;
using System.Data;

namespace OrderLinc.IDataContracts
{
    public interface IAccountService
    {
        bool AccountIsValid(DTOAccount mDTO, out string mValidationResponse);
        DataTable AccountSalesRepDataTable();
        DTOAccount AccountAuthenticate(string userName, string password);
        DTOAccount AccountListByUsername(long salesOrgID, string username);
        DTOAccount AccountListByUsername(long salesOrgID, string userName, string password);

        DTOMobileAccount AccountAuthenticate(string userName, string password, string deviceNo);

        bool IsAccountExistingAcrossAllOtherSalesOrgs(long salesOrgID, string userName, string password);
        
        DTOAccount AccountListByID(long mRecNo);

        DTOAccount AccountSaveRecord(DTOAccount mDTO);

        DTOAccountList AccountListBySearchAccountType(int mRecNo, AccountType accountTypeID, int currentPage, int pageItemCount, string searchText);
        DTOAccountList AccountListBySearch(int orgUnitID, long refID, int currentPage, int pageItemCount, string searchText);

        DTOAccountList AccountListByAccountTypeID(int accountTypeID);

        DTOAccountList AccountListByStateIDAndSalesOrgID(long StateID, long SalesOrgID);

        [Obsolete("Use AddressService instead.")]
        DTOAddress AddressListByID(long addressID);

        [Obsolete("Use AddressService instead.")]
        DTOAddress AddressSaveRecord(DTOAddress mDTO);

        DTOContact ContactListByAccountID(long accountID);
        DTOContact ContactListByID(long contactID);
        DTOContact ContactSaveRecord(DTOContact mDTO);
        void AccountDeleteRecord(long mRecNo);
        void AccountSaveRecord(DTOAccount mDTO, DTOContact contact, DTOAddress address);
    

        //DTOAccountList AccountList();
        //DTOAccountList AccountListByOrgUnitID(int mRecNo);
        //DTOAccountList AccountListByRefID(int mRecNo);
        //DTOAccountList AccountListByRoleID(int mRecNo);
        //DTOAccountList AccountListByServerID(int mRecNo);
        //DTOAccountTypeList AccountTypeListByNotAccountTypeID(int mRecNo);

        //DTOAccountType AccountTypeSaveRecord(DTOAccountType mDTO);
        //DTOAccountType AccountTypeDeleteRecord(DTOAccountType mDTO);
        //bool AccountTypeIsValid(DTOAccountType mDTO, out string mValidationResponse);
        DTOAccountTypeList AccountTypeList();
        //DTOAccountType AccountTypeListByID(int mRecNo);

        Int16 CheckNewAccountDetails(String pUsername, String pPassword, String pSalesOrgID, String pAccountID);

    }
}
