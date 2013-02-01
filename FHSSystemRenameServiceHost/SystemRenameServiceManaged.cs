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

namespace FHSSystemRenameServiceHost
{
    public partial class SystemRenameServiceManged : ServiceBase
    {
        public SystemRenameServiceManged()
        {
            InitializeComponent();
        }
        private SystemRenameWorker _RenameWorker = new SystemRenameWorker();

        protected override void OnStart(string[] args)
        {
            // perform rename worker starting functions
            _RenameWorker.Startup();

            // Create the Uri to be accessed
            Uri baseAddress = new Uri("htt://" + NetOps.GetLocalIP() + ":8080/SystemRenameService");

            // begin the self-hosting of the service
            // Create a ServiceHost for the SystemRenameService type.
            using (ServiceHost serviceHost =
                   new ServiceHost(typeof(SystemRenameService),baseAddress))
            {
                // Open the ServiceHost to create listeners         // and start listening for messages.
                serviceHost.Open();

                // The service can now be accessed.
                
            }
        }

        protected override void OnStop()
        {
            _RenameWorker.End();
        }
    }
}
