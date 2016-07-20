using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;
using System.Transactions;

namespace OrderLinc.Services
{
    public class CustomerService : ICustomerService
    {

        private ICustomerDA _customerDA;
        private ILogService _logService;
        private IProviderDA _providerDA;
        const string source = "Customer";
        private IContactService _contactService;
        private IAddressService _addressService;

        public CustomerService(ICustomerDA customerDA, IProviderDA providerDA, IContactService contactService, IAddressService addressService, ILogService logService)
        {
            _logService = logService;
            _customerDA = customerDA;
            _providerDA = providerDA;
            _contactService = contactService;
            _addressService = addressService;
        }

        #region ************************tblContact CRUDS ******************************************

        public Boolean ContactIsValid(DTOContact mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.Phone == null)
            {
                mValidationResponse = "Phone cannnot be null.";
                return false;
            }

            if (mDTO.Fax == null)
            {
                mValidationResponse = "Fax cannnot be null.";
                return false;
            }

            if (mDTO.Mobile == null)
            {
                mValidationResponse = "Mobile cannnot be null.";
                return false;
            }

            if (mDTO.Email == null)
            {
                mValidationResponse = "Email cannnot be null.";
                return false;
            }

            if (mDTO.LastName == null)
            {
                mValidationResponse = "LastName cannnot be null.";
                return false;
            }

