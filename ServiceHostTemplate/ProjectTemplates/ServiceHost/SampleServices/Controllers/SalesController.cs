﻿using CodeEndeavors.ServiceHost;
using CodeEndeavors.ServiceHost.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DomainObjects = $saferootprojectname$.Shared.DomainObjects;

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

        /// <summary>
        /// Returns sample customer
        /// </summary>
        /// <param name="userId">logged in user id</param>
        /// <param name="id">Customer id</param>
        [HttpGet]
        public ServiceResult<DomainObjects.Customer> CustomerGet(string userId, int id)
        {
            return SalesService.CustomerGet(id, userId);
        }

        [HttpPost]
        public ServiceResult<bool> CustomerSave(string userId, [FromBody]DomainObjects.Customer customer)
        {
            return SalesService.CustomerSave(customer, userId);
        }


    }
}
