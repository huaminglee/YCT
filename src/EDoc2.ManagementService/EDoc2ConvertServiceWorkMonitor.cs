using System;
using System.Collections.Generic;
using System.Text;
using EDoc2.Services;
using System.Threading;

namespace EDoc2.ManagementService
{
    public class EDoc2ConvertServiceWorkMonitor : ServiceWorkMonitor
    {
        ApiManager apiManager;
        public EDoc2ConvertServiceWorkMonitor()
            : base("EDoc2.ConversionService", new string[] { "EDoc2.WindowsService" })
        {
            
        }

        public override bool AtWork()
        {
            apiManager = new ApiManager();

            int fileVerId = GetLastConversionFileVerId();
            if (fileVerId > 0)
            {
                EDoc2.Document.IEDoc2FileVer fileVer;
                apiManager.Api.DocumentManagement.GetFileVerById(fileVerId, out fileVer);
                int excpetTime = ComputeConversionTime(fileVer.File_Size);
                LogManagement.Log.Info(string.Format("正在转档版本文件：名称:{0}, 版本ID:{1}, 大小:{2}M, {3}分钟后将判断是否转档成功。",
                    fileVer.File_VerName, fileVer.File_VerId, fileVer.File_Size / 1024.0 / 1024.0, excpetTime));
                Thread.Sleep(1000 * 60 * excpetTime);
                bool exist = ExistConversionFileVerId(fileVerId);
                if (exist)
                {
                    LogManagement.Log.Info("转档失败");
                    return false;
                }
                else
                {
                    LogManagement.Log.Info("转档成功");
                    return true;
                }
            }
            return true;
        }

        private int ComputeConversionTime(long fileSize)
        {
            double fileMegohmSize = fileSize / 1024.0 / 1024.0;//M                    
            int excpetTime = 5;//分钟
            if (fileMegohmSize < 5)
            {
                excpetTime = 5;
            }
            else if (fileMegohmSize < 10)
            {
                excpetTime = 10;
            }
            else if (fileMegohmSize < 50)
            {
                excpetTime = 20;
            }
            else if (fileMegohmSize < 100)
            {
                excpetTime = 30;
            }
            else
            {
                excpetTime = 60;
            }
            return excpetTime;
        }

        private int GetLastConversionFileVerId()
        {
            List<EDoc2.Region.IEDoc2Region> regionList;
            apiManager.Api.RegionManagement.GetAllRegions(apiManager.CurrentUserToken, out regionList);

            foreach (EDoc2.Region.IEDoc2Region region in regionList)
            {
                List<EDoc2.Document.IEDoc2FileConversion> convList;
                apiManager.Api.ConversionManagement.GetFileConversionList(region.RegionId, 10, false, out convList);
                if (convList != null && convList.Count > 0)
                {
                    return convList[0].FileVerId;
                }
            }

            return 0;
        }

        private bool ExistConversionFileVerId(int fileVerId)
        {
            List<EDoc2.Region.IEDoc2Region> regionList;
            apiManager.Api.RegionManagement.GetAllRegions(apiManager.CurrentUserToken, out regionList);

            foreach (EDoc2.Region.IEDoc2Region region in regionList)
            {
                List<EDoc2.Document.IEDoc2FileConversion> convList;
                apiManager.Api.ConversionManagement.GetFileConversionList(region.RegionId, 200, false, out convList);
                foreach (EDoc2.Document.IEDoc2FileConversion fileConv in convList)
                {
                    if (fileConv.FileVerId == fileVerId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
