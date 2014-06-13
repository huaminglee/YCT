using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Configuration;

namespace EDoc2.ManagementService
{
    partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread th = new Thread(new ThreadStart(this._Start));
            th.IsBackground = true;
            th.Start();
        }

        public void _Start()
        {
            LogManagement.Log.Info("EDoc2.ManagementService开始运行");
            while (true)
            {
                try
                {
                    OcrXinxiRepairer repaier = new OcrXinxiRepairer();
                    repaier.Repair();
                }
                catch (Exception ex)
                {
                    LogManagement.Log.Error(ex.Message, ex);
                }
                Thread.Sleep(1000 * 60 * 10);
            }
        }

        //public void _Start()
        //{
        //    LogManagement.Log.Info("EDoc2.ManagementService开始运行");
        //    while (true)
        //    {
        //        try
        //        {
        //            List<int> monitHours = new List<int>();

        //            if (ConfigurationManager.AppSettings == null ||
        //                string.IsNullOrEmpty(ConfigurationManager.AppSettings["MonitHour"]))
        //            {
        //                monitHours.Add(7);
        //            }
        //            else
        //            {
        //                string[] strMonitHours = ConfigurationManager.AppSettings["MonitHour"].Split(',');
        //                foreach (string strMonitHour in strMonitHours)
        //                {
        //                    int monitHour;
        //                    if (int.TryParse(strMonitHour, out monitHour))
        //                    {
        //                        monitHours.Add(monitHour);
        //                    }
        //                }
        //            }
        //            if (monitHours.Contains(DateTime.Now.Hour))
        //            {
        //                LogManagement.Log.Info("创建RegionalServiceWorkMinitor");
        //                RegionalServiceWorkMinitor monitor = new RegionalServiceWorkMinitor();
        //                monitor.Monitor();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogManagement.Log.Error(ex.Message, ex);
        //        }
        //        Thread.Sleep(1000 * 60 * 30);
        //    }
        //}

        protected override void OnStop()
        {
            LogManagement.Log.Info("EDoc2.ManagementService停止运行");
        }
    }
}
