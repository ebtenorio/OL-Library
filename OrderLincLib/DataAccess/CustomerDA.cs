using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;
using System.Transactions;

namespace OrderLinc.DataAccess
{
    public class CustomerDA : ICustomerDA
    {

        DatabaseService mDBService;

        public CustomerDA(DatabaseService dbService)
        {
            mDBService = dbService;
        }

        #region ************************tblContact CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOContactList ContactList()
        {
            DTOContactList mDTOContactList = new DTOContactList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblContact_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOContact mDTOContact = new DTOContact();
                mDTOContact.ContactID = int.Parse(mRow["ContactID"].ToString());
                mDTOContact.Phone = mRow["Phone"].ToString();
                mDTOContact.Fax = mRow["Fax"].ToString();
                mDTOContact.Mobile = mRow["Mobile"].ToString();
                mDTOContact.Email = mRow["Email"].ToString();
                mDTOContact.LastName = mRow["LastName"].ToString();
                mDTOContact.FirstName = mRow["FirstName"].ToString();
                mDTOContactList.Add(mDTOContact);
            }

            return mDTOContactList;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOContact ContactListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ContactID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblContact_ListByID", mParams);
            DTOContact mDTOContact = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOContact = new DTOContact();
                mDTOContact.ContactID = int.Parse(mRow["ContactID"].ToString());
                mDTOContact.Phone = mRow["Phone"].ToString();
                mDTOContact.Fax = mRow["Fax"].ToString();
                mDTOContact.Mobile = mRow["Mobile"].ToString();
                mDTOContact.Email = mRow["Email"].ToString();
                mDTOContact.LastName = mRow["LastName"].ToString();
                mDTOContact.FirstName = mRow["FirstName"].ToString();
            }

