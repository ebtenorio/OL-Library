using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.IDataContracts;
using OrderLinc.DTOs;

namespace OrderLinc.Services
{
    public class ReportService
    {

        DatabaseService mDBService;

        public ReportService(DatabaseService dbService)
        {

            mDBService = dbService;
        }
        public DataTable UserListBySalesOrgID(int mSalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_RPT_tblAccount_List", mParams);
            return mDT;
        }

        public DataTable ProductListBySalesOrgID(int mSalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_RPT_tblProduct_ListBySalesOrgID", mParams);
            return mDT;
        }

        public DataTable StoreListBySalesOrgID(int mSalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_RPT_tblCustomer_ListBySalesOrgID", mParams);
            return mDT;
        }


        public DataTable OrderReleaseListBySalesOrgID(int mSalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_RPT_tblOrderRelease_ListBySalesOrgID", mParams);
            return mDT;
        }



        public DataTable ProvidersListBySalesOrgID(int mSalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_RPT_tblProvider_ListbySalesOrgID", mParams);
            return mDT;
        }



        public DataTable WareHouseListBySalesOrgID(int mSalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_RPT_tblProviderWarehouse_ListByProviderID", mParams);
            return mDT;
        }




        public DataTable OrderLincOrders(int mSalesOrgID, DateTime mFromDate, DateTime mToDate)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@FromDate";
            mParam.ParameterValue = mFromDate.ToString("yyyy-MM-dd");
            mParams.Add(mParam);

            mParam = new DTODBParameter();
            mParam.ParameterName = "@ToDate";
            mParam.ParameterValue = mToDate.ToString("yyyy-MM-dd");
            mParams.Add(mParam);

            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_OrderLincOrders_Report", mParams);
            return mDT;
        }

        public DataTable OrderLincProducts(int mSalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_OrderLincProducts_Report", mParams);
            return mDT;
        }

        public DataTable OrderLincStores(int mSalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_OrderLincStores_Report", mParams);
            return mDT;
        }

        public DataTable OrderLincUsers(int mSalesOrgID)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SalesOrgID";
            mParam.ParameterValue = mSalesOrgID;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_OrderLincUsers_Report", mParams);
            return mDT;
        }

        public string Reportingpath()
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ConfigKey";
            mParam.ParameterValue = "ReportingPath";
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSConfig_ListByConfigKey", mParams);
            return mDT.Rows[0]["ConfigValue"].ToString();
        }

        public string ExportTime()
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@ConfigKey";
            mParam.ParameterValue = "ReportingExportTime";
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSConfig_ListByConfigKey", mParams);
            return mDT.Rows[0]["ConfigValue"].ToString();
        }

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
            mDTOSalesOrg.ContactID = Int64.Parse(mRow["ContactID"].ToString());
            mDTOSalesOrg.Latitude = float.Parse(mRow["Latitude"].ToString());
            mDTOSalesOrg.Deleted = Boolean.Parse(mRow["Deleted"].ToString());
            mDTOSalesOrg.UseGTINExport = Boolean.Parse(mRow["UseGTINExport"].ToString());
            mDTOSalesOrg.InActive = Boolean.Parse(mRow["InActive"].ToString());
            mDTOSalesOrg.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
            mDTOSalesOrg.UpdatedByUserID = long.Parse(mRow["UpdatedByUserID"].ToString());
            mDTOSalesOrg.LogoID = Int64.Parse(mRow["LogoID"].ToString());
            mDTOSalesOrg.IsOrderHeld = Boolean.Parse(mRow["IsOrderHeld"].ToString());

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





    }
}
