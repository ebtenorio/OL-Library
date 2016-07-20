using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.IDataContracts;
using OrderLinc.DTOs;


public class LogDA : ILogDA
{
    DatabaseService mDBService;

    public LogDA(DatabaseService dbService)
    {

        mDBService = dbService;
    }

    #region ************************tblLog CRUDS ******************************************

    //CASE Generated Code 5/7/2014 9:34:15 PM Lazy Dog 3.3.1.0
    public DTOLogList LogList()
    {
        DTOLogList mDTOLogList = new DTOLogList();
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblLog_List");
        foreach (DataRow mRow in mDT.Rows)
        {
            DTOLog mDTOLog = new DTOLog();
            mDTOLog.LogID = Int64.Parse(mRow["LogID"].ToString());
            mDTOLog.Source = mRow["Source"].ToString();
            mDTOLog.Description = mRow["Description"].ToString();
            if (mRow["DateCreated"] != System.DBNull.Value)
            {
                mDTOLog.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
            }
            mDTOLog.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
            mDTOLogList.Add(mDTOLog);
        }

        return mDTOLogList;
    }

    //CASE Generated Code 5/7/2014 9:34:15 PM Lazy Dog 3.3.1.0
    public DTOLog LogListByID(int mRecNo)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@LogID";
        mParam.ParameterValue = mRecNo;
        mParams.Add(mParam);
        DataTable mDT = mDBService.GetDataTable(SQLCommandTypes.StoredProcedure, "usp_tblLog_ListByID", mParams);
        DTOLog mDTOLog = new DTOLog();
        foreach (DataRow mRow in mDT.Rows)//not required but...
        {
            mDTOLog.LogID = Int64.Parse(mRow["LogID"].ToString());
            mDTOLog.Source = mRow["Source"].ToString();
            mDTOLog.Description = mRow["Description"].ToString();
            if (mRow["DateCreated"] != System.DBNull.Value)
            {
                mDTOLog.DateCreated = DateTime.Parse(mRow["DateCreated"].ToString());
            }
            mDTOLog.CreatedByUserID = long.Parse(mRow["CreatedByUserID"].ToString());
        }

        return mDTOLog;
    }

    //CASE Generated Code 5/7/2014 9:34:16 PM Lazy Dog 3.3.1.0
    public DTOLog LogCreateRecord(DTOLog mDTO)
    {
        DataTable mDT = mDBService.GenerateCreateTable("tblLog");
        DataRow mRow = mDT.NewRow();
        mRow["Source"] = mDTO.Source;
        mRow["Description"] = mDTO.Description;
        mRow["DateCreated"] = mDTO.DateCreated;
        mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
        mDT.Rows.Add(mRow);
        Object mRetval = mDBService.CreateRecord(mDT, "usp_tblLog_INSERT");
        Int64 ObjectID = Int64.Parse(mRetval.ToString());
        mDTO.LogID = ObjectID;
        return mDTO;
    }

    //CASE Generated Code 5/7/2014 9:34:16 PM Lazy Dog 3.3.1.0
    public DTOLog LogUpdateRecord(DTOLog mDTO)
    {
        DataTable mDT = mDBService.GenerateUpdateTable("tblLog");
        DataRow mRow = mDT.NewRow();
        mRow["LogID"] = mDTO.LogID;
        mRow["Source"] = mDTO.Source;
        mRow["Description"] = mDTO.Description;
        mRow["DateCreated"] = mDTO.DateCreated;
        mRow["CreatedByUserID"] = mDTO.CreatedByUserID;
        mDT.Rows.Add(mRow);
        mDBService.UpdateRecord(mDT, "usp_tblLog_UPDATE");
        return mDTO;
    }

    //CASE Generated Code 5/7/2014 9:34:16 PM Lazy Dog 3.3.1.0
    public DTOLog LogDeleteRecord(DTOLog mDTO)
    {
        DTODBParameters mParams = new DTODBParameters();
        DTODBParameter mParam = new DTODBParameter();
        mParam.ParameterName = "@LogID";
        mParam.ParameterValue = mDTO.LogID;
        mParams.Add(mParam);
        mDBService.DeleteRecord("usp_tblLog_DELETE", mParams);
        return mDTO;
    }

    //CASE Generated Code 5/7/2014 9:34:16 PM Lazy Dog 3.3.1.0
    public Boolean LogIsValid(DTOLog mDTO, out string mValidationResponse)
    {
        //please add your validation rules here. - Lazy Dog 

        if (mDTO.Source == null)
        {
            mValidationResponse = "Source cannnot be null.";
            return false;
        }

        if (mDTO.Description == null)
        {
            mValidationResponse = "Description cannnot be null.";
            return false;
        }

        if (mDTO.DateCreated == null)
        {
            mValidationResponse = "DateCreated cannnot be null.";
            return false;
        }

        if (mDTO.CreatedByUserID == 0)
        {
            mValidationResponse = "CreatedByUserID cannnot be 0.";
            return false;
        }

        mValidationResponse = "Ok";
        return true;
    }

    #endregion


} //end class