            return mDTOContact;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOContact ContactCreateRecord(DTOContact mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblContact");
            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();

            mRow["Phone"] = mDTO.Phone;
            mRow["Fax"] = mDTO.Fax;
            mRow["Mobile"] = mDTO.Mobile;
            mRow["Email"] = mDTO.Email;
            mRow["LastName"] = mDTO.LastName;
            mRow["FirstName"] = mDTO.FirstName;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblContact_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.ContactID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOContact ContactUpdateRecord(DTOContact mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblContact");
            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();

            mRow["ContactID"] = mDTO.ContactID;
            mRow["Phone"] = mDTO.Phone;
            mRow["Fax"] = mDTO.Fax;
            mRow["Mobile"] = mDTO.Mobile;
            mRow["Email"] = mDTO.Email;
            mRow["LastName"] = mDTO.LastName;
            mRow["FirstName"] = mDTO.FirstName;
            mRow["OldID"] = mDTO.OldID;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblContact_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOContact ContactDeleteRecord(DTOContact mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ContactID";
            mParam.ParameterValue = mDTO.ContactID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblContact_DELETE", mParams);
            return mDTO;
        }

        #endregion


        #region ************************tblCustomer CRUDS ******************************************




        private DTOCustomer DataRowToDTOCustomer(DataRow mRow)
        {
            DTOCustomer mDTOCustomer = new DTOCustomer();
            mDTOCustomer.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
            mDTOCustomer.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
            mDTOCustomer.BusinessNumber = mRow["BusinessNumber"].ToString();
            mDTOCustomer.CustomerName = mRow["CustomerName"].ToString();
            mDTOCustomer.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
            mDTOCustomer.AddressID = Int64.Parse(mRow["AddressID"].ToString());
            mDTOCustomer.SYSStateID = Int64.Parse(mRow["SYSStateID"].ToString());
            mDTOCustomer.RegionID = int.Parse(mRow["RegionID"].ToString());
            mDTOCustomer.ContactID = int.Parse(mRow["ContactID"].ToString());
            mDTOCustomer.BillToAddressID = Int64.Parse(mRow["BillToAddressID"].ToString());
            mDTOCustomer.ShipToAddressID = Int64.Parse(mRow["ShipToAddressID"].ToString());
            mDTOCustomer.Longitude = float.Parse(mRow["Longitude"].ToString());
            mDTOCustomer.Latitude = float.Parse(mRow["Latitude"].ToString());
            mDTOCustomer.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
            mDTOCustomer.InActive = Boolean.Parse(mRow["InActive"].ToString());

            try
            {
                mDTOCustomer.ProviderCustomerCode = mRow["ProviderCustomerCode"].ToString();
            }
            catch {


            }
            try
            {
                mDTOCustomer.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
            }
            catch
            {

            }
            try
            {
                mDTOCustomer.CustomerSalesRepID = Int64.Parse(mRow["CustomerSalesRepID"].ToString());
            }
            catch
            {

            }

            if (mRow.Table.Columns.Contains("ProviderCustomerCode"))
            {
                mDTOCustomer.CustomerCode = mRow["ProviderCustomerCode"].ToString();
            }


            if (mRow.Table.Columns.Contains("OldID")){
                if (mRow["OldID"] != System.DBNull.Value)
                mDTOCustomer.OldID = Int64.Parse(mRow["OldID"].ToString());
            }
            if (mRow.Table.Columns.Contains("StateName"))
                mDTOCustomer.StateName = mRow["StateName"].ToString();

            if (mRow.Table.Columns.Contains("StateCode"))
                mDTOCustomer.StateCode = mRow["StateCode"].ToString();

            if (mRow.Table.Columns.Contains("StartDate"))
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOCustomer.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
            if (mRow.Table.Columns.Contains("EndDate"))
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOCustomer.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
            if (mRow["DateCreated"] != System.DBNull.Value)
            {
                mDTOCustomer.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
            }
            if (mRow["DateUpdated"] != System.DBNull.Value)
            {
                mDTOCustomer.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
            }
            mDTOCustomer.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
            mDTOCustomer.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());

            return mDTOCustomer;
        }

        public DTOCustomer CustomerByCriteria(CustomerCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = string.Empty;


            if (criteria.SearchType == CustomerSearchType.ByCheckCustomerName)
            {
                sp = "usp_tblCustomer_CheckCustomerName";
                mParams.Add(new DTODBParameter("@CustomerName", criteria.CustomerName));
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
                mParams.Add(new DTODBParameter("@CustomerID", criteria.CustomerID));
            }
            else if (criteria.SearchType == CustomerSearchType.ByCustomerCode)
            {
                sp = "usp_tblCustomer_ListByCustomerCode";
                mParams.Add(new DTODBParameter("@CustomerCode", criteria.CustomerCode));
                mParams.Add(new DTODBParameter("@ProviderID", criteria.ProviderID));
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
            }
            else if (criteria.SearchType == CustomerSearchType.ByBusinessNumber)
            {
                sp = "usp_tblCustomer_ListByBusinessNumber";
                mParams.Add(new DTODBParameter("@BusinessNumber", criteria.BusinessNumber));
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
                mParams.Add(new DTODBParameter("@CustomerID", criteria.CustomerID));
            }
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);
            DTOCustomer mDTOCustomer = null;

            if (mDT.Rows.Count > 0)
                mDTOCustomer = DataRowToDTOCustomer(mDT.Rows[0]);


            return mDTOCustomer;
        }

        public DTOCustomerList CustomerList()
        {
            DTOCustomerList mDTOCustomerList = new DTOCustomerList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomer_List");
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOCustomerList.Add(DataRowToDTOCustomer(mRow));
            }

            return mDTOCustomerList;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomer CustomerListByID(long providerID, long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomer_ListByID", mParams);
            DTOCustomer mDTOCustomer = null;

            if (mDT.Rows.Count > 0)
                mDTOCustomer = DataRowToDTOCustomer(mDT.Rows[0]);


            return mDTOCustomer;
        }

