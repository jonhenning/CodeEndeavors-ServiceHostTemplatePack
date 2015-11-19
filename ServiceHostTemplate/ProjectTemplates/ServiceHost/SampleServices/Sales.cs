using CodeEndeavors.ServiceHost;
using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects = $saferootprojectname$.Shared.DomainObjects;

namespace $saferootprojectname$
{
    public class Sales : BaseService
    {
        public Sales()
        {
            base.Configure(@"bin\$saferootprojectname$.Log4net.config", "SampleServicesLogger");
            //_myConnection = GetConnectionString("MyConnectionString", "");
        }

        public ServiceResult<DomainObjects.Customer> CustomerGet(int id, string userId)
        {
            return this.ExecuteServiceResult<DomainObjects.Customer>(result =>
            {
                //todo: database code here!
                var customer = new DomainObjects.Customer()
                {
                    Id = id,
                    FirstName = "Service First Name",
                    LastName = "Service Last Name"
                };
                result.ReportResult(customer, true);
            });
        }

        public ServiceResult<bool> CustomerSave(DomainObjects.Customer customer, string userId)
        {
            return this.ExecuteServiceResult<bool>(result =>
            {
                //todo: database code here!
                result.ReportResult(true, true);
            });
        }

    }
}