            if (mDTO.FirstName == null)
            {
                mValidationResponse = "FirstName cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        public DTOContact ContactSaveRecord(DTOContact mDTO)
        {
            try
            {
                return _contactService.ContactSaveRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DTOContact ContactListByID(long contactID)
        {
            return _contactService.ContactListByID(contactID);
        }

        public DTOContact ContactListBySalesOrgID(long salesOrgID)
        {
            return _contactService.ContactListBySalesOrgID(salesOrgID);
        }

        public DTOContactList ContactListByAccountTypeSalesOrgID(long salesOrgID, AccountType accountType)
        {
            return _contactService.ContactListByAccountTypeSalesOrgID(salesOrgID, accountType);
        }


        #endregion


        #region ************************tblCustomer CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomer CustomerListByID(long providerID, long customerID)
        {
            return _customerDA.CustomerListByID(providerID, customerID);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomer CustomerSaveRecord(DTOCustomer mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("customer");

                if (mDTO.CustomerID == 0)
                    return _customerDA.CustomerCreateRecord(mDTO);
                else
                    return _customerDA.CustomerUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                _logService.LogSave(source, ex.Message, mDTO.ContactID);
                throw;
            }

        }

        public void CustomerSaveRecord(DTOCustomer mDTOCustomer, DTOContact dtoContact, DTOAddress dtoBillAddress, DTOAddress dtoShipToAddress, DTOAddress address)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (dtoBillAddress != null)
                    {
                        this._addressService.AddressSaveRecord(dtoBillAddress);
                    }
                    if (dtoShipToAddress != null)
                    {
                        this._addressService.AddressSaveRecord(dtoShipToAddress);
                    }
                    if (address != null)
                    {
                        this._addressService.AddressSaveRecord(address);
                    }
                    if (dtoContact != null)
                    {
                        ContactSaveRecord(dtoContact);
                    }

                    mDTOCustomer.ContactID = dtoContact == null ? 0 : dtoContact.ContactID;
                    mDTOCustomer.ShipToAddressID = dtoShipToAddress == null ? 0 : dtoShipToAddress.AddressID;
                    mDTOCustomer.BillToAddressID = dtoBillAddress == null ? 0 : dtoBillAddress.AddressID;
                    mDTOCustomer.AddressID = address == null ? 0 : address.AddressID;
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                _logService.LogSave(source, ex.Message, mDTOCustomer.CustomerID);
                throw;
            }
        }

        public bool CustomerDeleteRecord(long providerID, long customerID, long userId)
        {
            bool retValue = false;
            try
            {
                _providerDA.ProviderCustomerDeleteRecord(new DTOProviderCustomer()
                {
                    CustomerID = customerID,
                    ProviderID = providerID
                });
                retValue = true;
                _logService.LogSave(source, string.Format("Delete - ProviderID:{0}, CustomerID:{1}", providerID, customerID), userId);

                return retValue;
            }
            catch (Exception ex)
            {
                _logService.LogSave(source, ex.Message, userId);
                throw;
            }
            finally
            {

            }
        }

        public DataTable CustomerDataTableByProviderID(long providerID)
        {
            return _customerDA.CustomerDataTableByProviderID(providerID);
        }

        public DTOCustomerList CustomerListByProviderID(long providerID)
        {
            return _customerDA.CustomerListByProviderID(providerID);
        }

        public DTOCustomerList CustomerListBySalesRepID(long salesRepAccountID, int currentPage, int pageItemCount)
        {
            CustomerCriteria criteria = new CustomerCriteria()
            {
                AccountID = salesRepAccountID,
                CurrentPage = currentPage,
                PageItemCount = pageItemCount,
                SearchType = CustomerSearchType.BySalesRepID
            };
            return _customerDA.CustomerListByCriteria(criteria);
        }


        public DTOCustomer CustomerListByCustomerCode(long providerID, long salesOrgID, string customerCode)
        {
            CustomerCriteria criter = new CustomerCriteria()
            {
                CustomerCode = customerCode,
                SalesOrgID = salesOrgID,
                ProviderID = providerID,
                SearchType = CustomerSearchType.ByCustomerCode
            };
            return _customerDA.CustomerByCriteria(criter);
        }

        public DTOCustomer CustomerListByCustomerName(long salesOrgID, string customerName, Int64 customerID)
        {
            CustomerCriteria criteria = new CustomerCriteria()
            {
                CustomerName = customerName,
                SalesOrgID = salesOrgID,
                CustomerID = customerID,
                SearchType = CustomerSearchType.ByCheckCustomerName
            };
            return _customerDA.CustomerByCriteria(criteria);
        }

        public DTOCustomer CustomerListByBusinessNumber(long salesOrgID ,string businessNumber, long customerID)
        {
            CustomerCriteria criteria = new CustomerCriteria()
            {
                BusinessNumber = businessNumber,
                SalesOrgID = salesOrgID,
                CustomerID = customerID,
                SearchType = CustomerSearchType.ByBusinessNumber
            };
            return _customerDA.CustomerByCriteria(criteria);
        }


        public DTOCustomerList CustomerListBySearch(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount)
        {
            CustomerCriteria criteria = new CustomerCriteria()
            {
                CustomerName = customerName,
                SalesOrgID = salesOrgID,
                ProviderID = providerID,
                SYSStateID = stateID,
                CurrentPage = currentPage,
                PageItemCount = pageItemCount,
                SearchType = CustomerSearchType.BySearch
            };
            return _customerDA.CustomerListByCriteria(criteria);
        }

        public DTOCustomerList CustomerListByProviderSearchPage(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount)
        {
            return _customerDA.CustomerListByProviderSearchPage(providerID, salesOrgID, stateID, customerName, currentPage, pageItemCount);
        }

        public DTOCustomerList CustomerListByProviderSearchPage_WithDateFilter(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount)
        {
            return _customerDA.CustomerListByProviderSearchPage_WithDateFilter(providerID, salesOrgID, stateID, customerName, currentPage, pageItemCount);
        }


        

        public DTOCustomerList CustomerListBySalesOrgSearch(long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount)
        {
            CustomerCriteria criteria = new CustomerCriteria()
            {
                CustomerName = customerName,
                SalesOrgID = salesOrgID,
                SYSStateID = stateID,
                CurrentPage = currentPage,
                PageItemCount = pageItemCount,
                SearchType = CustomerSearchType.ByCustomerSalesOrgSearch
            };
            return _customerDA.CustomerListByCriteria(criteria);
        }


        public DTOCustomerList ListCustomerBySalesOrgNotInCustomerSalesRep(string customerName, long SalesOrgID, long AccountID, long SYSStateID, int CurrentPage, int PageItemCount)
        {


            return _customerDA.ListCustomerBySalesOrgNotInCustomerSalesRep(customerName,SalesOrgID, AccountID, SYSStateID, CurrentPage, PageItemCount);
        }

        public DTOCustomerList ListCustomerAllBySalesOrgNotInCustomerSalesRep(string customerName, long SalesOrgID, long AccountID, long SYSStateID)
        {


            return _customerDA.ListCustomerAllBySalesOrgNotInCustomerSalesRep(customerName, SalesOrgID, AccountID, SYSStateID);
        }

        public DTOCustomerSalesRepList ListAllAssignedCustomer(int AccountID) {

            return _customerDA.ListAllAssignedCustomer( AccountID);
        }


        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public Boolean CustomerIsValid(DTOCustomer mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }


            if (string.IsNullOrEmpty(mDTO.CustomerName))
            {
                mValidationResponse = "CustomerName cannnot be null.";
                return false;
            }
            else
            {
                //DTOCustomer customer = CustomerListByCustomerName(mDTO.SalesOrgID, mDTO.CustomerName);

                //if (mDTO.CustomerID == 0 && customer != null)
                //{
                //    mValidationResponse = "Customer Name already exists.";
                //    return false;
                //}
                //if (mDTO.CustomerID > 0 && (customer != null && customer.CustomerID != mDTO.CustomerID))
                //{
                //    mValidationResponse = "Customer Name already exists.";
                //    return false;
                //}
            }

            if (mDTO.SalesRepAccountID == 0)
            {
                mValidationResponse = "SalesRepAccountID cannnot be 0.";
                return false;
            }

            //if (mDTO.AddressID == 0)
            //{
            //    mValidationResponse = "AddressID cannnot be 0.";
            //    return false;
            //}

            if (mDTO.SYSStateID == 0)
            {
                mValidationResponse = "SYSStateID cannnot be 0.";
                return false;
            }


            //if (mDTO.CreatedByUserID == 0)
            //{
            //    mValidationResponse = "CreatedByUserID cannnot be 0.";
            //    return false;
            //}

            //if (mDTO.UpdatedByUserID == 0)
            //{
            //    mValidationResponse = "UpdatedByUserID cannnot be 0.";
            //    return false;
            //}

            mValidationResponse = "Ok";
            return true;
        }