        private void ProviderCustomerCreateRecord(DTOCustomer mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProviderCustomer");
            DataRow mRow = mDT.NewRow();
            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["CustomerID"] = mDTO.CustomerID;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mRow["ProviderCustomerCode"] = mDTO.CustomerCode;
            mDT.Rows.Add(mRow);

            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProviderCustomer_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            // mDTO.ProviderID = ObjectID;

        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomer CustomerCreateRecord(DTOCustomer mDTO)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DataTable mDT = mDBService.GenerateCreateTable("tblCustomer");
                DataRow mRow = mDT.NewRow();

                mDT.Columns.Remove("OldID");
                mDT.AcceptChanges();

                mRow["SalesOrgID"] = mDTO.SalesOrgID;
                mRow["BusinessNumber"] = mDTO.BusinessNumber;
                mRow["CustomerName"] = mDTO.CustomerName;
                mRow["SalesRepAccountID"] = mDTO.SalesRepAccountID;
                mRow["AddressID"] = mDTO.AddressID;
                mRow["SYSStateID"] = mDTO.SYSStateID;
                mRow["RegionID"] = mDTO.RegionID;
                mRow["ContactID"] = mDTO.ContactID;
                mRow["BillToAddressID"] = mDTO.BillToAddressID;
                mRow["ShipToAddressID"] = mDTO.ShipToAddressID;
                mRow["Longitude"] = mDTO.Longitude;
                mRow["Latitude"] = mDTO.Latitude;
                mRow["Deleted"] = mDTO.Deleted;
                mRow["InActive"] = mDTO.InActive;
                mRow["DateCreated"] = mDTO.DateCreated;
                mRow["DateUpdated"] = mDTO.DateUpdated;
                mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
                mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
                mDT.Rows.Add(mRow);

                Object mRetval = mDBService.CreateRecord(mDT, "usp_tblCustomer_INSERT");
                Int64 ObjectID = Int64.Parse(mRetval.ToString());
                mDTO.CustomerID = ObjectID;

                if (mDTO.ProviderID > 0)
                    ProviderCustomerCreateRecord(mDTO);
                scope.Complete();

            }
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomer CustomerUpdateRecord(DTOCustomer mDTO)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DataTable mDT = mDBService.GenerateUpdateTable("tblCustomer");
                DataRow mRow = mDT.NewRow();

                mDT.Columns.Remove("OldID");
                mDT.AcceptChanges();

                mRow["CustomerID"] = mDTO.CustomerID;
                mRow["SalesOrgID"] = mDTO.SalesOrgID;
                mRow["BusinessNumber"] = mDTO.BusinessNumber;
                mRow["CustomerName"] = mDTO.CustomerName;
                mRow["SalesRepAccountID"] = mDTO.SalesRepAccountID;
                mRow["AddressID"] = mDTO.AddressID;
                mRow["SYSStateID"] = mDTO.SYSStateID;
                mRow["RegionID"] = mDTO.RegionID;
                mRow["ContactID"] = mDTO.ContactID;
                mRow["BillToAddressID"] = mDTO.BillToAddressID;
                mRow["ShipToAddressID"] = mDTO.ShipToAddressID;
                mRow["Longitude"] = mDTO.Longitude;
                mRow["Latitude"] = mDTO.Latitude;
                mRow["Deleted"] = mDTO.Deleted;
                mRow["InActive"] = mDTO.InActive;
                mRow["DateCreated"] = mDTO.DateCreated;
                mRow["DateUpdated"] = mDTO.DateUpdated;
                mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
                mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
           
                mDT.Rows.Add(mRow);
                mDBService.UpdateRecord(mDT, "usp_tblCustomer_UPDATE");

