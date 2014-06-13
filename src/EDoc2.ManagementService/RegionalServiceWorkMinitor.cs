using System;
using System.Collections.Generic;
using System.Text;

namespace EDoc2.ManagementService
{
    public class RegionalServiceWorkMinitor : ServiceWorkMonitor
    {
        public RegionalServiceWorkMinitor()
            : base("EDoc2.RegionalService")
        {
            
        }
    
        public override bool  AtWork()
        {
            return false;
        }
    }
}
