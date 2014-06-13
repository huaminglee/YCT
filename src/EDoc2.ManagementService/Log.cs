using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace EDoc2.ManagementService
{
    public class LogManagement
    {
        static LogManagement()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static ILog _log;
        public static ILog Log
        {
            get
            {
                if (_log == null)
                {
                    _log = log4net.LogManager.GetLogger("EDoc2.ManagementServiceLogger");
                }
                return _log;
            }
        }
    }
}
