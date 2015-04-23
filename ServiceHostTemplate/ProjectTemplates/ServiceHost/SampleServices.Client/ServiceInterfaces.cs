using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects = $saferootprojectname$.Shared.DomainObjects;

namespace $saferootprojectname$.Client
{
    public interface ISalesService
    {
        ServiceResult<DomainObjects.Customer> GetCustomer(int id);
        void SetAquireUserIdDelegate(BaseClientHttpService.AquireUserId func);
    }
}
