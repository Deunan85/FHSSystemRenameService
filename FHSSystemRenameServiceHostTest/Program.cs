using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using FHSSystemRenameService;
using FHSSystemRenameServiceHost;
using System.ServiceModel.Description;

namespace FHSSystemRenameServiceHostTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SystemRenameWorker _RenameWorker = new SystemRenameWorker();
            
            // perform rename worker starting functions
            _RenameWorker.Startup();

            // Create the Uri to be accessed
            Uri baseAddress = new Uri("http://" + NetOps.GetLocalIP() + ":8080/SystemRenameService");

            //// Create Binding to be used by the service
            WSHttpBinding binding = new WSHttpBinding();

            try
            {
                // begin the self-hosting of the service
                // Create a ServiceHost for the SystemRenameService type.
                using (ServiceHost serviceHost =
                       new ServiceHost(typeof(SystemRenameService)))
                {
                    serviceHost.AddServiceEndpoint(typeof(ISystemRenameService), binding, baseAddress);
                    // Enable metadata publishing.
                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    smb.HttpGetUrl = baseAddress;
                    smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                    serviceHost.Description.Behaviors.Add(smb);

                    // Open the ServiceHost to create listeners         // and start listening for messages.
                    serviceHost.Open();

                    // The service can now be accessed.
                    Console.WriteLine("The service is ready.");
                    Console.WriteLine(baseAddress);
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.WriteLine();
                    Console.ReadLine();

                    //serviceHost.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error was encountered");
                Console.WriteLine(ex.Message);
                WriteInnerException(ex.InnerException);
                Console.WriteLine(ex.StackTrace);
            }
            _RenameWorker.End();
        }
        private static void WriteInnerException(Exception inner)
        {
            if (inner != null)
            {
                Console.WriteLine(inner.Message);
                WriteInnerException(inner.InnerException);
            }
        }
    }
}
