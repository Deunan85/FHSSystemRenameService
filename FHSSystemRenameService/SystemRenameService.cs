using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Security.Principal;
using System.Management;

namespace FHSSystemRenameService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class SystemRenameService : ISystemRenameService
    {
        //[OperationBehavior(Impersonation = ImpersonationOption.Required)]
        public bool RenameComputer(string Name)
        {
            try
            {
                bool renamed;
                uint returnedValue;
                WindowsIdentity identity = ServiceSecurityContext.Current.WindowsIdentity;
                using (identity.Impersonate())
                {
                    returnedValue = WindowsAPI.SetComputerName(Name);
                    Console.WriteLine("RenameCalled");
                    //renamed = true;
                }
                if (returnedValue == 0)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Error Code: "+returnedValue);
                    return false;
                }

                //return renamed;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error");
                WriteInnerException(ex);
                return false;
            }
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
