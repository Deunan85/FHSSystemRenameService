using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Security.Principal;

namespace FHSSystemRenameService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class SystemRenameService : ISystemRenameService
    {
        [OperationBehavior(Impersonation = ImpersonationOption.Required)]
        public bool RenameComputer(string Name)
        {

            bool renamed;
            WindowsIdentity identity = ServiceSecurityContext.Current.WindowsIdentity;
            using (identity.Impersonate())
            {
                renamed = WindowsAPI.SetComputerName(Name);
            }
            return renamed;
        }
    }
}
