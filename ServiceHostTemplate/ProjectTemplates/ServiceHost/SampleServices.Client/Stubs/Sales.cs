using CodeEndeavors.ServiceHost.Common.Services;

namespace $saferootprojectname$.Client.Stubs
{
    public class Sales : ISalesService
    {
        public ServiceResult<Shared.DomainObjects.Customer> GetCustomer(int id)
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


        public void SetAquireUserIdDelegate(BaseClientHttpService.AquireUserId func)
        {
            
        }
    }
}
