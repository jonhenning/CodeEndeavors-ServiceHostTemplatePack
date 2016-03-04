using CodeEndeavors.ServiceHost;
using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainObjects = $solutionname$.Shared.DomainObjects;

namespace $defaultnamespace$
{
    public class $safeitemname$ : BaseService
    {
        //private string _$lowercasesafeitemname$Connection;
        private static $safeitemname$ _$lowercasesafeitemname$Service;

        public static $safeitemname$ $safeitemname$Service
        {
            get
            {
                if (_$lowercasesafeitemname$Service == null)
                    _$lowercasesafeitemname$Service = new $safeitemname$();
                return _$lowercasesafeitemname$Service;
            }
        }


        public $safeitemname$()
        {
            base.Configure();
            //_$lowercasesafeitemname$Connection = GetConnectionString("MyConnectionString", "");
        }

        //public ServiceResult<DomainObjects.Customer> CustomerGet(int id, string userId)
        //{
        //    return this.ExecuteServiceResult<DomainObjects.Customer>(result =>
        //    {
        //        //todo: database code here!
        //        var customer = new DomainObjects.Customer()
        //        {
        //            Id = id,
        //            FirstName = "Service First Name",
        //            LastName = "Service Last Name"
        //        };
        //        result.ReportResult(customer, true);
        //    });
        //}

        //public ServiceResult<bool> CustomerSave(DomainObjects.Customer customer, string userId)
        //{
        //    return this.ExecuteServiceResult<bool>(result =>
        //    {
        //        //todo: database code here!
        //        result.ReportResult(true, true);
        //    });
        //}

    }
}
