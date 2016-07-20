using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;


public class ConfigurationDA : IConfigurationDA
{
    DatabaseService mDBService;

    public ConfigurationDA(DatabaseService dbService)
    {
        mDBService = dbService;
    }

    #region ************************tblServer CRUDS ******************************************

    //CASE Generated Code 5/7/2014 11:59:09 PM Lazy Dog 3.3.1.0
    public DTOServerList ServerList()
    {
        DTOServerList mDTOServerList = new DTOServerList();
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblServer_List");
        foreach (DataRow mRow in mDT.Rows)
        {
            DTOServer mDTOServer = new DTOServer();
            mDTOServer.ServerID = Int64.Parse(mRow["ServerID"].ToString());
            mDTOServer.ServerName = mRow["ServerName"].ToString();
            mDTOServer.ServerURL = mRow["ServerURL"].ToString();
            mDTOServerList.Add(mDTOServer);
        }

        return mDTOServerList;
    }

    //CASE Generated Code 5/7/2014 11:59:09 PM Lazy Dog 3.3.1.0
    public DTOServer ServerListByID(long mRecNo)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@ServerID";
        mParam.ParameterValue = mRecNo;
        mParams.Add(mParam);
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblServer_ListByID", mParams);
        DTOServer mDTOServer = new DTOServer();
        foreach (DataRow mRow in mDT.Rows)//not required but...
        {
            mDTOServer.ServerID = Int64.Parse(mRow["ServerID"].ToString());
            mDTOServer.ServerName = mRow["ServerName"].ToString();
            mDTOServer.ServerURL = mRow["ServerURL"].ToString();
        }

