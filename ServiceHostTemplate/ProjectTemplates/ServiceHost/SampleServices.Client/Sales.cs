using CodeEndeavors.Extensions;
using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects = $saferootprojectname$.Shared.DomainObjects;
using Logger = CodeEndeavors.ServiceHost.Common.Services.Logging;

namespace $saferootprojectname$.Client
{
    public class Sales
    {
        private ISalesService _service;

        public Sales()
        {
            Helpers.HandleAssemblyResolve();
            _service = new Stubs.Sales();
        }

        public Sales(string httpServiceUrl, int requestTimeout, string restfulServerExtension)
        {
            Helpers.HandleAssemblyResolve();
            _service = new Http.Sales(httpServiceUrl, requestTimeout, restfulServerExtension);
        }
        public Sales(string httpServiceUrl, int requestTimeout, string restfulServerExtension, string httpUser, string httpPassword, string authenticationType)
        {
            Helpers.HandleAssemblyResolve();
            _service = new Http.Sales(httpServiceUrl, requestTimeout, restfulServerExtension, httpUser, httpPassword, authenticationType);
        }

        public ClientCommandResult<DomainObjects.Customer> CustomerGet(int id)
        {
            return ClientCommandResult<DomainObjects.Customer>.Execute(result =>
            {
                result.ReportResult(_service.CustomerGet(id), true);
            });
        }

        public ClientCommandResult<bool> CustomerSave(DomainObjects.Customer customer, int id)
        {
            return ClientCommandResult<bool>.Execute(result =>
            {
                result.ReportResult(_service.CustomerSave(customer, id), true);
            });
        }

        #region Common Client Methods
        public void SetAquireUserIdDelegate(Func<string> func)
        {
            _service.SetAquireUserIdDelegate(func);
        }

        public void ConfigureLogging(string logLevel, Action<string, string> onLoggingMessage)
        {
            Logger.LogLevel = logLevel.ToType<Logger.LoggingLevel>();
            Logger.OnLoggingMessage += (Logger.LoggingLevel level, string message) =>
            {
                if (onLoggingMessage != null)
                    onLoggingMessage(level.ToString(), message);
            };
        }

        public static void Register(string url, int requestTimeout)
        {
            ServiceLocator.Register<Client.Sales>(url, requestTimeout);
        }
        public static void Register(string url, int requestTimeout, string httpUser, string httpPassword, string authenticationType)
        {
            ServiceLocator.Register<Client.Sales>(url, requestTimeout, httpUser, httpPassword, authenticationType.ToType<AuthenticationType>());
        }

        public static Client.Sales Resolve()
        {
            return ServiceLocator.Resolve<Client.Sales>();
        }

        public static T ExecuteClient<T>(Func<ClientCommandResult<T>> codeFunc) where T : new()
        {
            ClientCommandResult<T> clientCommandResult = codeFunc();
            if (clientCommandResult.Success)
            {
                return clientCommandResult.Data;
            }
            throw new Exception(clientCommandResult.ToString());
        }
        #endregion



    }
}
