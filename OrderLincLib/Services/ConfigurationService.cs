using System;
using System.Data;
using System.Linq;
using System.Collections;
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DTOs;
using OrderLinc.IDataContracts;


namespace OrderLinc.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private IConfigurationDA _configurationDA;
        private ILogService _logService;

        public ConfigurationService(IConfigurationDA configurationDA, ILogService logService)
        {
            _configurationDA = configurationDA;
            _logService = logService;
        }

        #region ************************tblServer CRUDS ******************************************

        //CASE Generated Code 5/7/2014 11:59:09 PM Lazy Dog 3.3.1.0
        public DTOServerList ServerList()
        {
            return _configurationDA.ServerList();
        }

        public DTOSYSConfig SYSConfigListByKey(string key)
        {
            return _configurationDA.SYSConfigListByKey(key);
        }

        //CASE Generated Code 5/7/2014 11:59:09 PM Lazy Dog 3.3.1.0
        public DTOServer ServerListByID(long mRecNo)
        {
            return _configurationDA.ServerListByID(mRecNo);
        }

        //CASE Generated Code 5/7/2014 11:59:09 PM Lazy Dog 3.3.1.0
        public DTOServer ServerSaveRecord(DTOServer mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("Server");

                if (mDTO.ServerID == 0)
                    return _configurationDA.ServerCreateRecord(mDTO);
                else
                    return _configurationDA.ServerUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
        public void ServerDeleteRecord(long mRecNo)
        {
            _configurationDA.ServerDeleteRecord(new DTOServer() { ServerID = mRecNo });
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




        public DTOSYSConfig SYSConfigSaveRecord(DTOSYSConfig mDTO)
        {
            try
            {
                if (mDTO == null) throw new ArgumentNullException("SYSconfig");

                if (mDTO.SYSConfigID == 0)
                    return _configurationDA.SYSConfigCreateRecord(mDTO);
                else
                    return _configurationDA.SYSConfigUpdateRecord(mDTO);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
        public DTOSYSConfigList SYSConfigList()
        {
            return _configurationDA.SYSConfigList();
        }

        //CASE Generated Code 5/7/2014 11:59:10 PM Lazy Dog 3.3.1.0
        public DTOSYSConfig SYSConfigListByID(int mRecNo)
        {
            return _configurationDA.SYSConfigListByID(mRecNo);
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

        public DTOMailConfig SYSConfigMail()
        {
            try
            {

                DTOMailConfig mailConfig = new DTOMailConfig();

                DTOSYSConfigList configList = SYSConfigList();

                DTOSYSConfig c = configList.Where(p => p.ConfigKey == "MailHost").FirstOrDefault();
                if (c != null)
                    mailConfig.HostName = c.ConfigValue;

                c = configList.Where(p => p.ConfigKey == "MailPort").FirstOrDefault();
                if (c != null)
                {
                    int port = 0;

                    int.TryParse(c.ConfigValue, out port);
                    mailConfig.Port = port;

                }
                c = configList.Where(p => p.ConfigKey == "MailSender").FirstOrDefault();
                if (c != null)
                    mailConfig.SenderEmail = c.ConfigValue;

                c = configList.Where(p => p.ConfigKey == "MailPassword").FirstOrDefault();
                if (c != null)
                    mailConfig.Password = c.ConfigValue;

                c = configList.Where(p => p.ConfigKey == "MailUseDefaultCredential").FirstOrDefault();
                if (c != null)
                    mailConfig.UseDefaultCredential = c.ConfigValue == "1" ? true : false;

                c = configList.Where(p => p.ConfigKey == "MailUserName").FirstOrDefault();
                if (c != null)
                    mailConfig.Username = c.ConfigValue;
                c = configList.Where(p => p.ConfigKey == "MailCC").FirstOrDefault();
                if (c != null)
                    mailConfig.CCMail = c.ConfigValue;

                configList = null;

                return mailConfig;
            }
            catch (Exception ex)
            {
                _logService.LogSave("Configuration", "Error reading configuration : " + ex.Message, 0);
                throw;
            }
        }

        public DTODataLoadConfig SYSConfigDataLoad()
        {
            try
            {

                DTODataLoadConfig dataLoadConfig = new DTODataLoadConfig();

                DTOSYSConfigList configList = SYSConfigList();

                DTOSYSConfig c = configList.Where(p => p.ConfigKey == "DataLoadSendLogToSysAdmin").FirstOrDefault();
                if (c != null)
                    dataLoadConfig.SendLogToSystemAdmin = c.ConfigValue == "1" ? true :false;

                c = configList.Where(p => p.ConfigKey == "DataLoadSendLogToOfficeAdmin").FirstOrDefault();
                if (c != null)
                {
                    dataLoadConfig.SendLogToOfficeAdmin= c.ConfigValue == "1" ? true : false;
                }

                c = configList.Where(p => p.ConfigKey == "DataLoadTime").FirstOrDefault();
                if (c != null)
                    dataLoadConfig.DataLoadTime = int.Parse(c.ConfigValue);

                c = configList.Where(p => p.ConfigKey == "DataLoadPath").FirstOrDefault();
                if (c != null)
                    dataLoadConfig.DataLoadPath = c.ConfigValue;

                c = configList.Where(p => p.ConfigKey == "DataLoadInterval").FirstOrDefault();
                if (c != null)
                    dataLoadConfig.Interval = int.Parse(c.ConfigValue);

                c = configList.Where(p => p.ConfigKey == "EmailFooter").FirstOrDefault();
                if (c != null)
                    dataLoadConfig.Footer = c.ConfigValue;


                c = configList.Where(p => p.ConfigKey == "DataLoadSubject").FirstOrDefault();
                if (c != null)
                    dataLoadConfig.Subject = c.ConfigValue;

                configList = null;

                return dataLoadConfig;
            }
            catch (Exception ex)
            {
                _logService.LogSave("Configuration", "Error reading configuration : " + ex.Message, 0);
                throw;
            }
        }

        public string SalesOrgConfigValueByKey(string salesorgid, string key)
        {
            return _configurationDA.SalesOrgConfigValueByKey(salesorgid, key);           
        }


    } //end class
}