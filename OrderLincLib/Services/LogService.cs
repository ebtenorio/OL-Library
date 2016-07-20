using System;
using System.Data;
//add your DTO namespace here
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.IDataContracts;
using OrderLinc.DTOs;


public class LogService : ILogService
{
    private ILogDA _logDA;

    public LogService(ILogDA logDA)
    {
        _logDA = logDA;
    }

    #region ************************tblLog CRUDS ******************************************

    //CASE Generated Code 5/7/2014 9:34:15 PM Lazy Dog 3.3.1.0
    public DTOLogList LogList()
    {

        return _logDA.LogList();
    }

    //CASE Generated Code 5/7/2014 9:34:15 PM Lazy Dog 3.3.1.0
    public DTOLog LogListByID(int mRecNo)
    {
        return _logDA.LogListByID(mRecNo);
    }

    //CASE Generated Code 5/7/2014 9:34:16 PM Lazy Dog 3.3.1.0
    public DTOLog LogSave(DTOLog mDTO)
    {
        try
        {
            if (mDTO.LogID == 0)
                return _logDA.LogCreateRecord(mDTO);
            else
                return _logDA.LogUpdateRecord(mDTO);
        }
        catch { return null; } // just ignore
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



    public DTOLog LogSave(string source, string description, long userId)
    {
        DTOLog log = new DTOLog()
        {
            Source = source,
            Description = description ,
            DateCreated = DateTime.Now,
            CreatedByUserID = userId
        };
        return LogSave(log);
    }
} //end class
