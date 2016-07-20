using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;

namespace OrderLinc.Services
{
    public class ProviderService : IProviderService
    {

        private IProviderDA _providerDA;
        private ILogService _logService;

        public ProviderService(IProviderDA providerDA, ILogService logService)
        {
            _logService = logService;
            _providerDA = providerDA;
        }

        #region ************************tblProvider CRUDS ******************************************

        // ContactListByProviderID
        
        public DTOContact ContactListByProviderWareHouseID(long providerID)
        {
            return _providerDA.ContactListByProviderWareHouseID(providerID);           
        }

        public DTOProvider ProviderListByProviderCode(string providerCode)
        {
            return _providerDA.ProviderListByProviderCode(providerCode);
        }

        public DataTable ProviderDataTable()
        {
            return _providerDA.ProviderDataTable();
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderList ProviderList()
        {
            return _providerDA.ProviderList();
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProvider ProviderListByID(long mRecNo)
        {
            return _providerDA.ProviderListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProvider ProviderSaveRecord(DTOProvider mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("provider");

                if (mDTO.ProviderID == 0)
                    _providerDA.ProviderCreateRecord(mDTO);
                else
                    _providerDA.ProviderUpdateRecord(mDTO);

                return mDTO;
            }
            catch (Exception ex)
            {
                _logService.LogSave("Provider", string.Format("ProviderSaveRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), mDTO.UpdatedByUserID);
                throw;
            }
        }

        public void ProviderDeleteRecord(long providerID)
        {
            _providerDA.ProviderDeleteRecord(new DTOProvider() { ProviderID = providerID });
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public Boolean ProviderIsValid(DTOProvider mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.ProviderCode == null)
            {
                mValidationResponse = "ProviderCode cannnot be null.";
                return false;
            }

            if (mDTO.BusinessNumber == null)
            {
                mValidationResponse = "BusinessNumber cannnot be null.";
                return false;
            }

            if (mDTO.ProviderName == null)
            {
                mValidationResponse = "ProviderName cannnot be null.";
                return false;
            }

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }

            if (mDTO.AddressID == 0)
            {
                mValidationResponse = "AddressID cannnot be 0.";
                return false;
            }

            if (mDTO.Longitude == 0)
            {
                mValidationResponse = "Longitude cannnot be 0.";
                return false;
            }

            if (mDTO.Latitude == 0)
            {
                mValidationResponse = "Latitude cannnot be 0.";
                return false;
            }

            if (mDTO.DateCreated == null)
            {
                mValidationResponse = "DateCreated cannnot be null.";
                return false;
            }

            if (mDTO.DateUpdated == null)
            {
                mValidationResponse = "DateUpdated cannnot be null.";
                return false;
            }

            if (mDTO.CreatedByUserID == 0)
            {
                mValidationResponse = "CreatedByUserID cannnot be 0.";
                return false;
            }

            if (mDTO.UpdatedByUserID == 0)
            {
                mValidationResponse = "UpdatedByUserID cannnot be 0.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderList ProviderListBySalesOrgID(long mRecNo)
        {
            return _providerDA.ProviderListBySalesOrgID(mRecNo);
        }
        public DTOProviderList ProviderListBySalesOrgIDWithFilter(long mRecNo)
        {
            return _providerDA.ProviderListBySalesOrgIDWithFilter(mRecNo);
        }
        
        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderList ProviderListByAddressID(long mRecNo)
        {
            return _providerDA.ProviderListByAddressID(mRecNo);
        }

        #endregion

        #region ************************tblProviderCustomer CRUDS ******************************************

        public void ProviderUpdateCustomerCode(long customerID, long providerID, string newCustomerCode)
        {
            try
            {
                _providerDA.UpdateCustomerCode(customerID, providerID, newCustomerCode);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Provider", string.Format("ProviderUpdateCustomerCode: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }



        public DTOProviderCustomer ProviderCustomerSaveRecord(DTOProviderCustomer mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("provider");

                if (mDTO.ProviderCustomerID == 0)
                    _providerDA.ProviderCustomerCreateRecord(mDTO);
                else
                    _providerDA.ProviderCustomerUpdateRecord(mDTO);

                return mDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DTOProviderCustomer ProviderCustomerByProviderCustomer(long providerID, long customerID)
        {
            try
            {
                return _providerDA.ProviderCustomerList(providerID, customerID);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Provider", string.Format("ProviderCustomerByProviderCustomer: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }

        #endregion

        #region ************************tblProviderProduct CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProductList ProviderProductList()
        {
            return _providerDA.ProviderProductList();
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProduct ProviderProductListByID(int mRecNo)
        {
            return _providerDA.ProviderProductListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProduct ProviderProductSaveRecord(DTOProviderProduct mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("provider product");

                if (mDTO.ProviderProductID == 0)
                    _providerDA.ProviderProductCreateRecord(mDTO);
                else
                    _providerDA.ProviderProductUpdateRecord(mDTO);

                return mDTO;
            }
            catch (Exception ex)
            {
                _logService.LogSave("Provider Product", string.Format("ProviderProductSaveRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }


        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public void ProviderProductDeleteRecord(long mRecNo)
        {
            _providerDA.ProviderProductDeleteRecord(new DTOProviderProduct() { ProviderProductID = mRecNo });
        }


        public Int64 CheckifProductCodeExist_by_SalesOrgAndProvider(int SalesOrgID, int ProviderID, int ProductID, string ProductCode)
        {


         return _providerDA.CheckifProductCodeExist_by_SalesOrgAndProvider(SalesOrgID, ProviderID, ProductID, ProductCode);

        }

        public DTOProviderCustomer CheckifCustomerCodeExist_by_SalesOrgAndProvider(int SalesOrgID, int ProviderID, int CustomerID, string CustomerCode)                                         
        {


            return _providerDA.CheckifCustomerCodeExist_by_SalesOrgAndProvider(SalesOrgID, ProviderID, CustomerID, CustomerCode);

        }

        public DTOProviderCustomerList ProviderCustomerListbyCustomerID(int CustomerID) {

            return _providerDA.ProviderCustomerListByCustomerID(CustomerID);
        
        }

        public Boolean ProviderProductIsValid(DTOProviderProduct mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.ProviderID == 0)
            {
                mValidationResponse = "ProviderID cannnot be 0.";
                return false;
            }

            if (mDTO.ProductID == 0)
            {
                mValidationResponse = "ProductID cannnot be 0.";
                return false;
            }

            if (mDTO.ProviderProductCode == null)
            {
                mValidationResponse = "ProviderProductCode cannnot be null.";
                return false;
            }

            if (mDTO.StartDate == null)
            {
                mValidationResponse = "StartDate cannnot be null.";
                return false;
            }

            if (mDTO.EndDate == null)
            {
                mValidationResponse = "EndDate cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProductList ProviderProductListByProviderID(int mRecNo)
        {
            return _providerDA.ProviderProductListByProviderID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProductList ProviderProductListByProductID(int mRecNo)
        {
            return _providerDA.ProviderProductListByProductID(mRecNo);
        }

        public DTOProviderProduct ProviderProductListByProductID(long providerID, long productID)
        {
            return _providerDA.ProviderProductListByProductID(providerID, productID);
        }


        #endregion

        #region ************************tblProviderWarehouse CRUDS ******************************************

        public DTOProviderWarehouse ProviderWarehouseListByWarehouseCode(long providerID, string providerWarehouseCode)
        {
            ProviderWarehouseCriteria criteria = new ProviderWarehouseCriteria()
            {
                SearchType = ProviderWarehouseSearchType.ByProviderWarehouseCode,
                ProviderID = providerID,
                ProviderWarehouseCode = providerWarehouseCode
            };

            return _providerDA.ProviderWarehouseByCriteria(criteria);

        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouseList ProviderWarehouseList()
        {
            return _providerDA.ProviderWarehouseList();
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouse ProviderWarehouseListByID(int mRecNo)
        {
            return _providerDA.ProviderWarehouseListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouse ProviderWarehouseSaveRecord(DTOProviderWarehouse mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("provider product");

                if (mDTO.ProviderWarehouseID == 0)
                    _providerDA.ProviderWarehouseCreateRecord(mDTO);
                else
                    _providerDA.ProviderWarehouseUpdateRecord(mDTO);

                return mDTO;
            }
            catch (Exception ex)
            {
                _logService.LogSave("Provider Warehouse", string.Format("ProviderWarehouseSaveRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }


        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public void ProviderWarehouseDeleteRecord(int mRecNo)
        {
            _providerDA.ProviderWarehouseDeleteRecord(new DTOProviderWarehouse() { ProviderWarehouseID = mRecNo });
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public Boolean ProviderWarehouseIsValid(DTOProviderWarehouse mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.ProviderID == 0)
            {
                mValidationResponse = "ProviderID cannnot be 0.";
                return false;
            }

            if (mDTO.ProviderWarehouseCode == null)
            {
                mValidationResponse = "ProviderWarehouseCode cannnot be null.";
                return false;
            }

            if (mDTO.ProviderWarehouseName == null)
            {
                mValidationResponse = "ProviderWarehouseName cannnot be null.";
                return false;
            }

            if (mDTO.AddressID == 0)
            {
                mValidationResponse = "AddressID cannnot be 0.";
                return false;
            }

            if (mDTO.Longitude == 0)
            {
                mValidationResponse = "Longitude cannnot be 0.";
                return false;
            }

            if (mDTO.Latitude == 0)
            {
                mValidationResponse = "Latitude cannnot be 0.";
                return false;
            }

            if (mDTO.DateCreated == null)
            {
                mValidationResponse = "DateCreated cannnot be null.";
                return false;
            }

            if (mDTO.DateUpdated == null)
            {
                mValidationResponse = "DateUpdated cannnot be null.";
                return false;
            }

            if (mDTO.CreatedByUserID == 0)
            {
                mValidationResponse = "CreatedByUserID cannnot be 0.";
                return false;
            }

            if (mDTO.UpdatedByUserID == 0)
            {
                mValidationResponse = "UpdatedByUserID cannnot be 0.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouseList ProviderWarehouseListByProviderID(int mRecNo)
        {
            return _providerDA.ProviderWarehouseListByProviderID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouseList ProviderWarehouseListByAddressID(int mRecNo)
        {
            return _providerDA.ProviderWarehouseListByAddressID(mRecNo);
        }

        #endregion

    }

}
