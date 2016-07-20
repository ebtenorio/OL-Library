using System;
using OrderLinc.DTOs;

namespace OrderLinc.IDataContracts
{
    public interface IContactService
    {
        bool ContactDeleteRecord(long contactID, long userID);
        DTOContact ContactListByID(long mRecNo);
        DTOContact ContactSaveRecord(OrderLinc.DTOs.DTOContact mDTO);

        DTOContact ContactListByAccountID(long accountID);
        DTOContactList ContactListByAccountTypeSalesOrgID(long salesOrgID, AccountType accountType);
        DTOContact ContactListBySalesOrgID(long salesOrgID);
    }
}
