using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using DomainObjects = $solutionname$.Shared.DomainObjects;

namespace $defaultnamespace$.Http
{
    public class $safeitemname$ : CodeEndeavors.ServiceHost.Common.Services.BaseClientHttpService, I$safeitemname$Service
    {
        public $safeitemname$(string httpServiceUrl, int requestTimeout, string restfulServerExtension)
            : base("$safeitemname$", httpServiceUrl, requestTimeout, restfulServerExtension)
        {
        }

        public $safeitemname$(string httpServiceUrl, int requestTimeout, string restfulServerExtension, string httpUser, string httpPassword, string authenticationType)
            : base("$safeitemname$", httpServiceUrl, requestTimeout, restfulServerExtension, httpUser, httpPassword, authenticationType)
        {
        }

        //public ServiceResult<DomainObjects.Customer> CustomerGet(int id)
        //{
        //    return base.GetHttpRequestObject<ServiceResult<DomainObjects.Customer>>(base.RequestUrl("CustomerGet", id.ToString()));
        //}

        public void SetAquireUserIdDelegate(Func<string> func)
        {
            base.AquireUserIdDelegate = func;
        }

    }
}
