using System;
using CodeEndeavors.ServiceHost.Common.Services;
using CodeEndeavors.ServiceHost.Common.Services.LoggingServices;
using DomainObjects = $saferootprojectname$.Shared.DomainObjects;

namespace $saferootprojectname$.Client.Http
{
    public class Sales : CodeEndeavors.ServiceHost.Common.Services.BaseClientHttpService, ISalesService
    {
        public Sales(string HttpServiceUrl, int RequestTimeout, string RestfulServerExtension)
            : base("Sales", HttpServiceUrl, RequestTimeout, RestfulServerExtension, Log.ConfigFileName)
        {
        }

        public void SetAquireUserIdDelegate(CodeEndeavors.ServiceHost.Common.Services.BaseClientHttpService.AquireUserId func)
        {
            base.AquireUserIdDelegate = func;
        }

        public ServiceResult<DomainObjects.Customer> GetCustomer(int id)
        {
            return base.GetHttpRequestObject<ServiceResult<DomainObjects.Customer>>(base.RequestUrl("GetCustomer", id.ToString()), false, true);
        }

    }
}
