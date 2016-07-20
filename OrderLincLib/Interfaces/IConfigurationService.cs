using System;
using OrderLinc.DTOs;

namespace OrderLinc.IDataContracts
{
    public interface IConfigurationService
    {
        DTOServer ServerSaveRecord(DTOServer mDTO);
        void ServerDeleteRecord(long mRecNo);
        bool ServerIsValid(DTOServer mDTO, out string mValidationResponse);
        DTOServerList ServerList();
        DTOServer ServerListByID(long mRecNo);
        DTOSYSConfig SYSConfigSaveRecord(DTOSYSConfig mDTO);
        
        bool SYSConfigIsValid(DTOSYSConfig mDTO, out string mValidationResponse);
        DTOSYSConfigList SYSConfigList();
        DTOSYSConfig SYSConfigListByID(int mRecNo);
        DTOSYSConfig SYSConfigListByKey(string key);
        string SalesOrgConfigValueByKey(string salesorgid, string key);
        DTOMailConfig SYSConfigMail();
        DTODataLoadConfig SYSConfigDataLoad();
    }
}
