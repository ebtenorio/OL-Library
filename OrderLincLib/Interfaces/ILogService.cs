using System;
using OrderLinc.DTOs;
namespace OrderLinc.IDataContracts
{
    public interface ILogService
    {

        DTOLog LogSave(string source, string description, long  userId);

        DTOLog LogSave(DTOLog mDTO);
        bool LogIsValid(DTOLog mDTO, out string mValidationResponse);
        DTOLog LogListByID(int mRecNo);
        DTOLogList LogList();
    }
}
