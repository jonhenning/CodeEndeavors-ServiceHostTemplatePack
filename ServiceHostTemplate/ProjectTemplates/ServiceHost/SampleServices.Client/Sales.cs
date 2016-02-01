using CodeEndeavors.ServiceHost.Common.Services;
using CodeEndeavors.ServiceHost.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects = $saferootprojectname$.Shared.DomainObjects;

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
        public Sales(string httpServiceUrl, int requestTimeout, string restfulServerExtension, string httpUser, string httpPassword, AuthenticationType authenticationType)
        {
            Helpers.HandleAssemblyResolve();
            _service = new Http.Sales(httpServiceUrl, requestTimeout, restfulServerExtension, httpUser, httpPassword, authenticationType);
        }

        public void SetAquireUserIdDelegate(Func<string> func)
        {
            _service.SetAquireUserIdDelegate(func);
        }

        public ClientCommandResult<DomainObjects.Customer> CustomerGet(int id)
        {
            return Helpers.ExecuteClientResult<DomainObjects.Customer>(result =>
            {
                result.ReportResult(_service.CustomerGet(id), true);
            });
        }

        public ClientCommandResult<bool> CustomerSave(DomainObjects.Customer customer, int id)
        {
            return Helpers.ExecuteClientResult<bool>(result =>
            {
                result.ReportResult(_service.CustomerSave(customer, id), true);
            });
        }

        public static void Register(string url, int requestTimeout)
        {
            ServiceLocator.Register<Client.Sales>(url, requestTimeout);
        }

        public static Client.Sales Resolve()
        {
            return ServiceLocator.Resolve<Client.Sales>();
        }



    }
}
