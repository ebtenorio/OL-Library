using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrderLinc.IDataContracts;
using PL.PersistenceServices;
using PL.PersistenceServices.DTOS;
using PL.PersistenceServices.Enumerations;
using OrderLinc.DataAccess;
using OrderLinc.Services;

namespace OrderLinc
{
    public class OrderLincServices
    {

        IAccountDA _accountDA;
        IAddressDA _addressDA;
        ICustomerDA _customerDA;
        IOrderDA _orderDA;
        IProductDA _productDA;
        IProviderDA _providerDA;
        ILogDA _logDA;
      
        DatabaseService _dbService;
        DTOConnectionConfiguration _dbConfig;
        private IConfigurationDA _configurationDA;
        private IContactDA _contactDA;

        public OrderLincServices(string dbServer, string dbName, bool IsWindowsAuthentication, string dbUsername, string dbPassword)
        {
            _dbConfig = new DTOConnectionConfiguration()
            {
                ServerName = dbServer,
                ServerUri = dbServer,
                DatabaseName = dbName,
                AuthenticationType = IsWindowsAuthentication ? DatabaseAuthenticationTypes.Integrated : DatabaseAuthenticationTypes.ServerAuthentication,
                ConnectionID = 0,
                DatabaseType = DatabaseTypes.MicrosoftSQL,
                UserName = dbUsername,
                Password = dbPassword,
            };

            _dbService = new DatabaseService(_dbConfig);

            _accountDA = new AccountDA(_dbService);
            _addressDA = new AddressDA(_dbService);
            _customerDA = new CustomerDA(_dbService);
            _orderDA = new OrderDA(_dbService);
            _productDA = new ProductDA(_dbService);
            _providerDA = new ProviderDA(_dbService);
            _logDA = new LogDA(_dbService);
            _configurationDA = new ConfigurationDA(_dbService);
            _contactDA = new ContactDA(_dbService);
            ReportService = new ReportService(_dbService);
            this.ContactService = new ContactService(_contactDA, LogService);
            this.AddressService = new AddressService(_addressDA, LogService);

            this.LogService = new LogService(_logDA);
            this.CatalogService = new CatalogService(_productDA, this.LogService);
            this.OrderService = new OrderService(_orderDA, this.LogService);
            this.CustomerService = new CustomerService(_customerDA, _providerDA, this.ContactService, this.AddressService, this.LogService);
            this.ProviderService = new ProviderService(_providerDA, this.LogService);
            this.ConfigurationService = new ConfigurationService(_configurationDA, LogService);
          
            this.AccountService = new AccountService(_accountDA, this.ContactService, this.AddressService, ConfigurationService, CustomerService, this.LogService);
          
        }

        public ReportService ReportService { get; private set; }

        public IProviderService ProviderService { get; private set; }

        public ICatalogService CatalogService { get; private set; }

        public IAccountService AccountService { get; private set; }

        public IOrderService OrderService { get; private set; }

        public ICustomerService CustomerService { get; private set; }

        public ILogService LogService { get; private set; }

        public IConfigurationService ConfigurationService { get; private set; }
        //  public IAccountService AccountService { get; private set; }

        //IContactService ContactService { get; set; }
        public IContactService ContactService { get; set; }

        public IAddressService AddressService { get; private set; }
    }
}