                if (mDTO.ProviderID > 0)
                    ProviderCustomerCreateRecord(mDTO);
                scope.Complete();

            }
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomer CustomerDeleteRecord(DTOCustomer mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            mParam.ParameterValue = mDTO.CustomerID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblCustomer_DELETE", mParams);
            return mDTO;
        }

        public DataTable CustomerDataTableByProviderID(long providerID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);


            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomer_ListByProviderID", mParams);

            return mDT;
        }

        public DTOCustomerList CustomerListByProviderID(long providerID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            DTOCustomerList mDTOCustomerList = new DTOCustomerList();

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomer_ListByProviderID", mParams);
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOCustomerList.Add(DataRowToDTOCustomer(mRow));
            }

            return mDTOCustomerList;
        }

        public DTOCustomerList CustomerListByCriteria(CustomerCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = string.Empty;

            if (criteria.SearchType == CustomerSearchType.BySalesRepID)
            {
                mParams.Add(new DTODBParameter("@SalesRepAccountID", criteria.AccountID));
                mParams.Add(new DTODBParameter("@CurrentPage", criteria.CurrentPage));
                mParams.Add(new DTODBParameter("@PageItemCount", criteria.PageItemCount));
                sp = "usp_tblCustomer_ListBySalesRepAccountID";
            }
            else if (criteria.SearchType == CustomerSearchType.BySearch)
            {
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
                mParams.Add(new DTODBParameter("@ProviderID", criteria.ProviderID));
                mParams.Add(new DTODBParameter("@SYSStateID", criteria.SYSStateID));
                mParams.Add(new DTODBParameter("@CustomerSearch", criteria.CustomerName));
                mParams.Add(new DTODBParameter("@CurrentPage", criteria.CurrentPage));
                mParams.Add(new DTODBParameter("@PageItemCount", criteria.PageItemCount));
                sp = "usp_tblCustomer_ListBySearch";
            }
            else if (criteria.SearchType == CustomerSearchType.ByCustomerSalesOrgSearch)
            {
                mParams.Add(new DTODBParameter("@SalesOrgID", criteria.SalesOrgID));
                mParams.Add(new DTODBParameter("@SYSStateID", criteria.SYSStateID));
                mParams.Add(new DTODBParameter("@CustomerSearch", criteria.CustomerName));
                mParams.Add(new DTODBParameter("@CurrentPage", criteria.CurrentPage));
                mParams.Add(new DTODBParameter("@PageItemCount", criteria.PageItemCount));
                sp = "usp_tblCustomer_ListBySalesOrgSearch";
            }

            DTOCustomerList mDTOCustomerList = new DTOCustomerList();

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
            {
                mDTOCustomerList.TotalRecords = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
            }
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOCustomerList.Add(DataRowToDTOCustomer(mRow));
            }

            return mDTOCustomerList;
        }


        #endregion


        public DTOCustomerList CustomerListByProviderSearchPage(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = string.Empty;

            mParams.Add(new DTODBParameter("@SalesOrgID", salesOrgID));
            mParams.Add(new DTODBParameter("@ProviderID", providerID));
            mParams.Add(new DTODBParameter("@SYSStateID", stateID));
            mParams.Add(new DTODBParameter("@CustomerSearch", customerName));
            mParams.Add(new DTODBParameter("@CurrentPage", currentPage));
            mParams.Add(new DTODBParameter("@PageItemCount", pageItemCount));
            sp = "usp_tblCustomer_ListProviderBySearch";

            DTOCustomerList mDTOCustomerList = new DTOCustomerList();

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
            {
                mDTOCustomerList.TotalRecords = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
            }
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOCustomerList.Add(DataRowToDTOCustomer(mRow));
            }

            return mDTOCustomerList;
        }


        public DTOCustomerList CustomerListByProviderSearchPage_WithDateFilter(long providerID, long salesOrgID, int stateID, string customerName, int currentPage, int pageItemCount)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = string.Empty;

            mParams.Add(new DTODBParameter("@SalesOrgID", salesOrgID));
            mParams.Add(new DTODBParameter("@ProviderID", providerID));
            mParams.Add(new DTODBParameter("@SYSStateID", stateID));
            mParams.Add(new DTODBParameter("@CustomerSearch", customerName));
            mParams.Add(new DTODBParameter("@CurrentPage", currentPage));
            mParams.Add(new DTODBParameter("@PageItemCount", pageItemCount));
            sp = "usp_tblCustomer_ListProviderBySearch_withDateFilter";

            DTOCustomerList mDTOCustomerList = new DTOCustomerList();

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
            {
                mDTOCustomerList.TotalRecords = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
            }
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOCustomerList.Add(DataRowToDTOCustomer(mRow));
            }

            return mDTOCustomerList;
        }

        

        public DTOCustomerList ListCustomerBySalesOrgNotInCustomerSalesRep( string customerName, long SalesOrgID, long AccountID, long SYSStateID, int CurrentPage, int PageItemCount)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();


            mParams.Add(new DTODBParameter("@CustomerSearch", customerName));
            mParams.Add(new DTODBParameter("@SalesOrgID", SalesOrgID));
            mParams.Add(new DTODBParameter("@AccountID", AccountID));
            mParams.Add(new DTODBParameter("@StateID", SYSStateID));
            mParams.Add(new DTODBParameter("@CurrentPage", CurrentPage));
            mParams.Add(new DTODBParameter("@PageItemCount", PageItemCount));



            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomer_ListBySalesOrgSearch_NotInCustomerSalesRep", mParams);


            DTOCustomerList mDTOCustomerList = new DTOCustomerList();

          

            if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
            {
                mDTOCustomerList.TotalRecords = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
            }
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOCustomerList.Add(DataRowToDTOCustomer(mRow));

            }

            return mDTOCustomerList;
         

        }

        public DTOCustomerSalesRepList ListAllAssignedCustomer( int AccountID) {

            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();


            
            mParams.Add(new DTODBParameter("@AccountID", AccountID));



           DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomerSalesRep_AllAssignCustomer", mParams);

           DTOCustomerSalesRepList mDTOCustomerList = new DTOCustomerSalesRepList();



           //if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
           //{
           //    mDTOCustomerList.TotalRecords = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
           //}
           foreach (DataRow mRow in mDT.Rows)
           {

               mDTOCustomerList.Add(DataRowToDTOSalesRep(mRow));

           }

           return mDTOCustomerList;


        }

        public DTOCustomerList ListCustomerAllBySalesOrgNotInCustomerSalesRep(string customerName, long SalesOrgID, long AccountID, long SYSStateID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();


            mParams.Add(new DTODBParameter("@CustomerSearch", customerName));
            mParams.Add(new DTODBParameter("@SalesOrgID", SalesOrgID));
            mParams.Add(new DTODBParameter("@AccountID", AccountID));
            mParams.Add(new DTODBParameter("@StateID", SYSStateID));
         


            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomer_ListALLBySalesOrgSearch_NotInCustomerSalesRep", mParams);


            DTOCustomerList mDTOCustomerList = new DTOCustomerList();



            //if (mDT.Rows.Count > 0 && mDT.Columns.Contains("TotalRecords"))
            //{
            //    mDTOCustomerList.TotalRecords = int.Parse(mDT.Rows[0]["TotalRecords"].ToString());
            //}
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOCustomerList.Add(DataRowToDTOCustomer(mRow));

            }

            return mDTOCustomerList;


        }


        #region ************************tblCustomerSalesRep CRUDS ******************************************

        private DTOCustomerSalesRep DataRowToDTOSalesRep(DataRow mRow)
        {
            DTOCustomerSalesRep mDTOCustomerSalesRep = new DTOCustomerSalesRep();
            mDTOCustomerSalesRep.CustomerSalesRepID = Int64.Parse(mRow["CustomerSalesRepID"].ToString());
            mDTOCustomerSalesRep.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
            mDTOCustomerSalesRep.SalesRepAccountID = Int64.Parse(mRow["SalesRepAccountID"].ToString());
            mDTOCustomerSalesRep.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
            mDTOCustomerSalesRep.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
            mDTOCustomerSalesRep.StateName = mRow["StateName"].ToString();

            mDTOCustomerSalesRep.CustomerCode = mRow["BusinessNumber"].ToString();
            mDTOCustomerSalesRep.CustomerName = mRow["CustomerName"].ToString();


            if (mRow.IsNull("StartDate") == false)
            {
                mDTOCustomerSalesRep.StartDate = mRow.Field<DateTime>("StartDate");
            }
            if (mRow.IsNull("EndDate") == false)
            {
                mDTOCustomerSalesRep.EndDate = mRow.Field<DateTime>("EndDate");
            }

            if (mRow["DateCreated"] != System.DBNull.Value)
            {
                mDTOCustomerSalesRep.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
            }
            return mDTOCustomerSalesRep;

        }

        public DTOCustomerSalesRepList CustomerSalesRepListByCriteria(CustomerCriteria customerCriteria)
        {
            string sp = string.Empty;
            DTODBParameters mParams = new DTODBParameters();

            if (customerCriteria.SearchType == CustomerSearchType.ByCustomerAndSalesRep)
            {
                mParams.Add(new DTODBParameter("@CustomerID", customerCriteria.CustomerID));
                mParams.Add(new DTODBParameter("@SalesRepAccountID", customerCriteria.AccountID));
                sp = "usp_tblCustomerSalesRep_ListBySearch";
            }
            else if (customerCriteria.SearchType == CustomerSearchType.BySalesRepID)
            {
                mParams.Add(new DTODBParameter("@CurrentPage", customerCriteria.CurrentPage));
                mParams.Add(new DTODBParameter("@PageItemCount", customerCriteria.PageItemCount));
                mParams.Add(new DTODBParameter("@SalesRepAccountID", customerCriteria.AccountID));
                //  sp = "usp_tblCustomerSalesRep_ListBySalesRepID";
                sp = "usp_tblCustomer_ListBySalesRepAccountID";
            }

            DTOCustomerSalesRepList mDTOCustomerSalesRepList = new DTOCustomerSalesRepList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);

            foreach (DataRow mRow in mDT.Rows)
            {
                mDTOCustomerSalesRepList.Add(DataRowToDTOSalesRep(mRow));
            }

            return mDTOCustomerSalesRepList;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomerSalesRepList CustomerSalesRepList()
        {
            DTOCustomerSalesRepList mDTOCustomerSalesRepList = new DTOCustomerSalesRepList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomerSalesRep_List");
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOCustomerSalesRepList.Add(DataRowToDTOSalesRep(mRow));
            }

            return mDTOCustomerSalesRepList;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomerSalesRep CustomerSalesRepListByID(long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerSalesRepID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomerSalesRep_ListByID", mParams);
            DTOCustomerSalesRep mDTOCustomerSalesRep = null;

            if (mDT.Rows.Count > 0)
                mDTOCustomerSalesRep = DataRowToDTOSalesRep(mDT.Rows[0]);

            return mDTOCustomerSalesRep;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomerSalesRep CustomerSalesRepCreateRecord(DTOCustomerSalesRep mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblCustomerSalesRep");
            DataRow mRow = mDT.NewRow();
            mRow["CustomerID"] = mDTO.CustomerID;
            mRow["SalesRepAccountID"] = mDTO.SalesRepAccountID;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblCustomerSalesRep_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.CustomerSalesRepID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomerSalesRep CustomerSalesRepUpdateRecord(DTOCustomerSalesRep mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblCustomerSalesRep");
            DataRow mRow = mDT.NewRow();
            mRow["CustomerSalesRepID"] = mDTO.CustomerSalesRepID;
            mRow["CustomerID"] = mDTO.CustomerID;
            mRow["SalesRepAccountID"] = mDTO.SalesRepAccountID;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblCustomerSalesRep_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOCustomerSalesRep CustomerSalesRepDeleteRecord(DTOCustomerSalesRep mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerSalesRepID";
            mParam.ParameterValue = mDTO.CustomerSalesRepID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblCustomerSalesRep_DELETE", mParams);
            return mDTO;
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
            DTOLogoList mDTOLogoList = new DTOLogoList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblLogo_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOLogo mDTOLogo = new DTOLogo();
                mDTOLogo.LogoID = Int64.Parse(mRow["LogoID"].ToString());
                mDTOLogo.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
                mDTOLogo.LogoURL = mRow["LogoURL"].ToString();
                mDTOLogo.LogoDescription = mRow["LogoDescription"].ToString();
                mDTOLogoList.Add(mDTOLogo);
            }

            return mDTOLogoList;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOLogo LogoListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@LogoID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblLogo_ListByID", mParams);
            DTOLogo mDTOLogo = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOLogo = new DTOLogo();
                mDTOLogo.LogoID = Int64.Parse(mRow["LogoID"].ToString());
                mDTOLogo.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
                mDTOLogo.LogoURL = mRow["LogoURL"].ToString();
                mDTOLogo.LogoDescription = mRow["LogoDescription"].ToString();
            }

            return mDTOLogo;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOLogo LogoCreateRecord(DTOLogo mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblLogo");
            DataRow mRow = mDT.NewRow();
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["LogoURL"] = mDTO.LogoURL;
            mRow["LogoDescription"] = mDTO.LogoDescription;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblLogo_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.LogoID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOLogo LogoUpdateRecord(DTOLogo mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblLogo");
            DataRow mRow = mDT.NewRow();
            mRow["LogoID"] = mDTO.LogoID;
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["LogoURL"] = mDTO.LogoURL;
            mRow["LogoDescription"] = mDTO.LogoDescription;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblLogo_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOLogo LogoDeleteRecord(DTOLogo mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@LogoID";
            mParam.ParameterValue = mDTO.LogoID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblLogo_DELETE", mParams);
            return mDTO;
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


        public DTOLogo LogoListBySalesOrgID(long salesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = salesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblLogo_ListBySalesOrgID", mParams);
            DTOLogo mDTOLogo = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOLogo = new DTOLogo();
                mDTOLogo.LogoID = Int64.Parse(mRow["LogoID"].ToString());
                mDTOLogo.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
                mDTOLogo.LogoURL = mRow["LogoURL"].ToString();
                mDTOLogo.LogoDescription = mRow["LogoDescription"].ToString();
            }

            return mDTOLogo;
        }

        #endregion


        #region ************************tblOrgUnit CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnitList OrgUnitList()
        {
            DTOOrgUnitList mDTOOrgUnitList = new DTOOrgUnitList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrgUnit_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrgUnit mDTOOrgUnit = new DTOOrgUnit();
                mDTOOrgUnit.OrgUnitID = int.Parse(mRow["OrgUnitID"].ToString());
                mDTOOrgUnit.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrgUnit.OrgUnitName = mRow["OrgUnitName"].ToString();
                mDTOOrgUnitList.Add(mDTOOrgUnit);
            }

            return mDTOOrgUnitList;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnit OrgUnitListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@OrgUnitID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrgUnit_ListByID", mParams);
            DTOOrgUnit mDTOOrgUnit = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOOrgUnit = new DTOOrgUnit();
                mDTOOrgUnit.OrgUnitID = int.Parse(mRow["OrgUnitID"].ToString());
                mDTOOrgUnit.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrgUnit.OrgUnitName = mRow["OrgUnitName"].ToString();
            }

            return mDTOOrgUnit;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnit OrgUnitCreateRecord(DTOOrgUnit mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblOrgUnit");
            DataRow mRow = mDT.NewRow();
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["OrgUnitName"] = mDTO.OrgUnitName;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblOrgUnit_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.OrgUnitID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnit OrgUnitUpdateRecord(DTOOrgUnit mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblOrgUnit");
            DataRow mRow = mDT.NewRow();
            mRow["OrgUnitID"] = mDTO.OrgUnitID;
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["OrgUnitName"] = mDTO.OrgUnitName;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblOrgUnit_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOOrgUnit OrgUnitDeleteRecord(DTOOrgUnit mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@OrgUnitID";
            mParam.ParameterValue = mDTO.OrgUnitID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblOrgUnit_DELETE", mParams);
            return mDTO;
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

        public DTOOrgUnitList OrgUnitListBySalesOrgID(long salesOrgID)
        {
            DTOOrgUnitList mDTOOrgUnitList = new DTOOrgUnitList();

            DTODBParameters mParams = new DTODBParameters();
            mParams.Add(new DTODBParameter("@SalesOrgID", salesOrgID));

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblOrgUnit_ListBySalesOrgID", mParams);
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOOrgUnit mDTOOrgUnit = new DTOOrgUnit();
                mDTOOrgUnit.OrgUnitID = int.Parse(mRow["OrgUnitID"].ToString());
                mDTOOrgUnit.SalesOrgID = int.Parse(mRow["SalesOrgID"].ToString());
                mDTOOrgUnit.OrgUnitName = mRow["OrgUnitName"].ToString();
                mDTOOrgUnitList.Add(mDTOOrgUnit);
            }

            return mDTOOrgUnitList;
        }
        #endregion




        #region ************************tblSalesOrg CRUDS ******************************************


        private DTOSalesOrg DataRowToDTOSalesOrg(DataRow mRow)
        {
            DTOSalesOrg mDTOSalesOrg = new DTOSalesOrg();
            mDTOSalesOrg.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
            mDTOSalesOrg.SalesOrgCode = mRow["SalesOrgCode"].ToString();
            mDTOSalesOrg.BusinessNumber = mRow["BusinessNumber"].ToString();
            mDTOSalesOrg.SalesOrgName = mRow["SalesOrgName"].ToString();
            mDTOSalesOrg.SalesOrgShortName = mRow["SalesOrgShortName"].ToString();
            mDTOSalesOrg.AddressID = Int64.Parse(mRow["AddressID"].ToString());
            mDTOSalesOrg.Longitude = float.Parse(mRow["Longitude"].ToString());
            if (mRow["ContactID"] != System.DBNull.Value)
            {
                mDTOSalesOrg.ContactID = Int64.Parse(mRow["ContactID"].ToString());
            }
            else {
                throw new Exception("SalesOrg "+ mDTOSalesOrg.SalesOrgName + " contactID is null");
            }
            mDTOSalesOrg.Latitude = float.Parse(mRow["Latitude"].ToString());
            mDTOSalesOrg.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
            mDTOSalesOrg.UseGTINExport = Boolean.Parse(mRow["UseGTINExport"].ToString());
            mDTOSalesOrg.InActive = Boolean.Parse(mRow["InActive"].ToString());
            mDTOSalesOrg.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
            mDTOSalesOrg.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
            mDTOSalesOrg.LogoID = Int64.Parse(mRow["LogoID"].ToString());


            if (mRow["IsOrderHeld"] != System.DBNull.Value)
            {
                mDTOSalesOrg.IsOrderHeld = Boolean.Parse(mRow["IsOrderHeld"].ToString());
            }
            else
            {
                throw new Exception("SalesOrg " + mDTOSalesOrg.SalesOrgName + " IsOrderHeld is null");
            }
           

            if (mRow["DateCreated"] != System.DBNull.Value)
            {
                mDTOSalesOrg.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
            }
            if (mRow["DateUpdated"] != System.DBNull.Value)
            {
                mDTOSalesOrg.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
            }

            return mDTOSalesOrg;

        }

        public DataTable SalesOrgDataTable()
        {
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSalesOrg_List");

            return mDT;
        }

        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        public DTOSalesOrgList SalesOrgList()
        {
            DTOSalesOrgList mDTOSalesOrgList = new DTOSalesOrgList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSalesOrg_List");
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOSalesOrgList.Add(DataRowToDTOSalesOrg(mRow));
            }

            return mDTOSalesOrgList;
        }

        public DTOSalesOrg SalesOrgListByID(long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSalesOrg_ListByID", mParams);
            DTOSalesOrg mDTOSalesOrg = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOSalesOrg = DataRowToDTOSalesOrg(mRow);
            }

            return mDTOSalesOrg;
        }

        public DTOSalesOrg SalesOrgListByShortName(string salesOrgShortName)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgShortName";
            mParam.ParameterValue = salesOrgShortName;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSalesOrg_ListBySalesOrgShortName", mParams);
            DTOSalesOrg mDTOSalesOrg = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOSalesOrg = DataRowToDTOSalesOrg(mRow);
            }

            return mDTOSalesOrg;
        }

        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        public DTOSalesOrg SalesOrgCreateRecord(DTOSalesOrg mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblSalesOrg");
            DataRow mRow = mDT.NewRow();
            mRow["SalesOrgCode"] = mDTO.SalesOrgCode;
            mRow["BusinessNumber"] = mDTO.BusinessNumber;
            mRow["SalesOrgName"] = mDTO.SalesOrgName;
            mRow["AddressID"] = mDTO.AddressID;
            mRow["Longitude"] = mDTO.Longitude;
            mRow["ContactID"] = mDTO.ContactID;
            mRow["Latitude"] = mDTO.Latitude;
            mRow["Deleted"] = mDTO.Deleted;
            mRow["UseGTINExport"] = mDTO.UseGTINExport;
            mRow["InActive"] = mDTO.InActive;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
            mRow["LogoID"] = mDTO.LogoID;
            mRow["IsOrderHeld"] = mDTO.IsOrderHeld;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblSalesOrg_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.SalesOrgID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        public DTOSalesOrg SalesOrgUpdateRecord(DTOSalesOrg mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblSalesOrg");
            DataRow mRow = mDT.NewRow();
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["SalesOrgCode"] = mDTO.SalesOrgCode;
            mRow["BusinessNumber"] = mDTO.BusinessNumber;
            mRow["SalesOrgName"] = mDTO.SalesOrgName;
            mRow["AddressID"] = mDTO.AddressID;
            mRow["Longitude"] = mDTO.Longitude;
            mRow["ContactID"] = mDTO.ContactID;
            mRow["Latitude"] = mDTO.Latitude;
            mRow["Deleted"] = mDTO.Deleted;
            mRow["UseGTINExport"] = mDTO.UseGTINExport;
            mRow["InActive"] = mDTO.InActive;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
            mRow["LogoID"] = mDTO.LogoID;
            mRow["IsOrderHeld"] = mDTO.IsOrderHeld;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblSalesOrg_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 11:21:36 PM Lazy Dog 3.3.1.0
        public DTOSalesOrg SalesOrgDeleteRecord(DTOSalesOrg mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mDTO.SalesOrgID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblSalesOrg_DELETE", mParams);
            return mDTO;
        }

        #endregion


    } //end class

}
