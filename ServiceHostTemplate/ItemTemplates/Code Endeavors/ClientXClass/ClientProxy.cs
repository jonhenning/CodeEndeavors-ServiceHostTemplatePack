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
            _service = new Stubs.$safeitemname$();
        }

        public $safeitemname$(string httpServiceUrl, int requestTimeout, string restfulServerExtension) : base(httpServiceUrl, requestTimeout, restfulServerExtension)
        {
            _service = new Http.$safeitemname$(httpServiceUrl, requestTimeout, restfulServerExtension);
        }
        public $safeitemname$(string httpServiceUrl, int requestTimeout, string restfulServerExtension, string httpUser, string httpPassword, string authenticationType) : base(httpServiceUrl, requestTimeout, restfulServerExtension, httpUser, httpPassword, authenticationType)
        {
            _service = new Http.$safeitemname$(httpServiceUrl, requestTimeout, restfulServerExtension, httpUser, httpPassword, authenticationType);
        }

        public override void SetAquireUserIdDelegate(Func<string> func)
        {
            _service.SetAquireUserIdDelegate(func);
        }

        //public ClientCommandResult<DomainObjects.Customer> CustomerGet(int id)
        //{
        //    return ClientCommandResult<DomainObjects.Customer>.Execute(() =>
        //    {
        //        return _service.CustomerGet(id);
        //    });
        //}

    }
}
