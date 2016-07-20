using System;
using OrderLinc.DTOs;
using System.Data;
namespace OrderLinc.IDataContracts
{
    public interface ICustomerDA
    {
        DTOContact ContactCreateRecord(DTOContact mDTO);
        DTOContact ContactDeleteRecord(DTOContact mDTO);
        DTOContactList ContactList();
        DTOContact ContactListByID(int mRecNo);
        DTOContact ContactUpdateRecord(DTOContact mDTO);

        DTOCustomer CustomerCreateRecord(DTOCustomer mDTO);
        DTOCustomer CustomerDeleteRecord(DTOCustomer mDTO);
        DTOCustomer CustomerListByID(long providerID, long customerID);
        DTOCustomer CustomerByCriteria(CustomerCriteria criteria);

        DataTable CustomerDataTableByProviderID(long providerID);
        DTOCustomerList CustomerListByProviderID(long providerID);
        DTOCustomerList CustomerListByCriteria(CustomerCriteria criteria);
        DTOCustomerList ListCustomerBySalesOrgNotInCustomerSalesRep(string customerName, long SalesOrgID, long AccountID, long SYSStateID, int CurrentPage, int PageItemCount);
        DTOCustomerList ListCustomerAllBySalesOrgNotInCustomerSalesRep(string customerName, long SalesOrgID, long AccountID, long SYSStateID);
        DTOCustomerSalesRepList ListAllAssignedCustomer(int AccountID);
        DTOCustomerSalesRep CustomerSalesRepCreateRecord(DTOCustomerSalesRep mDTO);
        DTOCustomerSalesRep CustomerSalesRepDeleteRecord(DTOCustomerSalesRep mDTO);
        //DTOCustomerSalesRepList CustomerSalesRepList();
        DTOCustomerSalesRepList CustomerSalesRepListByCriteria(CustomerCriteria customerCriteria);
        DTOCustomerSalesRep CustomerSalesRepListByID(long mRecNo);
        DTOCustomerSalesRep CustomerSalesRepUpdateRecord(DTOCustomerSalesRep mDTO);

        DTOCustomer CustomerUpdateRecord(DTOCustomer mDTO);
        DTOLogo LogoCreateRecord(DTOLogo mDTO);
        DTOLogo LogoDeleteRecord(DTOLogo mDTO);
        DTOLogoList LogoList();
        DTOLogo LogoListByID(int mRecNo);
        DTOLogo LogoUpdateRecord(DTOLogo mDTO);
        DTOOrgUnit OrgUnitCreateRecord(DTOOrgUnit mDTO);
        DTOOrgUnit OrgUnitDeleteRecord(DTOOrgUnit mDTO);
        DTOOrgUnitList OrgUnitList();
        DTOOrgUnit OrgUnitListByID(int mRecNo);
        DTOOrgUnit OrgUnitUpdateRecord(DTOOrgUnit mDTO);
      

        DataTable SalesOrgDataTable();

        DTOSalesOrgList SalesOrgList();

        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        DTOSalesOrg SalesOrgListByID(long mRecNo);


        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        DTOSalesOrg SalesOrgCreateRecord(DTOSalesOrg mDTO);

        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        DTOSalesOrg SalesOrgUpdateRecord(DTOSalesOrg mDTO);

        DTOSalesOrg SalesOrgListByShortName(string salesOrgShortName);

        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        DTOSalesOrg SalesOrgDeleteRecord(DTOSalesOrg mDTO);


        DTOOrgUnitList OrgUnitListBySalesOrgID(long salesOrgID);

        DTOLogo LogoListBySalesOrgID(long salesOrgID);
        DTOCustomerList CustomerListByProviderSearchPage(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount);

        DTOCustomerList CustomerListByProviderSearchPage_WithDateFilter(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount);
        
    }
}
