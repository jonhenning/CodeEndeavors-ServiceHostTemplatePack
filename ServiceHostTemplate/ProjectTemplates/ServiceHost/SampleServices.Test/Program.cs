using CodeEndeavors.Extensions;
using CodeEndeavors.ServiceHost.Common.Services;
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
            get { return ServiceLocator.Resolve<Client.Sales>(); }
        }

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            string url = "$servicehosturl$";
            Console.Write("Url (ENTER={0}):", url);
            string endpoint = Console.ReadLine();
            if (String.IsNullOrEmpty(endpoint) == false)
                url = endpoint;

            Console.WriteLine("Initializing Service ({0})", url);

            ServiceLocator.Register<Client.Sales>(url, 600000);
            SalesService.SetAquireUserIdDelegate(AquireUserId);

            SalesService.ConfigureLogging("Debug", (string level, string message) =>
                {
                    recordMessage(string.Format("{0}:{1}", level, message));
                });

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
                                DoGetCustomer(commandParts);
                                break;
                            }
                        default:
                            {
                                recordMessage("Unknown Command");
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    recordMessage(ex.Message);
                }
            }
        }

        private static void DoGetCustomer(string[] commandParts)
        {
            var id = 1;
            if (commandParts.Length > 1)
                id = int.Parse(commandParts[1]);

            var cr = SalesService.CustomerGet(id);
            recordMessage(cr.Data.ToJson(true));
        }

        private static void recordMessage(string message)
        {
            Console.WriteLine(message);
        }
        private static void recordMessage(string message, params object[] args)
        {
            Console.WriteLine(message, args);
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
