using System;
using System.Collections.Generic;
using System.Text;
using EDoc2.EDoc2InstanceSchema;
using EDoc2.Common.DataAccess;
using EDoc2.Data;
using EDoc2.Data.DataEntities;

namespace EDoc2.ManagementService
{
    public class EDoc2ConversionServiceDataRepairer : ServiceDataRepairer
    {

        protected ApiManager _apiManager;
        public EDoc2ConversionServiceDataRepairer() : 
            base()
        {
            _apiManager = new ApiManager();
        }

        public override void Repair()
        {
            LogManagement.Log.Debug("进入Repair");
            List<EDoc2.Region.IEDoc2Region> regionList;
            _apiManager.Api.RegionManagement.GetAllRegions(_apiManager.CurrentUserToken, out regionList);

            foreach (EDoc2.Region.IEDoc2Region region in regionList)
            {
                List<DmsFileConversionDataEntity> entityList = _edoc2DataProviderManager.DmsFileConversionDataProvider.Select("[conv_state]=5000 or [conv_state]=5001 or [conv_state]=5002 or [conv_state]=512", null);
                foreach (DmsFileConversionDataEntity entity in entityList)
                {
                    if (entity.ConvState == 512)
                    {
                        entity.ConvState = 0;
                        LogManagement.Log.Info(string.Format("修复FileConversion状态为512的数据：FileVerId:{0}", entity.FileVerId));
                        _edoc2DataProviderManager.DmsFileConversionDataProvider.UpdateByPrimaryKeys(entity, entity.InstanceId, entity.FileVerId);
                    }
                }
            }
            LogManagement.Log.Debug("退出Repair");
        }
    }
}
