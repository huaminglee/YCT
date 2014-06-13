using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EDoc2.Document;

namespace EDoc2.YCT.Core
{
    public class ContractShujuXiuhuService
    {
        ContractFactory _contractFactory;

        public ContractShujuXiuhuService(ContractFactory contractFactory)
        {
            this._contractFactory = contractFactory;
        }

        public void Start()
        {
            ContractManagerHelper.Logger.Info("启动ContractShujuXiuhuService");
            Thread t1 = new Thread(this.Thread1);
            t1.IsBackground = true;
            t1.Start();
        }

        private void Thread1()
        {
            Thread.Sleep(1000 * 3);
            while (true)
            {
                ContractManagerHelper.Logger.Info("开始数据修护");
                this.Xiuhu();
                ContractManagerHelper.Logger.Info("停止数据修护");
#if DEBUG
                Thread.Sleep(1000 * 60);
#else
                Thread.Sleep(1000 * 60 * 60 * 10);
#endif
            }
        }

        private void Xiuhu()
        {
            string token = null;
            ApiManager.Api.OrgnizationManagement.ImpersonateByLoginName("admin", "127.0.0.1", out token);
#if DEBUG
            List<IEDoc2File> fileList = this.GetFolderFiles(token, 767);
#else
            List<IEDoc2File> fileList = this.GetFolderFiles(token, 1166);
#endif
            foreach (IEDoc2File file in fileList)
            {
                if (EDoc2MetadataHelper.HasContactMeta(file) && !this._contractFactory.Exists(file.FileId))
                {
                    try
                    {
                        ContractManagerHelper.Logger.Info("Xiuhu" + file.FileId);
                        EDoc2MetadataHelper.CalculateExpireInfo(file.FileId, ContractManagerHelper.Logger);
                        DateTime? expireDate = EDoc2MetadataHelper.GetExpireDate(file);
                        int? validMonth = EDoc2MetadataHelper.GetValidMonth(file);
                        Contract contract = this._contractFactory.Create(file.FileId, expireDate, validMonth);
                    }
                    catch (Exception ex)
                    {
                        ContractManagerHelper.Logger.Error("数据修复错误:" + ex.ToString());
                    }
                }
            }
        }

        private List<IEDoc2File> GetFolderFiles(string token, int folderId)
        {
            List<IEDoc2File> fileList;
            ApiManager.Api.DocumentManagement.GetChildFileList(token, folderId, out fileList);
            if (fileList == null)
            {
                fileList = new List<IEDoc2File>();
            }
            List<IEDoc2Folder> folderList;
            ApiManager.Api.DocumentManagement.GetChildFolderList(token, folderId, out folderList);
            if (folderList != null)
            {
                foreach (IEDoc2Folder folder in folderList)
                {
                    fileList.AddRange(this.GetFolderFiles(token, folder.FolderId));
                }
            }
            return fileList;
        }
    }
}
