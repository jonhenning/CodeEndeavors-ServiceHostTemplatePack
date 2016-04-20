using CodeEndeavors.Extensions;
using CodeEndeavors.ServiceHost.Common.Client;
using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects = $saferootprojectname$.Shared.DomainObjects;
using Logger = CodeEndeavors.ServiceHost.Common.Services.Logging;
using CodeEndeavors.ServiceHost.Common.Client;

namespace $saferootprojectname$.Client
{
    public class Sales : BaseClient
    {
        private ISalesService _service;

        public Sales() : base()
        {
            _service = new Stubs.Sales();
        }

        public Sales(string httpServiceUrl, int requestTimeout, string restfulServerExtension) : base(httpServiceUrl, requestTimeout, restfulServerExtension)
        {
            _service = new Http.Sales(httpServiceUrl, requestTimeout, restfulServerExtension);
        }
        public Sales(string httpServiceUrl, int requestTimeout, string restfulServerExtension, string httpUser, string httpPassword, string authenticationType) : base(httpServiceUrl, requestTimeout, restfulServerExtension, httpUser, httpPassword, authenticationType)
        {
            _service = new Http.Sales(httpServiceUrl, requestTimeout, restfulServerExtension, httpUser, httpPassword, authenticationType);
        }

        public override void SetAquireUserIdDelegate(Func<string> func)
        {
            _service.SetAquireUserIdDelegate(func);
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

    }
}
