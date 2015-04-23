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

        public void SetAquireUserIdDelegate(BaseClientHttpService.AquireUserId func)
        {
            _service.SetAquireUserIdDelegate(func);
        }

        public ClientCommandResult<DomainObjects.Customer> GetCustomer(int id)
        {
            return Helpers.ExecuteClientResult<DomainObjects.Customer>(result =>
            {
                result.ReportResult(_service.GetCustomer(id), true);
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
