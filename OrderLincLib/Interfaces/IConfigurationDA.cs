using System;
using OrderLinc.DTOs;

namespace OrderLinc.IDataContracts
{
    public interface IConfigurationDA
    {
        DTOServer ServerCreateRecord(DTOServer mDTO);
        DTOServer ServerDeleteRecord(DTOServer mDTO);
        DTOServerList ServerList();
        DTOServer ServerListByID(long mRecNo);
        DTOServer ServerUpdateRecord(DTOServer mDTO);

        DTOSYSConfig SYSConfigListByKey(string key);
        DTOSYSConfig SYSConfigCreateRecord(DTOSYSConfig mDTO);
        DTOSYSConfig SYSConfigDeleteRecord(DTOSYSConfig mDTO);
        DTOSYSConfigList SYSConfigList();
        DTOSYSConfig SYSConfigListByID(int mRecNo);
        DTOSYSConfig SYSConfigUpdateRecord(DTOSYSConfig mDTO);
        string SalesOrgConfigValueByKey(string salesorgid, string key);
    }
}
