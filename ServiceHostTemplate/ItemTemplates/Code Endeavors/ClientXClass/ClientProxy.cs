using CodeEndeavors.Extensions;
using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects = $solutionname$.Shared.DomainObjects;
using Logger = CodeEndeavors.ServiceHost.Common.Services.Logging;

namespace $defaultnamespace$
{
    public class $safeitemname$
    {
        private I$safeitemname$Service _service;

        public $safeitemname$()
        {
            Helpers.HandleAssemblyResolve();
            _service = new Stubs.$safeitemname$();
        }

        public $safeitemname$(string httpServiceUrl, int requestTimeout, string restfulServerExtension)
        {
            Helpers.HandleAssemblyResolve();
            _service = new Http.$safeitemname$(httpServiceUrl, requestTimeout, restfulServerExtension);
        }
        public $safeitemname$(string httpServiceUrl, int requestTimeout, string restfulServerExtension, string httpUser, string httpPassword, string authenticationType)
        {
            Helpers.HandleAssemblyResolve();
            _service = new Http.$safeitemname$(httpServiceUrl, requestTimeout, restfulServerExtension, httpUser, httpPassword, authenticationType);
        }

        //public ClientCommandResult<DomainObjects.Customer> CustomerGet(int id)
        //{
        //    return ClientCommandResult<DomainObjects.Customer>.Execute(result =>
        //    {
        //        result.ReportResult(_service.CustomerGet(id), true);
        //    });
        //}

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
            ServiceLocator.Register<Client.$safeitemname$>(url, requestTimeout);
        }
        public static void Register(string url, int requestTimeout, string httpUser, string httpPassword, string authenticationType)
        {
            ServiceLocator.Register<Client.$safeitemname$>(url, requestTimeout, httpUser, httpPassword, authenticationType.ToType<AuthenticationType>());
        }

        public static Client.$safeitemname$ Resolve()
        {
            return ServiceLocator.Resolve<Client.$safeitemname$>();
        }
        #endregion

    }
}
