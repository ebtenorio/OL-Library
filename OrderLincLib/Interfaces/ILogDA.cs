using System;
using OrderLinc.DTOs;

namespace OrderLinc.IDataContracts
{
    public interface ILogDA
    {
        DTOLog LogCreateRecord(DTOLog mDTO);
        DTOLog LogDeleteRecord(DTOLog mDTO);
        DTOLogList LogList();
        DTOLog LogListByID(int mRecNo);
        DTOLog LogUpdateRecord(DTOLog mDTO);
    }
}
