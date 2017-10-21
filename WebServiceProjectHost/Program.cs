using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebServiceProjectTest;
using System.Configuration;

namespace WebServiceProjectHost
{
    class Program
    {
        private static bool running = true;
        static void Main(string[] args)
        { 
            ServiceHost webServiceProjectHost = null;
            try
            {
                //Base Address for StudentService
                Uri httpBaseAddress = new Uri("http://localhost:8733/Design_Time_Addresses/WebServiceProject/IzingaService/");

                //Instantiate ServiceHost
                webServiceProjectHost = new ServiceHost(typeof(WebServiceProjectTest.IzingaService),
                    httpBaseAddress);

                //Add Endpoint to Host
                webServiceProjectHost.AddServiceEndpoint(typeof(WebServiceProjectTest.IIzingaService),
                                                        new WSDualHttpBinding(), "");

                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
                webServiceProjectHost.Description.Behaviors.Add(serviceBehavior);

                //Open
                webServiceProjectHost.Open();
                Console.WriteLine("Service is live now at: {0}", httpBaseAddress);


                Console.ReadKey();
            }

            catch (Exception ex)
            {
                webServiceProjectHost = null;
                Console.WriteLine("There is an issue with StudentService" + ex.Message);
            }
        }

    }
}
