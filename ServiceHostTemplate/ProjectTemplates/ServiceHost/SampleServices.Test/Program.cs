using CodeEndeavors.Extensions;
using CodeEndeavors.ServiceHost.Common.Services;
using CodeEndeavors.ServiceHost.Common.Services.LoggingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Client = $saferootprojectname$.Client;
using DomainObjects = $saferootprojectname$.Shared.DomainObjects;

namespace $saferootprojectname$.Test
{
    class Program
    {

        private static string AquireUserId()
        {
            return "5";
        }

        private static Client.Sales SalesService
        {
            get { return Client.Sales.Resolve(); }
        }


        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            string url = "http://localhost/ServiceHost/";
            Console.Write("Url (ENTER={0}):", url);
            string endpoint = Console.ReadLine();
            if (String.IsNullOrEmpty(endpoint) == false)
                url = endpoint;

            Console.WriteLine("Initializing Service ({0})", url);
            Log.Configure("log4net.config", "SampleAppLogger");

            Client.Sales.Register(url, 600000);
            SalesService.SetAquireUserIdDelegate(AquireUserId);

            string command = "";
            while (command.ToLower() != "exit")
            {
                try
                {
                    Console.Write(">");
                    command = Console.ReadLine();

                    var commandParts = command.Split(' ');
                    var method = commandParts[0];

                    switch (method.ToLower())
                    {
                        case "getcustomer":
                            {
                                WriteCommandResult(DoGetCustomer(commandParts));
                                break;
                            }
                        default:
                            {
                                RecordMessage("Unknown Command");
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    Console.WriteLine(ex.Message);
                }
            }
        }


        private static ClientCommandResult<DomainObjects.Customer> DoGetCustomer(string[] commandParts)
        {var id = 1;

            if (commandParts.Length > 1)
                id = int.Parse(commandParts[1]);

            RecordMessage(String.Format("Performing GetCustomer({0})", id));

            var cr = SalesService.GetCustomer(id);
            Console.WriteLine(cr.Data.ToJson());
            return cr;
        }

        private static void RecordMessage(string Message)
        {
            Log.Info(Message);
            Console.WriteLine(Message);
        }

        private static void WriteCommandResult<T>(ClientCommandResult<T> ccr)
        {
            RecordMessage(ccr.ToString());
        }

        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);
            if (assemblyName.Name != args.Name)
                return Assembly.LoadWithPartialName(assemblyName.Name);
            return null;
        }


    }
}
