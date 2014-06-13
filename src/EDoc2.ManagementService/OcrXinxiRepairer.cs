using System;
using System.Collections.Generic;
using System.Text;
using EDoc2.Data.DataEntities;

namespace EDoc2.ManagementService
{
    public class OcrXinxiRepairer : ServiceDataRepairer
    {
        private const string EXCUTE_SQL = @"update dms_ocrTask set task_status = 0 where task_status = 3";

        public OcrXinxiRepairer() : 
            base()
        {
            
        }

        public override void Repair()
        {
            LogManagement.Log.Info("进入OcrXinxiRepairer");
            this.ExcuteSql(EXCUTE_SQL);
            LogManagement.Log.Info("退出OcrXinxiRepairer");
        }
    }
}
