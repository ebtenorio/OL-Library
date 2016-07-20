using System;
using OrderLinc.DTOs;
using System.Data;

namespace OrderLinc.IDataContracts
{
    public interface IAccountDA
    {
        DTOAccount AccountCreateRecord(DTOAccount mDTO);
        DTOAccount AccountDeleteRecord(DTOAccount mDTO);
        DTOAccount AccountUpdateRecord(DTOAccount mDTO);
        DTOAccount AccountByCriteria(AccountCriteria criteria);
        DataTable AccountDataTableByAccountTypeID(int accountTypeID);


        //DTOAccountList AccountList();
        DTOAccountList AccountListByAccountTypeID(int mRecNo);
        DTOAccountList AccountListByStateIDAndSalesOrgID(long StateID, long SalesOrgID);
        //DTOAccountList AccountListByOrgUnitID(int mRecNo);
        //DTOAccountList AccountListByRefID(int mRecNo);
        //DTOAccountList AccountListByRoleID(int mRecNo);
        //DTOAccountList AccountListByServerID(int mRecNo);
        DTOAccountList AccountListByCriteria(AccountCriteria criteria);
        DTOAccountType AccountTypeCreateRecord(DTOAccountType mDTO);
        DTOAccountType AccountTypeDeleteRecord(DTOAccountType mDTO);
        
        DTOAccountType AccountTypeListByID(int mRecNo);
        DTOAccountType AccountTypeUpdateRecord(DTOAccountType mDTO);
        
        DTOAccountTypeList AccountTypeList();
       // DTOAccountTypeList AccountTypeListByNotEqualAccountTypeID(int accountTypeID);


        // My Details
        Int16 CheckNewAccountDetails(string pUsername, string pPassword, string pSalesOrgID, string AccountID);

    }
}
