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
    public class AddressDA : IAddressDA
    {

        DatabaseService mDBService;

        public AddressDA(DatabaseService dbService)
        {
            mDBService = dbService;
        }



        #region ************************tblAddress CRUDS ******************************************


        private DTOAddress DataRowToDTOAddress(DataRow mRow)
        {
            DTOAddress mDTOAddress = new DTOAddress();
            mDTOAddress.AddressID = Int64.Parse(mRow["AddressID"].ToString());
            mDTOAddress.AddressTypeID = int.Parse(mRow["AddressTypeID"].ToString());
            mDTOAddress.AddressLine1 = mRow["AddressLine1"].ToString();
            mDTOAddress.AddressLine2 = mRow["AddressLine2"].ToString();
            mDTOAddress.CitySuburb = mRow["CitySuburb"].ToString();
            mDTOAddress.SYSStateID = int.Parse(mRow["SYSStateID"].ToString());
            mDTOAddress.PostalZipCode = mRow["PostalZipCode"].ToString();
            mDTOAddress.StateCode = mRow["StateCode"].ToString();
            mDTOAddress.StateName = mRow["StateName"].ToString();

            return mDTOAddress;
        }
        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddress AddressListByID(long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AddressID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAddress_ListByID", mParams);
            DTOAddress mDTOAddress = null;
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOAddress = DataRowToDTOAddress(mRow);

            }

            return mDTOAddress;
        }

        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddress AddressCreateRecord(DTOAddress mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblAddress");
            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();

            mRow["AddressTypeID"] = mDTO.AddressTypeID;
            mRow["AddressLine1"] = mDTO.AddressLine1;
            mRow["AddressLine2"] = mDTO.AddressLine2;
            mRow["CitySuburb"] = mDTO.CitySuburb;
            mRow["SYSStateID"] = mDTO.SYSStateID;
            mRow["PostalZipCode"] = mDTO.PostalZipCode;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblAddress_INSERT");
            Int64 ObjectID = Int64.Parse(mRetval.ToString());
            mDTO.AddressID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddress AddressUpdateRecord(DTOAddress mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblAddress");
            DataRow mRow = mDT.NewRow();

            mDT.Columns.Remove("OldID");
            mDT.AcceptChanges();

            mRow["AddressID"] = mDTO.AddressID;
            mRow["AddressTypeID"] = mDTO.AddressTypeID;
            mRow["AddressLine1"] = mDTO.AddressLine1;
            mRow["AddressLine2"] = mDTO.AddressLine2;
            mRow["CitySuburb"] = mDTO.CitySuburb;
            mRow["SYSStateID"] = mDTO.SYSStateID;
            mRow["PostalZipCode"] = mDTO.PostalZipCode;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblAddress_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddress AddressDeleteRecord(DTOAddress mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AddressID";
            mParam.ParameterValue = mDTO.AddressID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblAddress_DELETE", mParams);
            return mDTO;
        }


        #endregion


        #region ************************tblAddressType CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddressTypeList AddressTypeList()
        {
            DTOAddressTypeList mDTOAddressTypeList = new DTOAddressTypeList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAddressType_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOAddressType mDTOAddressType = new DTOAddressType();
                mDTOAddressType.AddressTypeID = int.Parse(mRow["AddressTypeID"].ToString());
                mDTOAddressType.AddressTypeText = mRow["AddressTypeText"].ToString();
                mDTOAddressTypeList.Add(mDTOAddressType);
            }

            return mDTOAddressTypeList;
        }

        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddressType AddressTypeListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AddressTypeID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblAddressType_ListByID", mParams);
            DTOAddressType mDTOAddressType = new DTOAddressType();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOAddressType.AddressTypeID = int.Parse(mRow["AddressTypeID"].ToString());
                mDTOAddressType.AddressTypeText = mRow["AddressTypeText"].ToString();
            }

            return mDTOAddressType;
        }

        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddressType AddressTypeCreateRecord(DTOAddressType mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblAddressType");
            DataRow mRow = mDT.NewRow();
            mRow["AddressTypeText"] = mDTO.AddressTypeText;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblAddressType_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.AddressTypeID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddressType AddressTypeUpdateRecord(DTOAddressType mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblAddressType");
            DataRow mRow = mDT.NewRow();
            mRow["AddressTypeID"] = mDTO.AddressTypeID;
            mRow["AddressTypeText"] = mDTO.AddressTypeText;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblAddressType_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:37:52 PM Lazy Dog 3.3.1.0
        public DTOAddressType AddressTypeDeleteRecord(DTOAddressType mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@AddressTypeID";
            mParam.ParameterValue = mDTO.AddressTypeID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblAddressType_DELETE", mParams);
            return mDTO;
        }


        #endregion

        #region ************************tblSYSState CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSStateList SYSStateList()
        {
            DTOSYSStateList mDTOSYSStateList = new DTOSYSStateList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSState_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTOSYSState mDTOSYSState = new DTOSYSState();
                mDTOSYSState.SYSStateID = int.Parse(mRow["SYSStateID"].ToString());
                mDTOSYSState.StateName = mRow["StateName"].ToString();
                mDTOSYSState.StateCode = mRow["StateCode"].ToString();
                mDTOSYSStateList.Add(mDTOSYSState);
            }

            return mDTOSYSStateList;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSState SYSStateListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSStateID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSState_ListByID", mParams);
            DTOSYSState mDTOSYSState = new DTOSYSState();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOSYSState.SYSStateID = int.Parse(mRow["SYSStateID"].ToString());
                mDTOSYSState.StateName = mRow["StateName"].ToString();
                mDTOSYSState.StateCode = mRow["StateCode"].ToString();
            }

            return mDTOSYSState;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSState SYSStateCreateRecord(DTOSYSState mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblSYSState");
            DataRow mRow = mDT.NewRow();
            mRow["StateName"] = mDTO.StateName;
            mRow["StateCode"] = mDTO.StateCode;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblSYSState_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.SYSStateID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSState SYSStateUpdateRecord(DTOSYSState mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblSYSState");
            DataRow mRow = mDT.NewRow();
            mRow["SYSStateID"] = mDTO.SYSStateID;
            mRow["StateName"] = mDTO.StateName;
            mRow["StateCode"] = mDTO.StateCode;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblSYSState_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTOSYSState SYSStateDeleteRecord(DTOSYSState mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@SYSStateID";
            mParam.ParameterValue = mDTO.SYSStateID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblSYSState_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public Boolean SYSStateIsValid(DTOSYSState mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.StateName == null)
            {
                mValidationResponse = "StateName cannnot be null.";
                return false;
            }

            if (mDTO.StateCode == null)
            {
                mValidationResponse = "StateCode cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        public DTOSYSState SYSStateListByCustomerID(long mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@CustomerID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSState_ListByCustomerID", mParams);
            DTOSYSState mDTOSYSState = new DTOSYSState();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTOSYSState.SYSStateID = int.Parse(mRow["SYSStateID"].ToString());
                mDTOSYSState.StateName = mRow["StateName"].ToString();
                mDTOSYSState.StateCode = mRow["StateCode"].ToString();
            }

            return mDTOSYSState;
        }

        #endregion

        #region ************************tblRegion CRUDS ******************************************

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegionList RegionList()
        {
            DTORegionList mDTORegionList = new DTORegionList();
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblRegion_List");
            foreach (DataRow mRow in mDT.Rows)
            {
                DTORegion mDTORegion = new DTORegion();
                mDTORegion.RegionID = int.Parse(mRow["RegionID"].ToString());
                mDTORegion.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTORegion.RegionName = (byte[])mRow["RegionName"];
                mDTORegionList.Add(mDTORegion);
            }

            return mDTORegionList;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegion RegionListByID(int mRecNo)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@RegionID";
            mParam.ParameterValue = mRecNo;
            mParams.Add(mParam);
            DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblRegion_ListByID", mParams);
            DTORegion mDTORegion = new DTORegion();
            foreach (DataRow mRow in mDT.Rows)//not required but...
            {
                mDTORegion.RegionID = int.Parse(mRow["RegionID"].ToString());
                mDTORegion.SalesOrgID = Int64.Parse(mRow["SalesOrgID"].ToString());
                mDTORegion.RegionName = (byte[])mRow["RegionName"];
            }

            return mDTORegion;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegion RegionCreateRecord(DTORegion mDTO)
        {
            DataTable mDT = mDBService.GenerateCreateTable("tblRegion");
            DataRow mRow = mDT.NewRow();
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["RegionName"] = mDTO.RegionName;
            mDT.Rows.Add(mRow);
            Object mRetval = mDBService.CreateRecord(mDT, "usp_tblRegion_INSERT");
            int ObjectID = int.Parse(mRetval.ToString());
            mDTO.RegionID = ObjectID;
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegion RegionUpdateRecord(DTORegion mDTO)
        {
            DataTable mDT = mDBService.GenerateUpdateTable("tblRegion");
            DataRow mRow = mDT.NewRow();
            mRow["RegionID"] = mDTO.RegionID;
            mRow["SalesOrgID"] = mDTO.SalesOrgID;
            mRow["RegionName"] = mDTO.RegionName;
            mDT.Rows.Add(mRow);
            mDBService.UpdateRecord(mDT, "usp_tblRegion_UPDATE");
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public DTORegion RegionDeleteRecord(DTORegion mDTO)
        {
            DTODBParameters mParams = new DTODBParameters();
            DTODBParameter mParam = new DTODBParameter();
            mParam.ParameterName = "@RegionID";
            mParam.ParameterValue = mDTO.RegionID;
            mParams.Add(mParam);
            mDBService.DeleteRecord("usp_tblRegion_DELETE", mParams);
            return mDTO;
        }

        //CASE Generated Code 5/7/2014 4:30:20 PM Lazy Dog 3.3.1.0
        public Boolean RegionIsValid(DTORegion mDTO, out string mValidationResponse)
        {
            //please add your validation rules here. - Lazy Dog 

            if (mDTO.SalesOrgID == 0)
            {
                mValidationResponse = "SalesOrgID cannnot be 0.";
                return false;
            }

            if (mDTO.RegionName == null)
            {
                mValidationResponse = "RegionName cannnot be null.";
                return false;
            }

            mValidationResponse = "Ok";
            return true;
        }

        #endregion


    } //end class

}
