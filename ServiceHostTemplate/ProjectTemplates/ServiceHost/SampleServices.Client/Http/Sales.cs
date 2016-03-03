using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using DomainObjects = $saferootprojectname$.Shared.DomainObjects;

namespace $saferootprojectname$.Client.Http
{
    public class Sales : CodeEndeavors.ServiceHost.Common.Services.BaseClientHttpService, ISalesService
    {
        public Sales(string httpServiceUrl, int requestTimeout, string restfulServerExtension)
            : base("Sales", httpServiceUrl, requestTimeout, restfulServerExtension)
        {
        }

        public Sales(string httpServiceUrl, int requestTimeout, string restfulServerExtension, string httpUser, string httpPassword, AuthenticationType authenticationType)
            : base("Sales", httpServiceUrl, requestTimeout, restfulServerExtension, httpUser, httpPassword, authenticationType)
        {
        }

        public void SetAquireUserIdDelegate(Func<string> func)
        {
            base.AquireUserIdDelegate = func;
        }

        public ServiceResult<DomainObjects.Customer> CustomerGet(int id)
        {
            return base.GetHttpRequestObject<ServiceResult<DomainObjects.Customer>>(base.RequestUrl("CustomerGet", id.ToString()));
        }

        public ServiceResult<bool> CustomerSave(DomainObjects.Customer customer, int id)
        {
            return base.GetHttpRequestObject<ServiceResult<bool>>(base.RequestUrl("CustomerSave"), customer);
        }

    }
}
