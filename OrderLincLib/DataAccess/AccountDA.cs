using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;

namespace OrderLinc.DataAccess
{
    public class AccountDA : IAccountDA
    {

        DatabaseService mDBService;

        public AccountDA(DatabaseService dbService)
        {

            mDBService = dbService;
        }


        #region ************************tblAccount CRUDS ******************************************

        private DTOAccount DataRowToDTOAccount(DataRow mRow)
        {
            DTOAccount mDTOAccount = new DTOAccount();

            mDTOAccount.AccountID = Int64.Parse(mRow["AccountID"].ToString());
            mDTOAccount.RefID = Int64.Parse(mRow["RefID"].ToString());
            mDTOAccount.AccountTypeID = int.Parse(mRow["AccountTypeID"].ToString());
            mDTOAccount.AccountTypeText = mRow["AccountTypeText"].ToString();
            mDTOAccount.OrgUnitID = int.Parse(mRow["OrgUnitID"].ToString());
            mDTOAccount.Username = mRow["Username"].ToString();
            mDTOAccount.Password = mRow["Password"].ToString();
            mDTOAccount.DeviceNo = mRow["DeviceNo"].ToString();
            mDTOAccount.RoleID = int.Parse(mRow["RoleID"].ToString());
            mDTOAccount.AddressID = Int64.Parse(mRow["AddressID"].ToString());
            mDTOAccount.ContactID = Int64.Parse(mRow["ContactID"].ToString());
            mDTOAccount.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
            mDTOAccount.InActive = Boolean.Parse(mRow["InActive"].ToString());
            mDTOAccount.Lockout = Boolean.Parse(mRow["Lockout"].ToString());
            mDTOAccount.LastIpAddress = mRow["LastIpAddress"].ToString();

            if(mRow["OrderLincVersion"] != null) 
                mDTOAccount.OrderLincVersion = mRow["OrderLincVersion"].ToString();

            if (mRow["iOSVersion"] != null)
                mDTOAccount.iOSVersion = mRow["iOSVersion"].ToString();

            if (mRow.IsExists("LastName"))
                mDTOAccount.LastName = mRow["LastName"].ToString();

            if (mRow.IsExists("FirstName"))
                mDTOAccount.FirstName = mRow["FirstName"].ToString();

            if (mRow.IsExists("Email"))
                mDTOAccount.Email = mRow["Email"].ToString();

            if (mRow["DateLockout"] != System.DBNull.Value)
            {
                mDTOAccount.DateLockout = DateTime.Parse(mRow["DateLockout"].ToString());
            }
            if (mRow["LastLoginDate"] != System.DBNull.Value)
            {
                mDTOAccount.LastLoginDate = DateTime.Parse(mRow["LastLoginDate"].ToString());
            }
            if (mRow["DateCreated"] != System.DBNull.Value)
            {
                mDTOAccount.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
            }
            if (mRow["DateUpdated"] != System.DBNull.Value)
            {
                mDTOAccount.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
            }

            mDTOAccount.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());

            mDTOAccount.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());

            if (mRow["ExpiryDate"] != System.DBNull.Value)
            {
                mDTOAccount.ExpiryDate = DateTime.Parse(mRow["ExpiryDate"].ToString());
            }
            if (mRow["DateActivated"] != System.DBNull.Value)
            {
                mDTOAccount.DateActivated = DateTime.Parse(mRow["DateActivated"].ToString());
            }
            mDTOAccount.ServerID = Int64.Parse(mRow["ServerID"].ToString());

            if (mRow["StartDate"] != System.DBNull.Value)
            {
                mDTOAccount.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
            }
            if (mRow["EndDate"] != System.DBNull.Value)
            {
                mDTOAccount.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
            }
            if (mRow.IsExists("OldID"))
            if (mRow["OldID"] != System.DBNull.Value) { 
            
                mDTOAccount.OldID = Int64.Parse(mRow["OldID"].ToString());            
            }

