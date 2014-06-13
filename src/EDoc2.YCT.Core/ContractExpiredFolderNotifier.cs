using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDoc2.Document;
using System.Threading;
using System.IO;

namespace EDoc2.YCT.Core
{
    public class ContractExpiredFolderNotifier
    {
        ContractFactory _factroy;
        int _advanceDays;
        string _notifyMails;
        int _folderId;
        ContractExpiredNotifiyDataManager _contractExpiredNotifiyDataManager;
        public ContractExpiredFolderNotifier(ContractExpiredNotifiyDataManager contractExpiredNotifiyDataManager, ContractFactory factroy, 
            int advanceDays, string notifyMails, int folderId)
        {
            this._contractExpiredNotifiyDataManager = contractExpiredNotifiyDataManager;
            this._factroy = factroy;
            this._advanceDays = advanceDays;
            this._notifyMails = notifyMails;
            this._folderId = folderId;
        }


        public void WriteLog(int fileId, DateTime expireDate)
        {
            ContractExpiredNotifiyLogInfo info = new ContractExpiredNotifiyLogInfo();
            info.EDoc2FileId = fileId;
            info.ExpireDate = expireDate;
            this._contractExpiredNotifiyDataManager.Insert(info);
        }

        public bool Notified(int fileId, DateTime expireDate)
        {
            return this._contractExpiredNotifiyDataManager.Exist(fileId, expireDate);
        }

        private bool InFolder(IEDoc2File file)
        {
            string[] strFolderIds = file.FilePath.Split('\\');
            return strFolderIds.Contains(this._folderId.ToString());
        }

        public void Notify()
        {
            string token;
            string edoc2PreviewAddress = "";

            try
            {
                ApiManager.Api.OrgnizationManagement.ImpersonateByLoginName("admin", "127.0.0.1", out token);
                InstanceConfigInfo configInfo;
                ApiManager.Api.SystemManagement.GetInstanceConfig(token, out configInfo);
                if (!string.IsNullOrEmpty(configInfo.PublishExternalAddress))
                {
                    edoc2PreviewAddress = configInfo.PublishExternalAddress.TrimEnd('/') + "/Preview.aspx";
                }
            }
            catch (Exception ex)
            {
                ContractManagerHelper.Logger.Error(ex.ToString());
            }
            ContractManagerHelper.Logger.Info("监控合同数量" + this._factroy.Contracts.Count);

            foreach (Contract contract in this._factroy.Contracts)
            {
                try
                {
                    IEDoc2File file;
                    ApiManager.Api.DocumentManagement.GetFileById(contract.EDoc2FileId, out file);
                    ContractManagerHelper.Logger.Info(string.Format("合同{0}到期时间{1}", contract.EDoc2FileId, contract.ExpireDate));
                    if (file != null
                        && contract.ExpireDate.HasValue 
                        && (contract.ExpireDate.Value - DateTime.Now).TotalDays <= this._advanceDays 
                        && !this.Notified(contract.EDoc2FileId, contract.ExpireDate.Value)
                        && this.InFolder(file))
                    {
                        ContractManagerHelper.Logger.Info(string.Format("合同{0}将于{1}到期准备发送邮件给{2}", contract.EDoc2FileId, contract.ExpireDate, this._notifyMails));
                        MailSender.Send(this._notifyMails, Path.GetFileName(file.FileName) + "-合同到期提醒", "", "", true,
                                string.Format("<html><body>合同{0}将于{1}过期, <a href='{2}?FileId={3}'>查看详细信息</a></body></html>", Path.GetFileName(file.FileName), contract.ExpireDate.Value.ToString("yyyy-MM-dd"), edoc2PreviewAddress, contract.EDoc2FileId));
                        this.WriteLog(contract.EDoc2FileId, contract.ExpireDate.Value);
                    }
                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    ContractManagerHelper.Logger.Error(ex.ToString());
                }
            }
        }
    }
}
