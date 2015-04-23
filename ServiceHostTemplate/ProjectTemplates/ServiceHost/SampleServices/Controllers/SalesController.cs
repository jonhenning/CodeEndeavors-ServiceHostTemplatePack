using CodeEndeavors.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $saferootprojectname$.Controllers
{
    public class SalesController : BaseController
    {
        private static Sales _salesService;

        private Sales SalesService
        {
            get
            {
                if (_salesService == null)
                    _salesService = new Sales();
                return _salesService;
            }
        }

        public void GetCustomer(string userId, int id)
        {
            WriteJSON(SalesService.GetCustomer(id, userId), true);
        }
    }
}
