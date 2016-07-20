using System;
using OrderLinc.DTOs;
namespace OrderLinc.IDataContracts
{
    public interface IContactDA
    {
        DTOContact ContactCreateRecord(DTOContact mDTO);
        DTOContact ContactDeleteRecord(DTOContact mDTO);
        DTOContact ContactListByID(long mRecNo);
        DTOContact ContactListByAccountID(long accountID);
        DTOContact ContactUpdateRecord(DTOContact mDTO);
        DTOContactList ContactListBySalesOrgID(long salesOrgID, AccountType accountTypeID);
        DTOContact ContactListBySalesOrgID(long salesOrgID);

        DTOContact ContactListByProviderID(long providerID);
    }
}