            return mDTOAccount;
        }

        public DataTable AccountDataTableByAccountTypeID(int accountTypeID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountTypeID";
            mParam.ParameterValue = accountTypeID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccount_ListByAccountTypeID", mParams);

            return mDT;
        }

        //public DTOAccountList AccountList()
        //{
        //    DTOAccountList mDTOAccountList = new DTOAccountList();
        //    DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccount_List");
        //    foreach (DataRow mRow in mDT.Rows)
        //    {
        //        mDTOAccountList.Add(DataRowToDTOAccount(mRow));
        //    }

        //    return mDTOAccountList;
        //}

        public DTOAccount AccountByCriteria(AccountCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = string.Empty;

            if (criteria.SearchType == AccountSearchType.ByAccountID)
            {
                sp = "usp_tblAccount_ListByID";
                mParams.Add(new DTODBParameter("@AccountID", criteria.AccountID));
            }
            //else if (criteria.SearchType == AccountSearchType.ByUserName)
            //{
            //    sp = "usp_tblAccount_ListByUserName";
            //    mParams.Add(new DTODBParameter("@UserName", criteria.Username));
            //} Remove by RR 5-20-14 causes Error
            else if (criteria.SearchType == AccountSearchType.ByUserNameAndPassword)
            {
                sp = "usp_tblAccount_Authenticate";
                mParams.Add(new DTODBParameter("@UserName", criteria.Username));
                mParams.Add(new DTODBParameter("@Password", criteria.Password));
            }
            else if (criteria.SearchType == AccountSearchType.ByUserNameAndPasswordWithDeviceNo)
            {
                sp = "usp_tblAccount_AuthenticateUserDevice";
                mParams.Add(new DTODBParameter("@UserName", criteria.Username));
                mParams.Add(new DTODBParameter("@Password", criteria.Password));
                mParams.Add(new DTODBParameter("@DeviceNo", criteria.DeviceNo));
            }
            else if (criteria.SearchType == AccountSearchType.ByUserName)
            {
                sp = "usp_tblAccount_ListByUserName";
                mParams.Add(new DTODBParameter("@UserName", criteria.Username));
                mParams.Add(new DTODBParameter("@Password", string.Empty));
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
            }
            else if (criteria.SearchType == AccountSearchType.BySalesOrgUsernamePassword)
            {
                sp = "usp_tblAccount_ListByUserName";
                mParams.Add(new DTODBParameter("@UserName", criteria.Username));
                mParams.Add(new DTODBParameter("@Password", criteria.Password));
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
            }
            else if (criteria.SearchType == AccountSearchType.ByOtherSalesOrgsUsernamePassword)
            {
                //sp = "usp_tblAccount_ListFromOtherSalesOrg";
                //mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
                //mParams.Add(new DTODBParameter("@UserName", criteria.Username));
                //mParams.Add(new DTODBParameter("@Password", criteria.Password));


                //sp = "usp_tblAccount_SelectUniqueUserPassword";
                //mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
                //mParams.Add(new DTODBParameter("@UserName", criteria.Username));
                //mParams.Add(new DTODBParameter("@Password", criteria.Password));
                //mParams.Add(new DTODBParameter("@AccountID", criteria.AccountID));

            }

            //else if (criteria.SearchType == AccountSearchType.ByBusinessNumber)
            //{
            //    sp = "usp_tblAccount_ListByBusinessNumberUserName";
            //    mParams.Add(new DTODBParameter("@UserName", criteria.Username));
            //    mParams.Add(new DTODBParameter("@BusinessNumber", criteria.BusinessNumber));
            //}

            if (sp == string.Empty) throw new ArgumentException("No criteria specified.");

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);
            DTOAccount mDTOAccount = null;
            if (mDT.Rows.Count > 0)
                mDTOAccount = DataRowToDTOAccount(mDT.Rows[0]);

            return mDTOAccount;
        }

        public DTOAccount AccountCreateRecord(DTOAccount mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblAccount");
            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();

            mRow["RefID"] = mDTO.RefID;
            mRow["AccountTypeID"] = mDTO.AccountTypeID;
            mRow["OrgUnitID"] = mDTO.OrgUnitID;
            mRow["Username"] = mDTO.Username;
            mRow["Password"] = mDTO.Password;
            mRow["DeviceNo"] = mDTO.DeviceNo;
            mRow["RoleID"] = mDTO.RoleID;
            mRow["AddressID"] = mDTO.AddressID;
            mRow["ContactID"] = mDTO.ContactID;
            mRow["Deleted"] = mDTO.Deleted;
            mRow["InActive"] = mDTO.InActive;
            mRow["Lockout"] = mDTO.Lockout;
            mRow["LastIpAddress"] = mDTO.LastIpAddress;
            mRow["DateLockout"] = mDTO.DateLockout.ToDataRowValue();
            mRow["LastLoginDate"] = mDTO.LastLoginDate;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
            mRow["ExpiryDate"] = mDTO.ExpiryDate.ToDataRowValue();
            mRow["DateActivated"] = mDTO.DateActivated.ToDataRowValue(); 
            mRow["ServerID"] = mDTO.ServerID;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;

            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblAccount_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.AccountID = ObjectID;
            return mDTO;
        }

        public DTOAccount AccountUpdateRecord(DTOAccount mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblAccount");

            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();
            mRow["AccountID"] = mDTO.AccountID;
            mRow["RefID"] = mDTO.RefID;
            mRow["AccountTypeID"] = mDTO.AccountTypeID;
            mRow["OrgUnitID"] = mDTO.OrgUnitID;
            mRow["Username"] = mDTO.Username;
            mRow["Password"] = mDTO.Password;
            mRow["DeviceNo"] = mDTO.DeviceNo;
            mRow["RoleID"] = mDTO.RoleID;
            mRow["AddressID"] = mDTO.AddressID;
            mRow["ContactID"] = mDTO.ContactID;
            mRow["Deleted"] = mDTO.Deleted;
            mRow["InActive"] = mDTO.InActive;
            mRow["Lockout"] = mDTO.Lockout;
            mRow["LastIpAddress"] = mDTO.LastIpAddress;
            mRow["DateLockout"] = mDTO.DateLockout.ToDataRowValue();
            mRow["LastLoginDate"] = mDTO.LastLoginDate;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
            mRow["ExpiryDate"] = mDTO.ExpiryDate.ToDataRowValue();
            mRow["DateActivated"] = mDTO.DateActivated.ToDataRowValue(); 
            mRow["ServerID"] = mDTO.ServerID;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mRow["OrderLincVersion"] = mDTO.OrderLincVersion;
            mRow["iOSVersion"] = mDTO.iOSVersion;
           
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblAccount_UPDATE");
            return mDTO;


        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccount AccountDeleteRecord(DTOAccount mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = mDTO.AccountID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblAccount_DELETE", mParams);
            return mDTO;
        }

        public DTOAccountList AccountListByCriteria(AccountCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();
         
            string sp = string.Empty;
            //AccountListByOrgUnitIDPagedConcat
            if (criteria.SearchType == AccountSearchType.ByOrgUnitWithAccountTypeIDSearch)
            {
                mParams.Add(new DTODBParameter("@OrgUnitID", criteria.OrgUnitID));
                mParams.Add(new DTODBParameter("@CurrentPage", criteria.CurrentPage));
                mParams.Add(new DTODBParameter("@PageItemCount", criteria.PageItemCount));
                mParams.Add(new DTODBParameter("@SearchText", criteria.SearchText));
                mParams.Add(new DTODBParameter("@AccountTypeID", (int)criteria.AccountTypeID));

                sp = "usp_tblAccount_ListByOrgUnitSearch";
            }
            else if (criteria.SearchType == AccountSearchType.ByOrgUnitSearch)
            {

                mParams.Add(new DTODBParameter("@RefID", criteria.SalesOrgID ));
                mParams.Add(new DTODBParameter("@OrgUnitID", criteria.OrgUnitID));
                mParams.Add(new DTODBParameter("@CurrentPage", criteria.CurrentPage));
                mParams.Add(new DTODBParameter("@PageItemCount", criteria.PageItemCount));
                mParams.Add(new DTODBParameter("@SearchText", criteria.SearchText));

                sp = "usp_tblAccount_ListPaged_Concat";
            }

            if (sp == null) throw new ArgumentException("No sp specified.");

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);
            DTOAccountList mDTOAccountList = new DTOAccountList();

            if (mDT.Rows.Count > 0)
            {
                if (mDT.Columns.Contains("TotalRecords"))
                    mDTOAccountList.TotalRecords = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
            }
            foreach (DataRow mRow in mDT.Rows)
            {
                mDTOAccountList.Add(DataRowToDTOAccount(mRow));
            }

            return mDTOAccountList;

        }

        ////CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        //public DTOAccountList AccountListByRefID(int mRecNo)
        //{
        //    DTODBParameters mParams = new DTODBParameters();
        //    DTODBParameter mParam = new DTODBParameter();
        //    mParam.ParameterName = "@RefID";
        //    mParam.ParameterValue = mRecNo;
        //    mParams.Add(mParam);
        //    DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccount_ListByRefID", mParams);
        //    DTOAccountList mDTOAccountList = new DTOAccountList();
        //    foreach (DataRow mRow in mDT.Rows)
        //    {
        //        DTOAccount mDTOAccount = new DTOAccount();
        //        mDTOAccount.AccountID = Int64.Parse(mRow["AccountID"].ToString());
        //        mDTOAccount.RefID = Int64.Parse(mRow["RefID"].ToString());
        //        mDTOAccount.AccountTypeID = int.Parse(mRow["AccountTypeID"].ToString());
        //        mDTOAccount.OrgUnitID = int.Parse(mRow["OrgUnitID"].ToString());
        //        mDTOAccount.Username = mRow["Username"].ToString();
        //        mDTOAccount.Password = mRow["Password"].ToString();
        //        mDTOAccount.DeviceNo = mRow["DeviceNo"].ToString();
        //        mDTOAccount.RoleID = int.Parse(mRow["RoleID"].ToString());
        //        mDTOAccount.AddressID = Int64.Parse(mRow["AddressID"].ToString());
        //        mDTOAccount.ContactID = Int64.Parse(mRow["ContactID"].ToString());
        //        mDTOAccount.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
        //        mDTOAccount.InActive = Boolean.Parse(mRow["InActive"].ToString());
        //        mDTOAccount.Lockout = Boolean.Parse(mRow["Lockout"].ToString());
        //        mDTOAccount.LastIpAddress = mRow["LastIpAddress"].ToString();
        //        if (mRow["DateLockout"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateLockout = DateTime.Parse(mRow["DateLockout"].ToString());
        //        }
        //        if (mRow["LastLoginDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.LastLoginDate = DateTime.Parse(mRow["LastLoginDate"].ToString());
        //        }
        //        if (mRow["DateCreated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
        //        }
        //        if (mRow["DateUpdated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
        //        }
        //        mDTOAccount.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
        //        mDTOAccount.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
        //        if (mRow["ExpiryDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.ExpiryDate = DateTime.Parse(mRow["ExpiryDate"].ToString());
        //        }
        //        if (mRow["DateActivated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateActivated = DateTime.Parse(mRow["DateActivated"].ToString());
        //        }
        //        mDTOAccount.ServerID = Int64.Parse(mRow["ServerID"].ToString());
        //        if (mRow["StartDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
        //        }
        //        if (mRow["EndDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
        //        }
        //        mDTOAccountList.Add(mDTOAccount);
        //    }

        //    return mDTOAccountList;
        //}

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountList AccountListByAccountTypeID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountTypeID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccount_ListByAccountTypeID", mParams);
            DTOAccountList mDTOAccountList = new DTOAccountList();
            foreach (DataRow mRow in mDT.Rows)
            {
                mDTOAccountList.Add(DataRowToDTOAccount(mRow));
            }

            return mDTOAccountList;
        }


        public DTOAccountList AccountListByStateIDAndSalesOrgID(long StateID, long SalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@StateID";
            mParam.ParameterValue = StateID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = SalesOrgID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccount_ListByStateID", mParams);
            DTOAccountList mDTOAccountList = new DTOAccountList();
            foreach (DataRow mRow in mDT.Rows)
            {
                mDTOAccountList.Add(DataRowToDTOAccount(mRow));
            }

            return mDTOAccountList;
        }
        ////CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        //public DTOAccountList AccountListByOrgUnitID(int mRecNo)
        //{
        //    DTODBParameters mParams = new DTODBParameters();
        //    DTODBParameter mParam = new DTODBParameter();
        //    mParam.ParameterName = "@OrgUnitID";
        //    mParam.ParameterValue = mRecNo;
        //    mParams.Add(mParam);
        //    DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccount_ListByOrgUnitID", mParams);
        //    DTOAccountList mDTOAccountList = new DTOAccountList();
        //    foreach (DataRow mRow in mDT.Rows)
        //    {
        //        DTOAccount mDTOAccount = new DTOAccount();
        //        mDTOAccount.AccountID = Int64.Parse(mRow["AccountID"].ToString());
        //        mDTOAccount.RefID = Int64.Parse(mRow["RefID"].ToString());
        //        mDTOAccount.AccountTypeID = int.Parse(mRow["AccountTypeID"].ToString());
        //        mDTOAccount.OrgUnitID = int.Parse(mRow["OrgUnitID"].ToString());
        //        mDTOAccount.Username = mRow["Username"].ToString();
        //        mDTOAccount.Password = mRow["Password"].ToString();
        //        mDTOAccount.DeviceNo = mRow["DeviceNo"].ToString();
        //        mDTOAccount.RoleID = int.Parse(mRow["RoleID"].ToString());
        //        mDTOAccount.AddressID = Int64.Parse(mRow["AddressID"].ToString());
        //        mDTOAccount.ContactID = Int64.Parse(mRow["ContactID"].ToString());
        //        mDTOAccount.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
        //        mDTOAccount.InActive = Boolean.Parse(mRow["InActive"].ToString());
        //        mDTOAccount.Lockout = Boolean.Parse(mRow["Lockout"].ToString());
        //        mDTOAccount.LastIpAddress = mRow["LastIpAddress"].ToString();
        //        if (mRow["DateLockout"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateLockout = DateTime.Parse(mRow["DateLockout"].ToString());
        //        }
        //        if (mRow["LastLoginDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.LastLoginDate = DateTime.Parse(mRow["LastLoginDate"].ToString());
        //        }
        //        if (mRow["DateCreated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
        //        }
        //        if (mRow["DateUpdated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
        //        }
        //        mDTOAccount.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
        //        mDTOAccount.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
        //        if (mRow["ExpiryDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.ExpiryDate = DateTime.Parse(mRow["ExpiryDate"].ToString());
        //        }
        //        if (mRow["DateActivated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateActivated = DateTime.Parse(mRow["DateActivated"].ToString());
        //        }
        //        mDTOAccount.ServerID = Int64.Parse(mRow["ServerID"].ToString());
        //        if (mRow["StartDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
        //        }
        //        if (mRow["EndDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
        //        }
        //        mDTOAccountList.Add(mDTOAccount);
        //    }

        //    return mDTOAccountList;
        //}

        ////CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        //public DTOAccountList AccountListByRoleID(int mRecNo)
        //{
        //    DTODBParameters mParams = new DTODBParameters();
        //    DTODBParameter mParam = new DTODBParameter();
        //    mParam.ParameterName = "@RoleID";
        //    mParam.ParameterValue = mRecNo;
        //    mParams.Add(mParam);
        //    DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccount_ListByRoleID", mParams);
        //    DTOAccountList mDTOAccountList = new DTOAccountList();
        //    foreach (DataRow mRow in mDT.Rows)
        //    {
        //        DTOAccount mDTOAccount = new DTOAccount();
        //        mDTOAccount.AccountID = Int64.Parse(mRow["AccountID"].ToString());
        //        mDTOAccount.RefID = Int64.Parse(mRow["RefID"].ToString());
        //        mDTOAccount.AccountTypeID = int.Parse(mRow["AccountTypeID"].ToString());
        //        mDTOAccount.OrgUnitID = int.Parse(mRow["OrgUnitID"].ToString());
        //        mDTOAccount.Username = mRow["Username"].ToString();
        //        mDTOAccount.Password = mRow["Password"].ToString();
        //        mDTOAccount.DeviceNo = mRow["DeviceNo"].ToString();
        //        mDTOAccount.RoleID = int.Parse(mRow["RoleID"].ToString());
        //        mDTOAccount.AddressID = Int64.Parse(mRow["AddressID"].ToString());
        //        mDTOAccount.ContactID = Int64.Parse(mRow["ContactID"].ToString());
        //        mDTOAccount.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
        //        mDTOAccount.InActive = Boolean.Parse(mRow["InActive"].ToString());
        //        mDTOAccount.Lockout = Boolean.Parse(mRow["Lockout"].ToString());
        //        mDTOAccount.LastIpAddress = mRow["LastIpAddress"].ToString();
        //        if (mRow["DateLockout"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateLockout = DateTime.Parse(mRow["DateLockout"].ToString());
        //        }
        //        if (mRow["LastLoginDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.LastLoginDate = DateTime.Parse(mRow["LastLoginDate"].ToString());
        //        }
        //        if (mRow["DateCreated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
        //        }
        //        if (mRow["DateUpdated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
        //        }
        //        mDTOAccount.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
        //        mDTOAccount.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
        //        if (mRow["ExpiryDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.ExpiryDate = DateTime.Parse(mRow["ExpiryDate"].ToString());
        //        }
        //        if (mRow["DateActivated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateActivated = DateTime.Parse(mRow["DateActivated"].ToString());
        //        }
        //        mDTOAccount.ServerID = Int64.Parse(mRow["ServerID"].ToString());
        //        if (mRow["StartDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
        //        }
        //        if (mRow["EndDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
        //        }
        //        mDTOAccountList.Add(mDTOAccount);
        //    }

        //    return mDTOAccountList;
        //}

        ////CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        //public DTOAccountList AccountListByServerID(int mRecNo)
        //{
        //    DTODBParameters mParams = new DTODBParameters();
        //    DTODBParameter mParam = new DTODBParameter();
        //    mParam.ParameterName = "@ServerID";
        //    mParam.ParameterValue = mRecNo;
        //    mParams.Add(mParam);
        //    DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccount_ListByServerID", mParams);
        //    DTOAccountList mDTOAccountList = new DTOAccountList();
        //    foreach (DataRow mRow in mDT.Rows)
        //    {
        //        DTOAccount mDTOAccount = new DTOAccount();
        //        mDTOAccount.AccountID = Int64.Parse(mRow["AccountID"].ToString());
        //        mDTOAccount.RefID = Int64.Parse(mRow["RefID"].ToString());
        //        mDTOAccount.AccountTypeID = int.Parse(mRow["AccountTypeID"].ToString());
        //        mDTOAccount.OrgUnitID = int.Parse(mRow["OrgUnitID"].ToString());
        //        mDTOAccount.Username = mRow["Username"].ToString();
        //        mDTOAccount.Password = mRow["Password"].ToString();
        //        mDTOAccount.DeviceNo = mRow["DeviceNo"].ToString();
        //        mDTOAccount.RoleID = int.Parse(mRow["RoleID"].ToString());
        //        mDTOAccount.AddressID = Int64.Parse(mRow["AddressID"].ToString());
        //        mDTOAccount.ContactID = Int64.Parse(mRow["ContactID"].ToString());
        //        mDTOAccount.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
        //        mDTOAccount.InActive = Boolean.Parse(mRow["InActive"].ToString());
        //        mDTOAccount.Lockout = Boolean.Parse(mRow["Lockout"].ToString());
        //        mDTOAccount.LastIpAddress = mRow["LastIpAddress"].ToString();
        //        if (mRow["DateLockout"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateLockout = DateTime.Parse(mRow["DateLockout"].ToString());
        //        }
        //        if (mRow["LastLoginDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.LastLoginDate = DateTime.Parse(mRow["LastLoginDate"].ToString());
        //        }
        //        if (mRow["DateCreated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
        //        }
        //        if (mRow["DateUpdated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
        //        }
        //        mDTOAccount.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
        //        mDTOAccount.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
        //        if (mRow["ExpiryDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.ExpiryDate = DateTime.Parse(mRow["ExpiryDate"].ToString());
        //        }
        //        if (mRow["DateActivated"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.DateActivated = DateTime.Parse(mRow["DateActivated"].ToString());
        //        }
        //        mDTOAccount.ServerID = Int64.Parse(mRow["ServerID"].ToString());
        //        if (mRow["StartDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
        //        }
        //        if (mRow["EndDate"] != System.DBNull.Value)
        //        {
        //            mDTOAccount.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
        //        }
        //        mDTOAccountList.Add(mDTOAccount);
        //    }

        //    return mDTOAccountList;
        //}


        #endregion


        #region ************************tblAccountType CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountTypeList AccountTypeList()
        {
            DTOAccountTypeList mDTOAccountTypeList = new DTOAccountTypeList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccountType_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOAccountType mDTOAccountType = new DTOAccountType();
                mDTOAccountType.AccountTypeID = int.Parse(mRow["AccountTypeID"].ToString());
                mDTOAccountType.AccountTypeCode = mRow["AccountTypeCode"].ToString();
                mDTOAccountType.AccountTypeText = mRow["AccountTypeText"].ToString();
                mDTOAccountTypeList.Add(mDTOAccountType);
            }

            return mDTOAccountTypeList;
        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountType AccountTypeListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountTypeID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccountType_ListByID", mParams);
            DTOAccountType mDTOAccountType = new DTOAccountType();
            foreach (DataRow mRow in mDT.Rows) //not required but...
            {
                mDTOAccountType.AccountTypeID = int.Parse(mRow["AccountTypeID"].ToString());
                mDTOAccountType.AccountTypeCode = mRow["AccountTypeCode"].ToString();
                mDTOAccountType.AccountTypeText = mRow["AccountTypeText"].ToString();
            }

            return mDTOAccountType;
        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountType AccountTypeCreateRecord(DTOAccountType mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblAccountType");
            DataRow mRow = mDT.NewRow();
            mRow["AccountTypeCode"] = mDTO.AccountTypeCode;
            mRow["AccountTypeText"] = mDTO.AccountTypeText;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblAccountType_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.AccountTypeID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountType AccountTypeUpdateRecord(DTOAccountType mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblAccountType");
            DataRow mRow = mDT.NewRow();
            mRow["AccountTypeID"] = mDTO.AccountTypeID;
            mRow["AccountTypeCode"] = mDTO.AccountTypeCode;
            mRow["AccountTypeText"] = mDTO.AccountTypeText;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblAccountType_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:47:17 PM Lazy Dog 3.3.1.0
        public DTOAccountType AccountTypeDeleteRecord(DTOAccountType mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountTypeID";
            mParam.ParameterValue = mDTO.AccountTypeID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblAccountType_DELETE", mParams);
            return mDTO;
        }


        //public DTOAccountTypeList AccountTypeListByNotEqualAccountTypeID(int accountTypeID)
        //{
        //    DTOAccountTypeList mDTOAccountTypeList = new DTOAccountTypeList();

        //    DTODBParameters mParams = new DTODBParameters();
        //    DTODBParameter mParam = new DTODBParameter();
        //    mParam.ParameterName = "@AccountTypeID";
        //    mParam.ParameterValue = accountTypeID;
        //    mParams.Add(mParam);
        //    DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccountType_ListByID", mParams);
            
        //    foreach (DataRow mRow in mDT.Rows)
        //    {
        //        DTOAccountType mDTOAccountType = new DTOAccountType();
        //        mDTOAccountType.AccountTypeID = int.Parse(mRow["AccountTypeID"].ToString());
        //        mDTOAccountType.AccountTypeCode = mRow["AccountTypeCode"].ToString();
        //        mDTOAccountType.AccountTypeText = mRow["AccountTypeText"].ToString();
        //        mDTOAccountTypeList.Add(mDTOAccountType);
        //    }

        //    return mDTOAccountTypeList;
        //}


        // Eldon - Changes

        public Int16 CheckNewAccountDetails(String pUsername, String pPassword, String pSalesOrgID, String pAccountID)
        {
            DTODBParameters mParams = new DTODBParameters();

            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@Username";
            mParam.ParameterValue = pUsername;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@Password";
            mParam.ParameterValue = pPassword;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = pSalesOrgID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@AccountID";
            mParam.ParameterValue = pAccountID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAccount_SelectUniqueUser", mParams);

            return Int16.Parse(mDT.Rows[0]["Result"].ToString());

        }



        #endregion






    } //end class
}

