using System;
using System.Collections.Generic;
using CodeEndeavors.ServiceHost.Common.Services;

namespace $saferootprojectname$.Client.Stubs
{
    public class Sales : ISalesService
    {
        public ServiceResult<Shared.DomainObjects.Customer> CustomerGet(int id)
        {
            var customer = new Shared.DomainObjects.Customer()
            {
                Id = id,
                FirstName = "John",
                LastName = "Doe"
            };
            var ret = new ServiceResult<Shared.DomainObjects.Customer>();
            ret.ReportResult(customer, true);
            return ret;
        }

        public ServiceResult<bool> CustomerSave(Shared.DomainObjects.Customer customer, int id)
        {
            throw new System.NotImplementedException();
        }

        public void SetAquireUserIdDelegate(Func<string> func)
        {
            
        }
    }
}
