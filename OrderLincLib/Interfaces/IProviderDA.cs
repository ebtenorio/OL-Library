using System;
using OrderLinc.DTOs;
using System.Data;

namespace OrderLinc.IDataContracts
{
    public interface IProviderDA
    {
        DTOProvider ProviderCreateRecord(DTOProvider mDTO);
        DTOProvider ProviderUpdateRecord(DTOProvider mDTO);
        DTOProvider ProviderDeleteRecord(DTOProvider mDTO);
        DTOProvider ProviderListByID(long mRecNo);
        DTOProvider ProviderListByProviderCode(string providerCode);

        DTOProviderCustomer ProviderCustomerCreateRecord(DTOProviderCustomer mDTO);
        DTOProviderCustomer ProviderCustomerDeleteRecord(DTOProviderCustomer mDTO);
        DTOProviderCustomer ProviderCustomerUpdateRecord(DTOProviderCustomer mDTO);
        DTOProviderCustomer ProviderCustomerListByID(int mRecNo);

        DTOProviderCustomerList ProviderCustomerList();
        DTOProviderCustomerList ProviderCustomerListByCustomerID(int mRecNo); 
        DTOProviderCustomerList ProviderCustomerListByProviderID(int mRecNo);
        DTOProviderCustomer ProviderCustomerList(long providerID, long customerID);

        DataTable ProviderDataTable();

        DTOProviderList ProviderList();
        DTOProviderList ProviderListByAddressID(long mRecNo);
        DTOProviderList ProviderListBySalesOrgID(long mRecNo);
        DTOProviderList ProviderListBySalesOrgIDWithFilter(long mRecNo);

        // DTOContact ContactListByProviderID(long providerID);

        DTOProviderList ProviderListByUpdatedByUserID(int mRecNo);
    
        DTOProviderProduct ProviderProductCreateRecord(DTOProviderProduct mDTO);
        DTOProviderProduct ProviderProductDeleteRecord(DTOProviderProduct mDTO);
        DTOProviderProduct ProviderProductListByID(int mRecNo);
        DTOProviderProduct ProviderProductUpdateRecord(DTOProviderProduct mDTO);

        DTOProviderProductList ProviderProductList();
        DTOProviderProduct ProviderProductListByProductID(long providerID, long productID);

        DTOProviderProductList ProviderProductListByProductID(int mRecNo);
        DTOProviderProductList ProviderProductListByProviderID(int mRecNo);

        DTOProviderWarehouse ProviderWarehouseCreateRecord(DTOProviderWarehouse mDTO);
        DTOProviderWarehouse ProviderWarehouseDeleteRecord(DTOProviderWarehouse mDTO);
        DTOProviderWarehouse ProviderWarehouseUpdateRecord(DTOProviderWarehouse mDTO);
        DTOProviderWarehouse ProviderWarehouseListByID(int mRecNo);
        DTOProviderWarehouse ProviderWarehouseByCriteria(ProviderWarehouseCriteria criteria);

        DataTable ProviderWarehouseDataTableByProviderID(int providerID);
        DTOProviderWarehouseList ProviderWarehouseListByProviderID(int mRecNo);
        DTOProviderWarehouseList ProviderWarehouseList();
        DTOProviderWarehouseList ProviderWarehouseListByAddressID(int mRecNo);

        Int64 CheckifProductCodeExist_by_SalesOrgAndProvider(int SalesOrgID, int ProviderID, int ProductID, string ProductCode);

        DTOProviderCustomer CheckifCustomerCodeExist_by_SalesOrgAndProvider(int SalesOrgID, int ProviderID, int ProductID, string CustomerCode);
        DTOProviderCustomerList ProviderCustomerListbyCustomerID(long CustomerID);
        void UpdateCustomerCode(long customerID, long providerID, string newCustomerCode);

        // Additional
        DTOContact ContactListByProviderWareHouseID(long providerWarehouseID);
    }
}
