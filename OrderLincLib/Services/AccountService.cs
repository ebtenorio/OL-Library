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
    public class AccountService : IAccountService
    {
        IAccountDA _accountDA;
        private ILogService _logService;
        IContactService _contactService;
        IAddressService _addressService;
        IConfigurationService _configurationService;
        ICustomerService _customerService;

        public AccountService(IAccountDA accountDA, IContactService contactService, IAddressService addressService, IConfigurationService configurationService, ICustomerService customerService, ILogService logService)
        {
            _logService = logService;
            _accountDA = accountDA;
            _contactService = contactService;
            _addressService = addressService;
            _configurationService = configurationService;
            _customerService = customerService;
        }


        #region ************************tblAccount CRUDS ******************************************



        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccount AccountListByID(long mRecNo)
        {
            AccountCriteria criteria = new AccountCriteria()
            {
                AccountID = mRecNo,
                SearchType = AccountSearchType.ByAccountID
            };
            return _accountDA.AccountByCriteria(criteria);
        }

        public DTOAccount AccountListByUsername(long salesOrgID, string username)
        {
            AccountCriteria criteria = new AccountCriteria()
            {
                SalesOrgID = salesOrgID,
                Username = username,
                SearchType = AccountSearchType.ByUserName
            };
            return _accountDA.AccountByCriteria(criteria);
        }

        public DTOAccount AccountAuthenticate(string userName, string password)
        {
            try
            {
                AccountCriteria criteria = new AccountCriteria()
                {
                    AccountID = 0,
                    Username = userName,
                    Password = password,
                    SearchType = AccountSearchType.ByUserNameAndPassword
                };
                return _accountDA.AccountByCriteria(criteria);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Account", "AccountAuthenticate : " + ex.Message, 0);
                throw;
            }

        }

        public DTOAccount AccountListByUsername(long salesOrgID, string userName, string password)
        {
            try
            {
                AccountCriteria criteria = new AccountCriteria()
                {
                    AccountID = 0,
                    Username = userName,
                    Password = password,
                    SalesOrgID = salesOrgID,

                    SearchType = AccountSearchType.BySalesOrgUsernamePassword,
                };
                return _accountDA.AccountByCriteria(criteria);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Account", "AccountAuthenticate : " + ex.Message, 0);
                throw;
            }

        }

        public bool IsAccountExistingAcrossAllOtherSalesOrgs(long salesOrgID, string userName, string password)
        {
 
            bool _isExisting = false;

            try
            {
                AccountCriteria criteria = new AccountCriteria()
                {
                    SalesOrgID = salesOrgID,
                    Username = userName,
                    Password = password,
                    SearchType = AccountSearchType.ByOtherSalesOrgsUsernamePassword,
                };
                return (_accountDA.AccountByCriteria(criteria) == null);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Account", "IsAccountExistingAcrossAllOtherSalesOrgs : " + ex.Message, 0);
                throw;
            }


            return _isExisting;
        }

        public DTOMobileAccount AccountAuthenticate(string userName, string password, string deviceNo)
        {
            try
            {

                AccountCriteria criteria = new AccountCriteria()
                {
                    AccountID = 0,
                    Username = userName,
                    Password = password,
                    DeviceNo = deviceNo,
                    SearchType = AccountSearchType.ByUserNameAndPasswordWithDeviceNo
                };
                DTOAccount account = _accountDA.AccountByCriteria(criteria);
                DTOMobileAccount mobileAccount = new DTOMobileAccount();

                if (account == null) return null;
                DTOServer server = _configurationService.ServerListByID(account.ServerID);

                if (server != null)
                    mobileAccount.ServerUrl = server.ServerURL;

                DTOLogo logo = _customerService.LogoListBySalesOrgID(account.RefID);
                if (logo != null)
                    mobileAccount.Logo = logo.LogoURL;

                mobileAccount.AccountID = account.AccountID;
                mobileAccount.AccountTypeID = account.AccountTypeID;
                mobileAccount.AccountTypeText = account.AccountTypeText;
                mobileAccount.AddressID = account.AddressID;
                mobileAccount.ContactID = account.ContactID;
                mobileAccount.DateActivated = account.DateActivated;
                mobileAccount.DateCreated = account.DateCreated;
                mobileAccount.DateLockout = account.DateLockout;
                mobileAccount.DateUpdated = account.DateUpdated;
                mobileAccount.DeviceNo = account.DeviceNo;
                mobileAccount.Email = account.Email;
                mobileAccount.EndDate = account.EndDate;
                mobileAccount.ExpiryDate = account.ExpiryDate;
                mobileAccount.FirstName = account.FirstName;
                mobileAccount.LastIpAddress = account.LastIpAddress;
                mobileAccount.LastLoginDate = account.LastLoginDate;
                mobileAccount.LastName = account.LastName;
                mobileAccount.Lockout = account.Lockout;

                mobileAccount.OrgUnitID = account.OrgUnitID;
                mobileAccount.RefID = account.RefID;
                mobileAccount.RoleID = account.RoleID;

                mobileAccount.StartDate = account.StartDate;
                mobileAccount.Username = account.Username;

                return mobileAccount;
            }
            catch (Exception ex)
            {
                _logService.LogSave("Account", string.Format("AccountAuthenticate With Device: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }


        }

        public DTOAccountList AccountListByAccountTypeID(int accountTypeID)
        {
            return _accountDA.AccountListByAccountTypeID(accountTypeID);
        }


        public DTOAccountList AccountListByStateIDAndSalesOrgID(long StateID, long SalesOrgID)
        {
            return _accountDA.AccountListByStateIDAndSalesOrgID(StateID, SalesOrgID);
        }


        public DataTable AccountSalesRepDataTable()
        {
            return _accountDA.AccountDataTableByAccountTypeID((int)AccountType.SalesRep);
        }


        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccount AccountSaveRecord(DTOAccount mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("product");

                if (mDTO.AccountID == 0)
                    return _accountDA.AccountCreateRecord(mDTO);
                else
                    return _accountDA.AccountUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Account", ex.Message, mDTO.UpdatedByUserID);
                throw;
            }
        }

        public void AccountSaveRecord(DTOAccount mDTO, DTOContact contact, DTOAddress address)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _contactService.ContactSaveRecord(contact);
                    _addressService.AddressSaveRecord(address);
                    mDTO.AddressID = address.AddressID;
                    mDTO.ContactID = contact.ContactID;
                    AccountSaveRecord(mDTO);
                    scope.Complete();
                }
            }
            catch(Exception ex)
            {
                _logService.LogSave("Account", string.Format("AccountSaveRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), mDTO.UpdatedByUserID);
                throw;
            }
        }
        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public void AccountDeleteRecord(long mRecNo)
        {
            try
            {
                _accountDA.AccountDeleteRecord(new DTOAccount() { AccountID = mRecNo });
            }
            catch (Exception ex)
            {
                _logService.LogSave("Account", string.Format("AccountDeleteRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public Boolean AccountIsValid(DTOAccount mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.RefID == 0)
            {
                mValidationResponse = "RefID cannnot be 0.";
                return false;
            }

            if (mDTO.AccountTypeID == 0)
            {
                mValidationResponse = "AccountTypeID cannnot be 0.";
                return false;
            }

            if (mDTO.OrgUnitID == 0)
            {
                mValidationResponse = "OrgUnitID cannnot be 0.";
                return false;
            }

            if (string.IsNullOrEmpty(mDTO.Username))
            {
                mValidationResponse = "Username cannnot be null.";
                return false;
            }
            else
            {
                if (mDTO.AccountID == 0) // new account should be unique in all sales org
                {
                    DTOAccount account = AccountListByUsername(mDTO.RefID, mDTO.Username);
                    if (account != null)
                    {
                        mValidationResponse = "Username already exists.";
                        return false;
                    }
                }
                else
                {
                    DTOAccount account = AccountListByUsername(mDTO.RefID, mDTO.Username); // RefID is required, should be validated above.

                    if (account != null && account.AccountID != mDTO.AccountID)
                    {
                        mValidationResponse = "Username already exists.";
                        return false;
                    }
                }
            }

            if (string.IsNullOrEmpty(mDTO.Password))
            {
                mValidationResponse = "Password cannnot be null.";
                return false;
            }

            //if (mDTO.DeviceNo == null)
            //{
            //    mValidationResponse = "DeviceNo cannnot be null.";
            //    return false;
            //}

            //if (mDTO.RoleID == 0)
            //{
            //    mValidationResponse = "RoleID cannnot be 0.";
            //    return false;
            //}

            //if (mDTO.AddressID == 0)
            //{
            //    mValidationResponse = "AddressID cannnot be 0.";
            //    return false;
            //}

            //if (mDTO.ContactID == 0)
            //{
            //    mValidationResponse = "ContactID cannnot be 0.";
            //    return false;
            //}

            //if (mDTO.LastIpAddress == null)
            //{
            //    mValidationResponse = "LastIpAddress cannnot be null.";
            //    return false;
            //}

            //if (mDTO.DateLockout == null)
            //{
            //    mValidationResponse = "DateLockout cannnot be null.";
            //    return false;
            //}

            //if (mDTO.LastLoginDate == null)
            //{
            //    mValidationResponse = "LastLoginDate cannnot be null.";
            //    return false;
            //}

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

            //if (mDTO.ExpiryDate == null)
            //{
            //    mValidationResponse = "ExpiryDate cannnot be null.";
            //    return false;
            //}

            //if (mDTO.DateActivated == null)
            //{
            //    mValidationResponse = "DateActivated cannnot be null.";
            //    return false;
            //}

            //if (mDTO.ServerID == 0)
            //{
            //    mValidationResponse = "ServerID cannnot be 0.";
            //    return false;
            //}

            //if (mDTO.StartDate == null)
            //{
            //    mValidationResponse = "StartDate cannnot be null.";
            //    return false;
            //}

            //if (mDTO.EndDate == null)
            //{
            //    mValidationResponse = "EndDate cannnot be null.";
            //    return false;
            //}

            mValidationResponse = "Ok";
            return true;
        }

        //AccountListByOrgUnitIDAndTypePagedConcat(int mRecNo, int CurrentPage, int PageItemCount, string SearchText, int mAccountTypeID)
        public DTOAccountList AccountListBySearchAccountType(int recNo, AccountType accountTypeID, int currentPage, int pageItemCount, string searchText)
        {
            AccountCriteria criteria = new AccountCriteria()
            {
                SearchType = AccountSearchType.ByOrgUnitWithAccountTypeIDSearch,
                OrgUnitID = recNo,
                AccountTypeID = accountTypeID,
                CurrentPage = currentPage,
                PageItemCount = pageItemCount,
                SearchText = searchText,
            };

            return _accountDA.AccountListByCriteria(criteria);
        }

        //AccountListByOrgUnitIDPagedConcat(int mRecNo, int CurrentPage, int PageItemCount, string SearchText, int RefID)
        public DTOAccountList AccountListBySearch(int orgUnitID, long refID, int currentPage, int pageItemCount, string searchText)
        {
            AccountCriteria criteria = new AccountCriteria()
            {
                SearchType = AccountSearchType.ByOrgUnitSearch,
                OrgUnitID = orgUnitID,
                SalesOrgID = refID,
                CurrentPage = currentPage,
                PageItemCount = pageItemCount,
                SearchText = searchText,
            };

            return _accountDA.AccountListByCriteria(criteria);
        }


        #endregion

        #region ************************tblAccountType CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountTypeList AccountTypeList()
        {
            return _accountDA.AccountTypeList();
        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountType AccountTypeListByID(int mRecNo)
        {
            return _accountDA.AccountTypeListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountType AccountTypeSaveRecord(DTOAccountType mDTO)
        {
           
            try
            {
                if (mDTO.AccountTypeID == 0)
                    return _accountDA.AccountTypeCreateRecord(mDTO);
                else
                    return _accountDA.AccountTypeUpdateRecord(mDTO);

            }
            catch (Exception ex)
            {
                _logService.LogSave("Account Type", string.Format("AccountTypeSaveRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountType AccountTypeDeleteRecord(DTOAccountType mDTO)
        {
            return _accountDA.AccountTypeDeleteRecord(mDTO);
        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public Boolean AccountTypeIsValid(DTOAccountType mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.AccountTypeCode == null)
            {
                mValidationResponse = "AccountTypeCode cannnot be null.";
                return false;
            }

            if (mDTO.AccountTypeText == null)
            {
                mValidationResponse = "AccountTypeText cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        #endregion

        [Obsolete("Use AddressService instead.")]
        public DTOContact ContactSaveRecord(DTOContact mDTO)
        {
            try
            {
                return _contactService.ContactSaveRecord(mDTO);
            }
            catch (Exception ex)
            {
                _logService.LogSave("Contact", string.Format("ContactSaveRecord: {0}\r\n{1}", ex.Message, ex.StackTrace), 0);
                throw;
            }
        }

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

        public DTOContact ContactListByAccountID(long accountID)
        {
            return _contactService.ContactListByAccountID(accountID);
        }

        public DTOAddress AddressListByID(long addressID)
        {
            try
            {
                return _addressService.AddressListByID(addressID);
            }
            catch { throw; }
        }

        public DTOContact ContactListByID(long contactID)
        {
            return _contactService.ContactListByID(contactID);
        }


        //public DTOAccountTypeList AccountTypeListByNotAccountTypeID(int mRecNo)
        //{
        //    return _accountDA.AccountTypeListByNotEqualAccountTypeID(mRecNo);
        //}

        public Int16 CheckNewAccountDetails(string pUsername, string pPassword, string pSalesOrgID, string AccountID)
        {
            return _accountDA.CheckNewAccountDetails(pUsername, pPassword, pSalesOrgID, AccountID);
        }




    } //end class
}

