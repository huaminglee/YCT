using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace EDoc2.ManagementService
{
    public abstract class ServiceWorkMonitor
    {
        private const string STOP_CMD = "stop ";

        private const string START_CMD = "start ";

        private string _serviceName;

        private string[] _dependentServiceName;

        public ServiceWorkMonitor(string serviceName)
        {
            this._serviceName = serviceName;
        }

        public ServiceWorkMonitor(string serviceName, string[] dependentServiceName)
        {
            this._serviceName = serviceName;
            this._dependentServiceName = dependentServiceName;
        }

        public void Restart()
        {
            LogManagement.Log.Info("准备重启服务:" + this._serviceName);
            Stop();
            Thread.Sleep(1000*60);
            Start();
            LogManagement.Log.Info("已经重启服务:" + this._serviceName);
        }

        public abstract bool AtWork();

        public void Monitor()
        {
            if (!this.AtWork())
            {
                this.Restart();
            }
        }

        public virtual void Stop()
        {
            NetProcess process = new NetProcess(STOP_CMD + this._serviceName);
            LogManagement.Log.Info("停止服务:" + this._serviceName);
            process.Run();
            if (this._dependentServiceName != null)
            {
                foreach (string servericeName in _dependentServiceName)
                {
                    process = new NetProcess(STOP_CMD + servericeName);
                    LogManagement.Log.Info("停止服务:" + servericeName);
                    process.Run();
                }

            }
        }

        public virtual void Start()
        {
            NetProcess process = new NetProcess(START_CMD + this._serviceName);
            LogManagement.Log.Info("启动服务:" + this._serviceName);
            process.Run();
            if (this._dependentServiceName != null)
            {
                foreach (string servericeName in _dependentServiceName)
                {
                    process = new NetProcess(START_CMD + servericeName);
                    LogManagement.Log.Info("启动服务:" + servericeName);
                    process.Run();
                }
            }
        }
    }
}
