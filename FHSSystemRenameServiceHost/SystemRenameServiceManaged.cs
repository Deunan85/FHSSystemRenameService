using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using FHSSystemRenameService;
using System.ServiceModel.Description;

namespace FHSSystemRenameServiceHost
{
    public partial class SystemRenameServiceManged : ServiceBase
    {
        
        public SystemRenameServiceManged()
        {
            InitializeComponent();
        }

        private SystemRenameWorker _RenameWorker = new SystemRenameWorker();
        private ServiceHost serviceHost;

        protected override void OnStart(string[] args)
        {
            // perform rename worker starting functions
            _RenameWorker.Startup();

             // Create the Uri to be accessed
            Uri baseAddress = new Uri("http://" + NetOps.GetLocalIP() + ":8080/SystemRenameService");

            // Create Binding to be used by the service
            WSHttpBinding binding = new WSHttpBinding();

            // begin the self-hosting of the service
            // Create a ServiceHost for the SystemRenameService type.
            serviceHost = new ServiceHost(typeof(SystemRenameService));
            {
                serviceHost.AddServiceEndpoint(typeof(ISystemRenameService), binding, baseAddress);
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.HttpGetUrl = baseAddress;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                serviceHost.Description.Behaviors.Add(smb);

                // Open the ServiceHost to create listeners
                // and start listening for messages.
                serviceHost.Open();
            }
        }

        protected override void OnStop()
        {
            // Close the connection
            serviceHost.Close();
            serviceHost = null;
            _RenameWorker.End();
        }
    }
}
