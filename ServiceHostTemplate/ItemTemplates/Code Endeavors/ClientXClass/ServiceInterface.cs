using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects = $solutionname$.Shared.DomainObjects;

namespace $defaultnamespace$
{
    public interface $safeitemname$
    {
        //ServiceResult<DomainObjects.Customer> CustomerGet(int id);
        void SetAquireUserIdDelegate(Func<string> func);
    }
}
