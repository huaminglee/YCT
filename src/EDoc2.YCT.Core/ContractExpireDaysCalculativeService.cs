using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EDoc2.Common.Log;

namespace EDoc2.YCT.Core
{
    public class ContractExpireDaysCalculativeService
    {
        ILog _log;
        ContractFactory _factroy;
        public ContractExpireDaysCalculativeService(ContractFactory factroy)
        {
            _factroy = factroy;
        }

        public void Start(ILog log)
        {
            this._log = log;
            this._log.Info("启动计算合同过期天数服务");
            Thread t1 = new Thread(this.Thread1);
            t1.IsBackground = true;
            t1.Start();
        }

        private void Thread1()
        {
            this.CalculateExpireDays();
            while (true)
            {
                if (DateTime.Now.Hour == 1)
                {
                    this._log.Info("开始计算");
                    this.CalculateExpireDays();
                    this._log.Info("停止计算");
                }
#if DEBUG
                Thread.Sleep(1000 * 60);
#else
                Thread.Sleep(1000 * 60 * 60 * 5);
#endif
            }
        }

        private void CalculateExpireDays()
        {
            foreach (Contract contract in this._factroy.Contracts)
            {
                this._log.Info("计算文件" + contract.EDoc2FileId);
                try
                {
                    EDoc2MetadataHelper.CalculateExpireInfo(contract.EDoc2FileId, this._log);
                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    this._log.Error(ex.ToString());
                }
            }
        }
    }
}