        return mDTOServer;
    }

    //CASE Generated Code 5/7/2014 11:59:09 PM Lazy Dog 3.3.1.0
    public DTOServer ServerCreateRecord(DTOServer mDTO)
    {
        DataTable mDT = mDBService.GenerateCreateTable("tblServer");
        DataRow mRow = mDT.NewRow();
        mRow["ServerName"] = mDTO.ServerName;
        mRow["ServerURL"] = mDTO.ServerURL;
        mDT.Rows.Add(mRow);
        Object mRetval = mDBService.CreateRecord(mDT, "usp_tblServer_INSERT");
        Int64 ObjectID = Int64.Parse(mRetval.ToString());
        mDTO.ServerID = ObjectID;
        return mDTO;
    }

    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public DTOServer ServerUpdateRecord(DTOServer mDTO)
    {
        DataTable mDT = mDBService.GenerateUpdateTable("tblServer");
        DataRow mRow = mDT.NewRow();
        mRow["ServerID"] = mDTO.ServerID;
        mRow["ServerName"] = mDTO.ServerName;
        mRow["ServerURL"] = mDTO.ServerURL;
        mDT.Rows.Add(mRow);
        mDBService.UpdateRecord(mDT, "usp_tblServer_UPDATE");
        return mDTO;
    }

    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public DTOServer ServerDeleteRecord(DTOServer mDTO)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@ServerID";
        mParam.ParameterValue = mDTO.ServerID;
        mParams.Add(mParam);
        mDBService.DeleteRecord("usp_tblServer_DELETE", mParams);
        return mDTO;
    }

    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public Boolean ServerIsValid(DTOServer mDTO, out string mValidationResponse)
    {
        //please add your validation rules here. - Lazy Dog 

        if (mDTO.ServerName == null)
        {
            mValidationResponse = "ServerName cannnot be null.";
            return false;
        }

        if (mDTO.ServerURL == null)
        {
            mValidationResponse = "ServerURL cannnot be null.";
            return false;
        }

        mValidationResponse = "Ok";
        return true;
    }

    #endregion


    #region ************************tblSYSConfig CRUDS ******************************************

    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public DTOSYSConfigList SYSConfigList()
    {
        DTOSYSConfigList mDTOSYSConfigList = new DTOSYSConfigList();
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSConfig_List");
        foreach (DataRow mRow in mDT.Rows)
        {
            DTOSYSConfig mDTOSYSConfig = new DTOSYSConfig();
            mDTOSYSConfig.SYSConfigID = Int64.Parse(mRow["SYSConfigID"].ToString());
            mDTOSYSConfig.ConfigKey = mRow["ConfigKey"].ToString();
            mDTOSYSConfig.ConfigValue = mRow["ConfigValue"].ToString();
            mDTOSYSConfig.ConfigDescription = mRow["ConfigDescription"].ToString();
            mDTOSYSConfigList.Add(mDTOSYSConfig);
        }

        return mDTOSYSConfigList;
    }

    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public DTOSYSConfig SYSConfigListByID(int mRecNo)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@SYSConfigID";
        mParam.ParameterValue = mRecNo;
        mParams.Add(mParam);
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSConfig_ListByID", mParams);
        DTOSYSConfig mDTOSYSConfig = new DTOSYSConfig();
        foreach (DataRow mRow in mDT.Rows)//not required but...
        {
            mDTOSYSConfig.SYSConfigID = Int64.Parse(mRow["SYSConfigID"].ToString());
            mDTOSYSConfig.ConfigKey = mRow["ConfigKey"].ToString();
            mDTOSYSConfig.ConfigValue = mRow["ConfigValue"].ToString();
            mDTOSYSConfig.ConfigDescription = mRow["ConfigDescription"].ToString();
        }

        return mDTOSYSConfig;
    }

    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public DTOSYSConfig SYSConfigListByKey(string key)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@ConfigKey";
        mParam.ParameterValue = key;
        mParams.Add(mParam);
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSYSConfig_ListByConfigKey", mParams);
        DTOSYSConfig mDTOSYSConfig = new DTOSYSConfig();
        foreach (DataRow mRow in mDT.Rows)//not required but...
        {
            mDTOSYSConfig.SYSConfigID = Int64.Parse(mRow["SYSConfigID"].ToString());
            mDTOSYSConfig.ConfigKey = mRow["ConfigKey"].ToString();
            mDTOSYSConfig.ConfigValue = mRow["ConfigValue"].ToString();
            mDTOSYSConfig.ConfigDescription = mRow["ConfigDescription"].ToString();
        }

        return mDTOSYSConfig;
    }

    public string SalesOrgConfigValueByKey(string salesorgid, string key)
    {
        string ConfigValue = "";
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@SalesOrgID";
        mParam.ParameterValue = salesorgid;
        mParams.Add(mParam);

        mParam = new DTODBParameter();
        mParam.ParameterName = "@ConfigKey";
        mParam.ParameterValue = key;
        mParams.Add(mParam);

        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblSalesOrgConfig_Select", mParams);

        if (mDT.Rows.Count > 0)
        {
            ConfigValue = mDT.Rows[0]["ConfigValue"].ToString();
        }

        return ConfigValue;
    }


    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public DTOSYSConfig SYSConfigCreateRecord(DTOSYSConfig mDTO)
    {
        DataTable mDT = mDBService.GenerateCreateTable("tblSYSConfig");
        DataRow mRow = mDT.NewRow();
        mRow["ConfigKey"] = mDTO.ConfigKey;
        mRow["ConfigValue"] = mDTO.ConfigValue;
        mRow["ConfigDescription"] = mDTO.ConfigDescription;
        mDT.Rows.Add(mRow);
        Object mRetval = mDBService.CreateRecord(mDT, "usp_tblSYSConfig_INSERT");
        Int64 ObjectID = Int64.Parse(mRetval.ToString());
        mDTO.SYSConfigID = ObjectID;
        return mDTO;
    }

    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public DTOSYSConfig SYSConfigUpdateRecord(DTOSYSConfig mDTO)
    {
        DataTable mDT = mDBService.GenerateUpdateTable("tblSYSConfig");
        DataRow mRow = mDT.NewRow();
        mRow["SYSConfigID"] = mDTO.SYSConfigID;
        mRow["ConfigKey"] = mDTO.ConfigKey;
        mRow["ConfigValue"] = mDTO.ConfigValue;
        mRow["ConfigDescription"] = mDTO.ConfigDescription;
        mDT.Rows.Add(mRow);
        mDBService.UpdateRecord(mDT, "usp_tblSYSConfig_UPDATE");
        return mDTO;
    }

    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public DTOSYSConfig SYSConfigDeleteRecord(DTOSYSConfig mDTO)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@SYSConfigID";
        mParam.ParameterValue = mDTO.SYSConfigID;
        mParams.Add(mParam);
        mDBService.DeleteRecord("usp_tblSYSConfig_DELETE", mParams);
        return mDTO;
    }

    //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
    public Boolean SYSConfigIsValid(DTOSYSConfig mDTO, out string mValidationResponse)
    {
        //please add your validation rules here. - Lazy Dog 

        if (mDTO.ConfigKey == null)
        {
            mValidationResponse = "ConfigKey cannnot be null.";
            return false;
        }

        if (mDTO.ConfigValue == null)
        {
            mValidationResponse = "ConfigValue cannnot be null.";
            return false;
        }

        if (mDTO.ConfigDescription == null)
        {
            mValidationResponse = "ConfigDescription cannnot be null.";
            return false;
        }

        mValidationResponse = "Ok";
        return true;
    }

    #endregion


} //end class
