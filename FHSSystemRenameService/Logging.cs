using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace FHSSystemRenameService
{
    class Logging
    {
        // Log4net call pulled directly from 
        // http://www.codeproject.com/Articles/140911/log4net-Tutorial
        // made the change from private to public for access
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
