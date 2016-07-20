using System;
using OrderLinc.DTOs;
using System.Data;
namespace OrderLinc.IDataContracts
{
    public interface ICustomerService
    {        //bool ContactDeleteRecord(int contactID, long userID);
        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        bool ContactIsValid(DTOContact mDTO, out string mValidationResponse);
        bool CustomerDeleteRecord(long providerID, long customerID, long userId);
        bool CustomerIsValid(DTOCustomer mDTO, out string mValidationResponse);
        bool CustomerSalesRepIsValid(DTOCustomerSalesRep mDTO, out string mValidationResponse);
        bool LogoIsValid(DTOLogo mDTO, out string mValidationResponse);
        bool OrgUnitIsValid(DTOOrgUnit mDTO, out string mValidationResponse);
 
        DataTable CustomerDataTableByProviderID(long providerID);
        DataTable SalesOrgDataTable();

        [Obsolete("Use AddressService instead.")]
        DTOAddress AddressListByID(long addressID);

        [Obsolete("Use AddressService instead.")]
        DTOAddress AddressSaveRecord(DTOAddress mDTO);

        DTOContact ContactListByID(long mRecNo);
        DTOContact ContactListBySalesOrgID(long salesOrgID);
        DTOContact ContactSaveRecord(DTOContact mDTO);
        DTOContactList ContactListByAccountTypeSalesOrgID(long salesOrgID, AccountType accountType);

        DTOCustomer CustomerListByBusinessNumber(long salesOrgID, string businessNumber,long CustomerID);
        DTOCustomer CustomerListByCustomerName(long salesOrgID, string customerName, Int64 CustomerID);
        DTOCustomer CustomerListByID(long providerID, long customerID);
        DTOCustomer CustomerSaveRecord(DTOCustomer mDTO);
        DTOCustomer CustomerListByCustomerCode(long providerID, long salesOrgID, string customerCode);
        DTOCustomerList CustomerListByProviderID(long providerID);
        DTOCustomerList CustomerListBySearch(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount);
        DTOCustomerList CustomerListByProviderSearchPage(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount);
        DTOCustomerList CustomerListByProviderSearchPage_WithDateFilter(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount);

       
        DTOCustomerList CustomerListBySalesOrgSearch(long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount);
        DTOCustomerList CustomerListBySalesRepID(long salesRepAccountID, int currentPage, int pageItemCount);

        //DTOCustomerSalesRep CustomerSalesRepCreateRecord(DTOCustomerSalesRep mDTO);
        DTOCustomerSalesRep CustomerSalesRepDeleteRecord(DTOCustomerSalesRep mDTO);
        DTOCustomerSalesRep CustomerSalesRepListByID(int mRecNo);
        DTOCustomerSalesRep CustomerSalesRepSaveRecord(DTOCustomerSalesRep mDTO);
        DTOCustomerSalesRepList CustomerSalesRepListSearchSalesRepAndCustomer(long salesRepID, long customerID);
        DTOCustomerSalesRepList CustomerSalesRepListBySalesRepID(long salesRepAccountID, int CurrentPage, int PageItemCount);
        DTOCustomerList ListCustomerBySalesOrgNotInCustomerSalesRep(string customerName, long SalesOrgID, long AccountID, long SYSStateID, int CurrentPage, int PageItemCount);
        DTOCustomerList ListCustomerAllBySalesOrgNotInCustomerSalesRep(string customerName, long SalesOrgID, long AccountID, long SYSStateID);
        DTOCustomerSalesRepList ListAllAssignedCustomer( int AccountID);
        DTOLogo LogoCreateRecord(DTOLogo mDTO);
        DTOLogo LogoDeleteRecord(DTOLogo mDTO);
        DTOLogo LogoListByID(int mRecNo);
        DTOLogo LogoUpdateRecord(DTOLogo mDTO);

        DTOLogoList LogoList();
        DTOOrgUnit OrgUnitCreateRecord(DTOOrgUnit mDTO);
        DTOOrgUnit OrgUnitDeleteRecord(DTOOrgUnit mDTO);
        DTOOrgUnit OrgUnitListByID(int mRecNo);
        DTOOrgUnit OrgUnitUpdateRecord(DTOOrgUnit mDTO);
        DTOOrgUnitList OrgUnitList();
        DTOOrgUnitList OrgUnitListBySalesOrgID(long salesOrgID);


        DTOSalesOrg SalesOrgListBySalesOrgShortName(string salesOrgShortName);
        DTOSalesOrg SalesOrgListByID(long mRecNo);
        DTOSalesOrg SalesOrgSaveRecord(DTOSalesOrg mDTO);
        DTOSalesOrgList SalesOrgList();

        void SalesOrgDeleteRecord(long mRecNo);


        DTOLogo LogoListBySalesOrgID(long p);
    }
}
