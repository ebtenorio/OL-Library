using System;
using OrderLinc.DTOs;
using System.Data;

namespace OrderLinc.IDataContracts
{
    public interface IProviderService
    {
        DataTable ProviderDataTable();
        DTOProvider ProviderListByID(long mRecNo);
        DTOProvider ProviderListByProviderCode(string providerCode);
        DTOProvider ProviderSaveRecord(DTOProvider mDTO);
        void ProviderDeleteRecord(long providerID);

        //DTOProviderCustomer ProviderCustomerSaveRecord(DTOProviderCustomer mDTO);
        //void ProviderCustomerDeleteRecord(long providerCustomerID);
        //DTOProviderCustomer ProviderCustomerListByID(int mRecNo);

        //bool ProviderCustomerIsValid(DTOProviderCustomer mDTO, out string mValidationResponse);
        //DTOProviderCustomerList ProviderCustomerList();
        //DTOProviderCustomerList ProviderCustomerListByCustomerID(int mRecNo);
        //DTOProviderCustomerList ProviderCustomerListByProviderID(int mRecNo);

       // DTOContact ContactListByProviderID(long providerID);
       void ProviderUpdateCustomerCode(long customerID, long providerID, string newCustomerCode);
       DTOProviderCustomer ProviderCustomerSaveRecord(DTOProviderCustomer mDTO);
       DTOProviderCustomer ProviderCustomerByProviderCustomer(long providerID, long customerID);

       bool ProviderIsValid(DTOProvider mDTO, out string mValidationResponse);

        DTOProviderList ProviderList();
        DTOProviderList ProviderListByAddressID(long mRecNo);
        DTOProviderList ProviderListBySalesOrgID(long mRecNo);

        DTOProviderList ProviderListBySalesOrgIDWithFilter(long mRecNo);

        
        DTOProviderProduct ProviderProductSaveRecord(DTOProviderProduct mDTO);
        void ProviderProductDeleteRecord(long mRecNo);
        DTOProviderProduct ProviderProductListByID(int mRecNo);
        DTOProviderProduct ProviderProductListByProductID(long providerID, long productID);

        bool ProviderProductIsValid(DTOProviderProduct mDTO, out string mValidationResponse);
        DTOProviderProductList ProviderProductList();
        DTOProviderProductList ProviderProductListByProductID(int mRecNo);
        DTOProviderProductList ProviderProductListByProviderID(int mRecNo);
       
        bool ProviderWarehouseIsValid(DTOProviderWarehouse mDTO, out string mValidationResponse);
        DTOProviderWarehouseList ProviderWarehouseList();
        DTOProviderWarehouseList ProviderWarehouseListByAddressID(int mRecNo);
        DTOProviderWarehouseList ProviderWarehouseListByProviderID(int mRecNo);

        DTOProviderWarehouse ProviderWarehouseListByID(int mRecNo);
        DTOProviderWarehouse ProviderWarehouseListByWarehouseCode(long providerID, string providerWarehouseCode);

        DTOProviderWarehouse ProviderWarehouseSaveRecord(DTOProviderWarehouse mDTO);

        Int64 CheckifProductCodeExist_by_SalesOrgAndProvider(int SalesOrgID, int ProviderID, int ProductID, string ProductCode);
        DTOProviderCustomer CheckifCustomerCodeExist_by_SalesOrgAndProvider(int SalesOrgID, int ProviderID, int ProductID, string CustomerCode);
        DTOProviderCustomerList ProviderCustomerListbyCustomerID(int CustomerID);
        void ProviderWarehouseDeleteRecord(int providerWarehouseID);
        DTOContact ContactListByProviderWareHouseID(long providerWarehouseID);
    }
}
