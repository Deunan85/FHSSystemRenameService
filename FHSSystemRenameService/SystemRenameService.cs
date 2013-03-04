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
                Logging.log.Debug("Attempting to rename " + Environment.MachineName + " to " + Name);
                uint returnedValue;
                WindowsIdentity identity = ServiceSecurityContext.Current.WindowsIdentity;
                using (identity.Impersonate())
                {
                    Logging.log.Debug("Calling rename function");
                    returnedValue = ComputerRename.SetComputerName(Name);
                    Logging.log.Debug("Rename function called");
                    //Console.WriteLine("RenameCalled");
                    Logging.log.Debug(returnedValue);
                }
                if (returnedValue == 0)
                {
                    return true;
                }
                else
                {
                    //Console.WriteLine("Error Code: "+returnedValue);
                    return false;
                }

                //return renamed;
            }
            catch (Exception ex)
            {
                Logging.log.Debug("Error occured " + ex.Message, ex);
                //Console.WriteLine("There was an error");
                //WriteInnerException(ex);
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
