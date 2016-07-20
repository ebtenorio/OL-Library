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
    public class ProviderDA : IProviderDA
    {

        DatabaseService mDBService;

        public ProviderDA(DatabaseService dbService)
        {

            mDBService = dbService;
        }


        #region ************************tblProvider CRUDS ******************************************

        private DTOProvider DataRowToDTOProvider(DataRow mRow)
        {
            DTOProvider mDTOProvider = new DTOProvider();
            mDTOProvider.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
            mDTOProvider.ProviderCode = mRow["ProviderCode"].ToString();
            mDTOProvider.BusinessNumber = mRow["BusinessNumber"].ToString();
            mDTOProvider.ProviderName = mRow["ProviderName"].ToString();
            mDTOProvider.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
            mDTOProvider.AddressID = Int64.Parse(mRow["AddressID"].ToString());
            mDTOProvider.Longitude = float.Parse(mRow["Longitude"].ToString());
            mDTOProvider.Latitude = float.Parse(mRow["Latitude"].ToString());
            mDTOProvider.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
            mDTOProvider.InActive = Boolean.Parse(mRow["InActive"].ToString());
            
            if (mRow["DateCreated"] != System.DBNull.Value)
            {
                mDTOProvider.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
            }
            
            if (mRow["DateUpdated"] != System.DBNull.Value)
            {
                mDTOProvider.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
            }

            // Additional fields for the PepsiCo Distributor Project
            //if (mRow["ContactID"] != System.DBNull.Value)
            //{
            //    mDTOProvider.ContactID = long.Parse(mRow["ContactID"].ToString());
            //}

            if (mRow["IsPepsiDistributor"] != System.DBNull.Value)
            {
                mDTOProvider.IsPepsiDistributor = Boolean.Parse(mRow["IsPepsiDistributor"].ToString());
            }
            
            mDTOProvider.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
            mDTOProvider.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());

            return mDTOProvider;
        }
        public DataTable ProviderDataTable()
        {
            return mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProvider_List");
        }


        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderList ProviderList()
        {
            DTOProviderList mDTOProviderList = new DTOProviderList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProvider_List");
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOProviderList.Add(DataRowToDTOProvider(mRow));
            }

            return mDTOProviderList;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProvider ProviderListByID(long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProvider_ListByID", mParams);
            DTOProvider mDTOProvider = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProvider = DataRowToDTOProvider(mRow);
            }

            return mDTOProvider;
        }

        public DTOContact ContactListByProviderWareHouseID(long providerWarehouseID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderWareHouseID";
            mParam.ParameterValue = providerWarehouseID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblContact_ListByProviderWarehouseID", mParams);
            DTOContact mDTOContact = new DTOContact();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
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

        public DTOProvider ProviderListByProviderCode(string providerCode)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderCode";
            mParam.ParameterValue = providerCode;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProvider_ListByProviderCode", mParams);
            DTOProvider mDTOProvider = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProvider = DataRowToDTOProvider(mRow);
            }

            return mDTOProvider;
        }

        public Int64 CheckifProductCodeExist_by_SalesOrgAndProvider(int SalesOrgID, int ProviderID, int ProductID, string ProductCode)
        {

            try
            {

                Int64 res = 0;

                DTODBParameters mParams = new DTODBParameters();
                DTODBParameter mParam = new DTODBParameter();
                mParam.ParameterName = "@SalesOrgID";
                mParam.ParameterValue = SalesOrgID;
                mParams.Add(mParam);

                mParam = new DTODBParameter();
                mParam.ParameterName = "@ProviderID";
                mParam.ParameterValue = ProviderID;
                mParams.Add(mParam);



                mParam = new DTODBParameter();
                mParam.ParameterName = "@ProductID";
                mParam.ParameterValue = ProductID;
                mParams.Add(mParam);


                mParam = new DTODBParameter();
                mParam.ParameterName = "@ProductCode";
                mParam.ParameterValue = ProductCode;
                mParams.Add(mParam);


                DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProduct_CheckProductCodeifExist_ProviderAndSalesOrg", mParams);

                if (mDT.Rows.Count > 0)
                {
                    res = int.Parse(mDT.Rows[0][0].ToString());

                }
                else {

                    res = 0;
                }

               

                return res;


            }
            catch (Exception ex)
            {

                return 0;

            }

        }

        public DTOProviderCustomer CheckifCustomerCodeExist_by_SalesOrgAndProvider(int SalesOrgID, int ProviderID, int CustomerID, string CustomerCode)
        {

            try
            {

                DTODBParameters mParams = new DTODBParameters();
                DTODBParameter mParam = new DTODBParameter();
                mParam.ParameterName = "@SalesOrgID";
                mParam.ParameterValue = SalesOrgID;
                mParams.Add(mParam);

                mParam = new DTODBParameter();
                mParam.ParameterName = "@ProviderID";
                mParam.ParameterValue = ProviderID;
                mParams.Add(mParam);



                mParam = new DTODBParameter();
                mParam.ParameterName = "@CustomerID";
                mParam.ParameterValue = CustomerID;
                mParams.Add(mParam);


                mParam = new DTODBParameter();
                mParam.ParameterName = "@CustomerCode";
                mParam.ParameterValue = CustomerCode;
                mParams.Add(mParam);


                DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomer_CheckCustomerCodeifExist_ProviderAndSalesOrg", mParams);
                
                DTOProviderCustomer mDTOProviderCustomer = new DTOProviderCustomer();

                foreach (DataRow mRow in mDT.Rows)
                {
                   
                    mDTOProviderCustomer.ProviderCustomerID = Int64.Parse(mRow["ProviderCustomerID"].ToString());
                    mDTOProviderCustomer.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                    mDTOProviderCustomer.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                    mDTOProviderCustomer.ProviderCustomerCode = mRow["ProviderCustomerCode"].ToString();
                    mDTOProviderCustomer.CustomerName = mRow["CustomerName"].ToString();
                    if (mRow["StartDate"] != System.DBNull.Value)
                    {
                        mDTOProviderCustomer.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                    }
                    if (mRow["EndDate"] != System.DBNull.Value)
                    {
                        mDTOProviderCustomer.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                    }
                   
                }


                return mDTOProviderCustomer;


            }
            catch (Exception ex)
            {

                return null;
            }

        }


        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProvider ProviderCreateRecord(DTOProvider mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProvider");
            DataRow mRow = mDT.NewRow();
            mRow["ProviderCode"] = mDTO.ProviderCode;
            mRow["BusinessNumber"] = mDTO.BusinessNumber;
            mRow["ProviderName"] = mDTO.ProviderName;
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["AddressID"] = mDTO.AddressID;
            mRow["Longitude"] = mDTO.Longitude;
            mRow["Latitude"] = mDTO.Latitude;
            mRow["Deleted"] = mDTO.Deleted;
            mRow["InActive"] = mDTO.InActive;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;

            // Additional fields for PepsiCo Distributor Project
            //mRow["ContactID"] = mDTO.ContactID;
            mRow["IsPepsiDistributor"] = mDTO.IsPepsiDistributor;

            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProvider_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.ProviderID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProvider ProviderUpdateRecord(DTOProvider mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProvider");
            DataRow mRow = mDT.NewRow();
            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProviderCode"] = mDTO.ProviderCode;
            mRow["BusinessNumber"] = mDTO.BusinessNumber;
            mRow["ProviderName"] = mDTO.ProviderName;
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["AddressID"] = mDTO.AddressID;
            mRow["Longitude"] = mDTO.Longitude;
            mRow["Latitude"] = mDTO.Latitude;
            mRow["Deleted"] = mDTO.Deleted;
            mRow["InActive"] = mDTO.InActive;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;            
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;

            // Additional fields for the PepsiCo Distributor Project
            //mRow["ContactID"] = mDTO.ContactID;
            mRow["IsPepsiDistributor"] = mDTO.IsPepsiDistributor;

            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProvider_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProvider ProviderDeleteRecord(DTOProvider mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = mDTO.ProviderID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProvider_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderList ProviderListBySalesOrgID(long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
             DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProvider_ListBySalesOrgID", mParams);
            DTOProviderList mDTOProviderList = new DTOProviderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProvider mDTOProvider = new DTOProvider();
                mDTOProvider.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProvider.ProviderCode = mRow["ProviderCode"].ToString();
                mDTOProvider.BusinessNumber = mRow["BusinessNumber"].ToString();
                mDTOProvider.ProviderName = mRow["ProviderName"].ToString();
                mDTOProvider.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProvider.AddressID = Int64.Parse(mRow["AddressID"].ToString());
                mDTOProvider.Longitude = float.Parse(mRow["Longitude"].ToString());
                mDTOProvider.Latitude = float.Parse(mRow["Latitude"].ToString());
                mDTOProvider.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
                mDTOProvider.InActive = Boolean.Parse(mRow["InActive"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOProvider.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOProvider.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }

                // Additional fields for the PepsiCo Distributor Project

                //if (mRow["ContactID"] != System.DBNull.Value)
                //{
                //    mDTOProvider.ContactID = long.Parse(mRow["ContactID"].ToString());
                //}

                if (mRow["IsPepsiDistributor"] != System.DBNull.Value)
                {
                    mDTOProvider.IsPepsiDistributor = Boolean.Parse(mRow["IsPepsiDistributor"].ToString());                
                }
                

                mDTOProvider.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOProvider.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                
                mDTOProviderList.Add(mDTOProvider);
            }

            return mDTOProviderList;
        }


        public DTOProviderList ProviderListBySalesOrgIDWithFilter(long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProvider_ListBySalesOrgIDWithFilter", mParams);
            DTOProviderList mDTOProviderList = new DTOProviderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProvider mDTOProvider = new DTOProvider();
                mDTOProvider.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProvider.ProviderCode = mRow["ProviderCode"].ToString();
                mDTOProvider.BusinessNumber = mRow["BusinessNumber"].ToString();
                mDTOProvider.ProviderName = mRow["ProviderName"].ToString();
                mDTOProvider.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProvider.AddressID = Int64.Parse(mRow["AddressID"].ToString());
                mDTOProvider.Longitude = float.Parse(mRow["Longitude"].ToString());
                mDTOProvider.Latitude = float.Parse(mRow["Latitude"].ToString());
                mDTOProvider.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
                mDTOProvider.InActive = Boolean.Parse(mRow["InActive"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOProvider.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOProvider.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }

                // Additional fields for the PepsiCo Distributor Project

                //if (mRow["ContactID"] != System.DBNull.Value)
                //{
                //    mDTOProvider.ContactID = long.Parse(mRow["ContactID"].ToString());
                //}

                if (mRow["IsPepsiDistributor"] != System.DBNull.Value)
                {
                    mDTOProvider.IsPepsiDistributor = bool.Parse(mRow["IsPepsiDistributor"].ToString());
                }

                mDTOProvider.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOProvider.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                mDTOProviderList.Add(mDTOProvider);
            }

            return mDTOProviderList;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderList ProviderListByAddressID(long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AddressID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProvider_ListByAddressID", mParams);
            DTOProviderList mDTOProviderList = new DTOProviderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProvider mDTOProvider = new DTOProvider();
                mDTOProvider.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProvider.ProviderCode = mRow["ProviderCode"].ToString();
                mDTOProvider.BusinessNumber = mRow["BusinessNumber"].ToString();
                mDTOProvider.ProviderName = mRow["ProviderName"].ToString();
                mDTOProvider.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProvider.AddressID = Int64.Parse(mRow["AddressID"].ToString());
                mDTOProvider.Longitude = float.Parse(mRow["Longitude"].ToString());
                mDTOProvider.Latitude = float.Parse(mRow["Latitude"].ToString());
                mDTOProvider.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
                mDTOProvider.InActive = Boolean.Parse(mRow["InActive"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOProvider.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOProvider.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOProvider.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOProvider.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                mDTOProviderList.Add(mDTOProvider);
            }

            return mDTOProviderList;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderList ProviderListByUpdatedByUserID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@UpdatedByUserID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProvider_ListByUpdatedByUserID", mParams);
            DTOProviderList mDTOProviderList = new DTOProviderList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProvider mDTOProvider = new DTOProvider();
                mDTOProvider.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProvider.ProviderCode = mRow["ProviderCode"].ToString();
                mDTOProvider.BusinessNumber = mRow["BusinessNumber"].ToString();
                mDTOProvider.ProviderName = mRow["ProviderName"].ToString();
                mDTOProvider.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTOProvider.AddressID = Int64.Parse(mRow["AddressID"].ToString());
                mDTOProvider.Longitude = float.Parse(mRow["Longitude"].ToString());
                mDTOProvider.Latitude = float.Parse(mRow["Latitude"].ToString());
                mDTOProvider.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
                mDTOProvider.InActive = Boolean.Parse(mRow["InActive"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOProvider.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOProvider.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOProvider.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOProvider.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                mDTOProviderList.Add(mDTOProvider);
            }

            return mDTOProviderList;
        }

        #endregion


        #region ************************tblProviderCustomer CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderCustomerList ProviderCustomerList()
        {
            DTOProviderCustomerList mDTOProviderCustomerList = new DTOProviderCustomerList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderCustomer_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProviderCustomer mDTOProviderCustomer = new DTOProviderCustomer();
                mDTOProviderCustomer.ProviderCustomerID = Int64.Parse(mRow["ProviderCustomerID"].ToString());
                mDTOProviderCustomer.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOProviderCustomer.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderCustomer.ProviderCustomerCode = mRow["ProviderCustomerCode"].ToString();
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
                mDTOProviderCustomerList.Add(mDTOProviderCustomer);
            }

            return mDTOProviderCustomerList;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderCustomer ProviderCustomerListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderCustomerID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderCustomer_ListByID", mParams);
            DTOProviderCustomer mDTOProviderCustomer = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProviderCustomer = new DTOProviderCustomer();
                mDTOProviderCustomer.ProviderCustomerID = Int64.Parse(mRow["ProviderCustomerID"].ToString());
                mDTOProviderCustomer.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOProviderCustomer.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderCustomer.ProviderCustomerCode = mRow["ProviderCustomerCode"].ToString();
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
            }

            return mDTOProviderCustomer;
        }


  
        public DTOProviderCustomer ProviderCustomerList(long providerID, long customerID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            mParam.ParameterValue = customerID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderCustomer_List", mParams);
            DTOProviderCustomer mDTOProviderCustomer = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProviderCustomer = new DTOProviderCustomer();
                mDTOProviderCustomer.ProviderCustomerID = Int64.Parse(mRow["ProviderCustomerID"].ToString());
                mDTOProviderCustomer.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOProviderCustomer.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderCustomer.ProviderCustomerCode = mRow["ProviderCustomerCode"].ToString();
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
            }

            return mDTOProviderCustomer;
        }


        public DTOProviderCustomerList ProviderCustomerListbyCustomerID(long CustomerID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            mParam.ParameterValue = CustomerID;
            mParams.Add(mParam);


            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblCustomerProduct_ListByCustomerID", mParams);
            DTOProviderCustomerList mDTOProviderCustomerList = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                DTOProviderCustomer mDTOProviderCustomer = new DTOProviderCustomer();

                mDTOProviderCustomer = new DTOProviderCustomer();
                mDTOProviderCustomer.ProviderCustomerID = Int64.Parse(mRow["ProviderCustomerID"].ToString());
                mDTOProviderCustomer.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOProviderCustomer.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderCustomer.ProviderCustomerCode = mRow["ProviderCustomerCode"].ToString();
                mDTOProviderCustomer.ProviderName = mRow["ProviderName"].ToString();
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }

                mDTOProviderCustomerList.Add(mDTOProviderCustomer);
            }

            return mDTOProviderCustomerList;
        }


        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderCustomer ProviderCustomerCreateRecord(DTOProviderCustomer mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProviderCustomer");
            DataRow mRow = mDT.NewRow();
            mRow["CustomerID"] = mDTO.CustomerID;
            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProviderCustomerCode"] = mDTO.ProviderCustomerCode;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProviderCustomer_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.ProviderCustomerID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderCustomer ProviderCustomerUpdateRecord(DTOProviderCustomer mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProviderCustomer");
            DataRow mRow = mDT.NewRow();
            mRow["ProviderCustomerID"] = mDTO.ProviderCustomerID;
            mRow["CustomerID"] = mDTO.CustomerID;
            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProviderCustomerCode"] = mDTO.ProviderCustomerCode;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProviderCustomer_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderCustomer ProviderCustomerDeleteRecord(DTOProviderCustomer mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderCustomerID";
            mParam.ParameterValue = mDTO.ProviderCustomerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = mDTO.ProviderID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            mParam.ParameterValue = mDTO.CustomerID;
            mParams.Add(mParam);


            mDBService.DeleteRecord("usp_tblProviderCustomer_DELETE", mParams);
            return mDTO;
        }


        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderCustomerList ProviderCustomerListByCustomerID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderCustomer_ListByCustomerID", mParams);
            DTOProviderCustomerList mDTOProviderCustomerList = new DTOProviderCustomerList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProviderCustomer mDTOProviderCustomer = new DTOProviderCustomer();
                mDTOProviderCustomer.ProviderCustomerID = Int64.Parse(mRow["ProviderCustomerID"].ToString());
                mDTOProviderCustomer.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOProviderCustomer.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderCustomer.ProviderCustomerCode = mRow["ProviderCustomerCode"].ToString();
                mDTOProviderCustomer.ProviderName = mRow["ProviderName"].ToString();
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
                mDTOProviderCustomerList.Add(mDTOProviderCustomer);
            }

            return mDTOProviderCustomerList;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderCustomerList ProviderCustomerListByProviderID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderCustomer_ListByProviderID", mParams);
            DTOProviderCustomerList mDTOProviderCustomerList = new DTOProviderCustomerList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProviderCustomer mDTOProviderCustomer = new DTOProviderCustomer();
                mDTOProviderCustomer.ProviderCustomerID = Int64.Parse(mRow["ProviderCustomerID"].ToString());
                mDTOProviderCustomer.CustomerID = Int64.Parse(mRow["CustomerID"].ToString());
                mDTOProviderCustomer.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderCustomer.ProviderCustomerCode = mRow["ProviderCustomerCode"].ToString();
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderCustomer.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
                mDTOProviderCustomerList.Add(mDTOProviderCustomer);
            }

            return mDTOProviderCustomerList;
        }

        #endregion


        #region ************************tblProviderProduct CRUDS ******************************************



        public DTOProviderProduct ProviderProductListByProductID(long providerID, long productID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductID";
            mParam.ParameterValue = productID;
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderProduct_ListByID", mParams);
            DTOProviderProduct mDTOProviderProduct = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProviderProduct = new DTOProviderProduct();
                mDTOProviderProduct.ProviderProductID = Int64.Parse(mRow["ProviderProductID"].ToString());
                mDTOProviderProduct.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderProduct.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProviderProduct.ProviderProductCode = mRow["ProviderProductCode"].ToString();

                if (mRow["Discount"] == null || mRow["Discount"].ToString() == "")
                {
                    mDTOProviderProduct.Discount = 0.00f;
                }
                else
                {
                    mDTOProviderProduct.Discount = float.Parse(mRow["Discount"].ToString());
                }

                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
            }

            return mDTOProviderProduct;
        }

        public void UpdateCustomerCode(long customerID, long providerID, string newCustomerCode)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            mParam.ParameterValue = customerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderCustomerCode";
            mParam.ParameterValue = newCustomerCode;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderCustomer_UpdateCustomerCode", mParams);
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProductList ProviderProductList()
        {
            DTOProviderProductList mDTOProviderProductList = new DTOProviderProductList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderProduct_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProviderProduct mDTOProviderProduct = new DTOProviderProduct();
                mDTOProviderProduct.ProviderProductID = Int64.Parse(mRow["ProviderProductID"].ToString());
                mDTOProviderProduct.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderProduct.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProviderProduct.ProviderProductCode = mRow["ProviderProductCode"].ToString();
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
                mDTOProviderProductList.Add(mDTOProviderProduct);
            }

            return mDTOProviderProductList;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProduct ProviderProductListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderProductID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderProduct_ListByID", mParams);
            DTOProviderProduct mDTOProviderProduct = null;

            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOProviderProduct = new DTOProviderProduct();
                mDTOProviderProduct.ProviderProductID = Int64.Parse(mRow["ProviderProductID"].ToString());
                mDTOProviderProduct.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderProduct.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProviderProduct.ProviderProductCode = mRow["ProviderProductCode"].ToString();
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
            }

            return mDTOProviderProduct;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProduct ProviderProductCreateRecord(DTOProviderProduct mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProviderProduct");
            DataRow mRow = mDT.NewRow();

            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProductID"] = mDTO.ProductID;
            mRow["ProviderProductCode"] = mDTO.ProviderProductCode;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mRow["Discount"] = mDTO.Discount;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProviderProduct_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.ProviderProductID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProduct ProviderProductUpdateRecord(DTOProviderProduct mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProviderProduct");
            DataRow mRow = mDT.NewRow();
            mRow["ProviderProductID"] = mDTO.ProviderProductID;
            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProductID"] = mDTO.ProductID;
            mRow["ProviderProductCode"] = mDTO.ProviderProductCode;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mRow["Discount"] = mDTO.Discount;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProviderProduct_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProduct ProviderProductDeleteRecord(DTOProviderProduct mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderProductID";
            mParam.ParameterValue = mDTO.ProviderProductID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = mDTO.ProviderID;
            mParams.Add(mParam);


            mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductID";
            mParam.ParameterValue = mDTO.ProductID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProviderProduct_DELETE", mParams);
            return mDTO;
        }




        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProductList ProviderProductListByProviderID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderProduct_ListByProviderID", mParams);
            DTOProviderProductList mDTOProviderProductList = new DTOProviderProductList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProviderProduct mDTOProviderProduct = new DTOProviderProduct();
                mDTOProviderProduct.ProviderProductID = Int64.Parse(mRow["ProviderProductID"].ToString());
                mDTOProviderProduct.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderProduct.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProviderProduct.ProviderProductCode = mRow["ProviderProductCode"].ToString();
                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
                mDTOProviderProductList.Add(mDTOProviderProduct);
            }

            return mDTOProviderProductList;
        }


        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderProductList ProviderProductListByProductID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProductID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderProduct_ListByProductID", mParams);
            DTOProviderProductList mDTOProviderProductList = new DTOProviderProductList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProviderProduct mDTOProviderProduct = new DTOProviderProduct();
                mDTOProviderProduct.ProviderProductID = Int64.Parse(mRow["ProviderProductID"].ToString());
                mDTOProviderProduct.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderProduct.ProductID = Int64.Parse(mRow["ProductID"].ToString());
                mDTOProviderProduct.ProviderProductCode = mRow["ProviderProductCode"].ToString();
                mDTOProviderProduct.ProviderName = mRow["ProviderName"].ToString();

                if (mRow["Discount"] == null || mRow["Discount"].ToString() == "")
                {
                    mDTOProviderProduct.Discount = 0.00f;
                }
                else
                {
                    mDTOProviderProduct.Discount = float.Parse(mRow["Discount"].ToString());
                }


                if (mRow["StartDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.StartDate = DateTime.Parse(mRow["StartDate"].ToString());
                }
                if (mRow["EndDate"] != System.DBNull.Value)
                {
                    mDTOProviderProduct.EndDate = DateTime.Parse(mRow["EndDate"].ToString());
                }
                mDTOProviderProductList.Add(mDTOProviderProduct);
            }

            return mDTOProviderProductList;
        }

        #endregion


        #region ************************tblProviderWarehouse CRUDS ******************************************

        private DTOProviderWarehouse DataRowToProviderWarehouse(DataRow mRow)
        {
            DTOProviderWarehouse mDTOProviderWarehouse = new DTOProviderWarehouse();
            mDTOProviderWarehouse.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
            mDTOProviderWarehouse.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
            mDTOProviderWarehouse.ProviderWarehouseCode = mRow["ProviderWarehouseCode"].ToString();
            mDTOProviderWarehouse.ProviderWarehouseName = mRow["ProviderWarehouseName"].ToString();
            mDTOProviderWarehouse.AddressID = Int64.Parse(mRow["AddressID"].ToString());
            mDTOProviderWarehouse.Longitude = float.Parse(mRow["Longitude"].ToString());
            mDTOProviderWarehouse.Latitude = float.Parse(mRow["Latitude"].ToString());
            mDTOProviderWarehouse.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
            mDTOProviderWarehouse.InActive = Boolean.Parse(mRow["InActive"].ToString());

            if (mRow.IsNull("StartDate") == false)
            {
                mDTOProviderWarehouse.StartDate = mRow.Field<DateTime>("StartDate");
            }
            if (mRow.IsNull("EndDate") == false)
            {
                mDTOProviderWarehouse.StartDate = mRow.Field<DateTime>("EndDate");
            }
            if (mRow["DateCreated"] != System.DBNull.Value)
            {
                mDTOProviderWarehouse.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
            }

            if (mRow["DateUpdated"] != System.DBNull.Value)
            {
                mDTOProviderWarehouse.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
            }

            if (mRow["ContactID"] != System.DBNull.Value)
            {
                if (mRow["ContactID"].ToString().Trim() != "")
                {
                    mDTOProviderWarehouse.ContactID = long.Parse(mRow["ContactID"].ToString());
                }
            }

            mDTOProviderWarehouse.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
            mDTOProviderWarehouse.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
            return mDTOProviderWarehouse;

        }
        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouseList ProviderWarehouseList()
        {
            DTOProviderWarehouseList mDTOProviderWarehouseList = new DTOProviderWarehouseList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderWarehouse_List");
            foreach (DataRow mRow in mDT.Rows)
            {

                mDTOProviderWarehouseList.Add(DataRowToProviderWarehouse(mRow));
            }

            return mDTOProviderWarehouseList;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouse ProviderWarehouseListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderWarehouseID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderWarehouse_ListByID", mParams);
            DTOProviderWarehouse mDTOProviderWarehouse = null;

            if (mDT.Rows.Count > 0)
                mDTOProviderWarehouse = DataRowToProviderWarehouse(mDT.Rows[0]);


            return mDTOProviderWarehouse;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouse ProviderWarehouseCreateRecord(DTOProviderWarehouse mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblProviderWarehouse");
            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();

            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProviderWarehouseCode"] = mDTO.ProviderWarehouseCode;
            mRow["ProviderWarehouseName"] = mDTO.ProviderWarehouseName;
            mRow["AddressID"] = mDTO.AddressID;
            mRow["Longitude"] = mDTO.Longitude;
            mRow["Latitude"] = mDTO.Latitude;
            mRow["Deleted"] = mDTO.Deleted;
            mRow["InActive"] = mDTO.InActive;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mRow["ContactID"] = mDTO.ContactID;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblProviderWarehouse_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.ProviderWarehouseID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouse ProviderWarehouseUpdateRecord(DTOProviderWarehouse mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblProviderWarehouse");
            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();

            mRow["ProviderWarehouseID"] = mDTO.ProviderWarehouseID;
            mRow["ProviderID"] = mDTO.ProviderID;
            mRow["ProviderWarehouseCode"] = mDTO.ProviderWarehouseCode;
            mRow["ProviderWarehouseName"] = mDTO.ProviderWarehouseName;
            mRow["AddressID"] = mDTO.AddressID;
            mRow["Longitude"] = mDTO.Longitude;
            mRow["Latitude"] = mDTO.Latitude;
            mRow["Deleted"] = mDTO.Deleted;
            mRow["InActive"] = mDTO.InActive;
            mRow["DateCreated"] = mDTO.DateCreated;
            mRow["DateUpdated"] = mDTO.DateUpdated;
            mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
            mRow["UpdatedByUserID"] = mDTO.UpdatedByUserID;
            mRow["StartDate"] = mDTO.StartDate;
            mRow["EndDate"] = mDTO.EndDate;
            mRow["ContactID"] = mDTO.ContactID;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblProviderWarehouse_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouse ProviderWarehouseDeleteRecord(DTOProviderWarehouse mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderWarehouseID";
            mParam.ParameterValue = mDTO.ProviderWarehouseID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblProviderWarehouse_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouseList ProviderWarehouseListByProviderID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderWarehouse_ListByProviderID", mParams);
            DTOProviderWarehouseList mDTOProviderWarehouseList = new DTOProviderWarehouseList();
            foreach (DataRow mRow in mDT.Rows)
            {
                mDTOProviderWarehouseList.Add(DataRowToProviderWarehouse(mRow));
            }

            return mDTOProviderWarehouseList;
        }

        public DTOProviderWarehouse ProviderWarehouseByCriteria(ProviderWarehouseCriteria criteria)
        {
            DTODBParameters mParams = new DTODBParameters();
            string sp = string.Empty;

            if (criteria.SearchType == ProviderWarehouseSearchType.ByProviderWarehouseCode)
            {
                mParams.Add(new DTODBParameter("@ProviderID", criteria.ProviderID));
                mParams.Add(new DTODBParameter("@ProviderWarehouseCode", criteria.ProviderWarehouseCode));
                sp = "usp_tblProviderWarehouse_ListByProviderWarehouseCode";
            }

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, sp, mParams);
            DTOProviderWarehouse mDTOProviderWarehouse = null;

            foreach (DataRow mRow in mDT.Rows)
            {
                mDTOProviderWarehouse = DataRowToProviderWarehouse(mRow);
            }

            return mDTOProviderWarehouse;
        }


        public DataTable ProviderWarehouseDataTableByProviderID(int providerID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ProviderID";
            mParam.ParameterValue = providerID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderWarehouse_ListByProviderID", mParams);

            return mDT;
        }

        //CASE Generated Code 5/7/2014 4:36:15 PM Lazy Dog 3.3.1.0
        public DTOProviderWarehouseList ProviderWarehouseListByAddressID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AddressID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblProviderWarehouse_ListByAddressID", mParams);
            DTOProviderWarehouseList mDTOProviderWarehouseList = new DTOProviderWarehouseList();
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOProviderWarehouse mDTOProviderWarehouse = new DTOProviderWarehouse();
                mDTOProviderWarehouse.ProviderWarehouseID = int.Parse(mRow["ProviderWarehouseID"].ToString());
                mDTOProviderWarehouse.ProviderID = Int64.Parse(mRow["ProviderID"].ToString());
                mDTOProviderWarehouse.ProviderWarehouseCode = mRow["ProviderWarehouseCode"].ToString();
                mDTOProviderWarehouse.ProviderWarehouseName = mRow["ProviderWarehouseName"].ToString();
                mDTOProviderWarehouse.AddressID = Int64.Parse(mRow["AddressID"].ToString());
                mDTOProviderWarehouse.Longitude = float.Parse(mRow["Longitude"].ToString());
                mDTOProviderWarehouse.Latitude = float.Parse(mRow["Latitude"].ToString());
                mDTOProviderWarehouse.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
                mDTOProviderWarehouse.InActive = Boolean.Parse(mRow["InActive"].ToString());
                if (mRow["DateCreated"] != System.DBNull.Value)
                {
                    mDTOProviderWarehouse.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
                }
                if (mRow["DateUpdated"] != System.DBNull.Value)
                {
                    mDTOProviderWarehouse.DateUpdated = DateTime.Parse(mRow["DateUpdated"].ToString());
                }
                mDTOProviderWarehouse.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
                mDTOProviderWarehouse.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
                mDTOProviderWarehouseList.Add(mDTOProviderWarehouse);
            }

            return mDTOProviderWarehouseList;
        }

        #endregion




    } //end class

}
