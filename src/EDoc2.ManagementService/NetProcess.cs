using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace EDoc2.ManagementService
{
    class NetProcess
    {
        Process cmdProcess;
        public NetProcess(string cmd)
        {
            cmdProcess = new Process();
            cmdProcess.StartInfo.FileName = "net ";
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.Arguments = cmd;
            cmdProcess.StartInfo.CreateNoWindow = true;
            
        }

        public void Run()
        {
            this.cmdProcess.Start();
            this.cmdProcess.Close();
        }
    }
}