        #endregion


        #region ************************tblCustomerSalesRep CRUDS ******************************************



        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomerSalesRep CustomerSalesRepListByID(int mRecNo)
        {
            return _customerDA.CustomerSalesRepListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomerSalesRep CustomerSalesRepCreateRecord(DTOCustomerSalesRep mDTO)
        {

            return _customerDA.CustomerSalesRepCreateRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomerSalesRep CustomerSalesRepSaveRecord(DTOCustomerSalesRep mDTO)
        {
            try
            {
                if (mDTO.CustomerSalesRepID > 0)
                {
                    return _customerDA.CustomerSalesRepUpdateRecord(mDTO);
                }
                else
                {
                    return _customerDA.CustomerSalesRepCreateRecord(mDTO);
                }
            }
            catch (Exception ex)
            {
                _logService.LogSave("Customer Sales Rep", ex.Message, mDTO.CustomerSalesRepID);
                throw;
            }
           
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomerSalesRep CustomerSalesRepDeleteRecord(DTOCustomerSalesRep mDTO)
        {

            return _customerDA.CustomerSalesRepDeleteRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public Boolean CustomerSalesRepIsValid(DTOCustomerSalesRep mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.CustomerID == 0)
            {
                mValidationResponse = "CustomerID cannnot be 0.";
                return false;
            }

            if (mDTO.SalesRepAccountID == 0)
            {
                mValidationResponse = "SalesRepAccountID cannnot be 0.";
                return false;
            }

            if (mDTO.DateCreated == null)
            {
                mValidationResponse = "DateCreated cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        #endregion


        #region ************************tblLogo CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOLogoList LogoList()
        {

            return _customerDA.LogoList();
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOLogo LogoListByID(int mRecNo)
        {
            return _customerDA.LogoListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOLogo LogoCreateRecord(DTOLogo mDTO)
        {

            return _customerDA.LogoCreateRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOLogo LogoUpdateRecord(DTOLogo mDTO)
        {

            return _customerDA.LogoUpdateRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOLogo LogoDeleteRecord(DTOLogo mDTO)
        {

            return _customerDA.LogoDeleteRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public Boolean LogoIsValid(DTOLogo mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }

            if (mDTO.LogoURL == null)
            {
                mValidationResponse = "LogoURL cannnot be null.";
                return false;
            }

            if (mDTO.LogoDescription == null)
            {
                mValidationResponse = "LogoDescription cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        #endregion


        #region ************************tblOrgUnit CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnitList OrgUnitList()
        {
            return _customerDA.OrgUnitList();
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnit OrgUnitListByID(int mRecNo)
        {
            return _customerDA.OrgUnitListByID(mRecNo);
        }

        public DTOOrgUnitList OrgUnitListBySalesOrgID(long salesOrgID)
        {
            return _customerDA.OrgUnitListBySalesOrgID(salesOrgID);
        }
        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnit OrgUnitCreateRecord(DTOOrgUnit mDTO)
        {

            return _customerDA.OrgUnitCreateRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnit OrgUnitUpdateRecord(DTOOrgUnit mDTO)
        {

            return _customerDA.OrgUnitUpdateRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnit OrgUnitDeleteRecord(DTOOrgUnit mDTO)
        {

            return _customerDA.OrgUnitDeleteRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public Boolean OrgUnitIsValid(DTOOrgUnit mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }

            if (mDTO.OrgUnitName == null)
            {
                mValidationResponse = "OrgUnitName cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }


        #endregion


        //#region ************************tblRegion CRUDS ******************************************

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTORegionList RegionList()
        //{

        //    return _customerDA.RegionList();
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTORegion RegionListByID(int mRecNo)
        //{
        //    return _customerDA.RegionListByID(mRecNo);
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTORegion RegionCreateRecord(DTORegion mDTO)
        //{
        //    return _customerDA.RegionCreateRecord(mDTO);
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTORegion RegionUpdateRecord(DTORegion mDTO)
        //{
        //    return _customerDA.RegionUpdateRecord(mDTO);
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTORegion RegionDeleteRecord(DTORegion mDTO)
        //{
        //    return _customerDA.RegionDeleteRecord(mDTO);
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public Boolean RegionIsValid(DTORegion mDTO, out string mValidationResponse)
        //{
        //    //please add your validation rules here. - Lazy Dog 

        //    if (mDTO.SalesOrgID == 0)
        //    {
        //        mValidationResponse = "SalesOrgID cannnot be 0.";
        //        return false;
        //    }

        //    if (mDTO.RegionName == null)
        //    {
        //        mValidationResponse = "RegionName cannnot be null.";
        //        return false;
        //    }

        //    mValidationResponse = "Ok";
        //    return true;
        //}

        //#endregion


        //#region ************************tblSYSState CRUDS ******************************************

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTOSYSStateList SYSStateList()
        //{
        //    return _customerDA.SYSStateList();
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTOSYSState SYSStateListByID(int mRecNo)
        //{
        //    return _customerDA.SYSStateListByID(mRecNo);
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTOSYSState SYSStateCreateRecord(DTOSYSState mDTO)
        //{
        //    return _customerDA.SYSStateCreateRecord(mDTO);
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTOSYSState SYSStateUpdateRecord(DTOSYSState mDTO)
        //{
        //    return _customerDA.SYSStateUpdateRecord(mDTO);
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public DTOSYSState SYSStateDeleteRecord(DTOSYSState mDTO)
        //{
        //    return _customerDA.SYSStateDeleteRecord(mDTO);
        //}

        ////CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        //public Boolean SYSStateIsValid(DTOSYSState mDTO, out string mValidationResponse)
        //{
        //    //please add your validation rules here. - Lazy Dog 

        //    if (mDTO.StateName == null)
        //    {
        //        mValidationResponse = "StateName cannnot be null.";
        //        return false;
        //    }

        //    if (mDTO.StateCode == null)
        //    {
        //        mValidationResponse = "StateCode cannnot be null.";
        //        return false;
        //    }

        //    mValidationResponse = "Ok";
        //    return true;
        //}

        //#endregion

        #region SalesOrg


        public DTOSalesOrg SalesOrgListBySalesOrgShortName(string salesOrgShortName)
        {
            return _customerDA.SalesOrgListByShortName(salesOrgShortName);
        }

        public DataTable SalesOrgDataTable()
        {
            return SalesOrgDataTable();
        }

        public DTOSalesOrgList SalesOrgList()
        {
            return _customerDA.SalesOrgList();
        }

        public DTOSalesOrg SalesOrgListByID(long mRecNo)
        {
            return _customerDA.SalesOrgListByID(mRecNo);
        }

        public DTOSalesOrg SalesOrgSaveRecord(DTOSalesOrg mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("product");

                if (mDTO.SalesOrgID == 0)
                    return _customerDA.SalesOrgCreateRecord(mDTO);
                else
                    return _customerDA.SalesOrgUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SalesOrgDeleteRecord(long mRecNo)
        {
            try
            {
                _customerDA.SalesOrgDeleteRecord(new DTOSalesOrg() { SalesOrgID = mRecNo });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        [Obsolete("Use AddressService instead.")]
        public DTOAddress AddressSaveRecord(DTOAddress mDTO)
        {
            try
            {
                return _addressService.AddressSaveRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Obsolete("Use AddressService instead.")]
        public DTOAddress AddressListByID(long addressID)
        {
            try
            {
                return _addressService.AddressListByID(addressID);
            }
            catch { throw; }
        }

        public DTOCustomerSalesRepList CustomerSalesRepListSearchSalesRepAndCustomer(long salesRepID, long customerID)
        {
            CustomerCriteria criteria = new CustomerCriteria()
            {
                SearchType = CustomerSearchType.ByCustomerAndSalesRep,
                CustomerID = customerID,
                AccountID = salesRepID,
            };

            return _customerDA.CustomerSalesRepListByCriteria(criteria);
        }

        public DTOCustomerSalesRepList CustomerSalesRepListBySalesRepID(long salesRepID, int CurrentPage, int PageItemCount)
        {
            CustomerCriteria criteria = new CustomerCriteria()
            {
                SearchType = CustomerSearchType.BySalesRepID,
                AccountID = salesRepID,
                CurrentPage = CurrentPage,
                PageItemCount = PageItemCount
            };

            return _customerDA.CustomerSalesRepListByCriteria(criteria);
        }

        public DTOLogo LogoListBySalesOrgID(long salesOrgID)
        {
            return _customerDA.LogoListBySalesOrgID(salesOrgID);
        }


    } //end class

}
