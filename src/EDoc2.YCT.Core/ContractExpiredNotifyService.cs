using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDoc2.Common.Log;
using System.Threading;
using EDoc2.Document;
using System.IO;
using EDoc2.MetaData;
using EDoc2.Organization;

namespace EDoc2.YCT.Core
{
    public class ContractExpiredNotifyService
    {
        ContractFactory _factroy;
        int _advanceDays;
        int _notifyMetaTypeId;
        int _contractMetaTypeId;
        int _contractNameAttrTypeId;
        ContractExpiredNotifiyDataManager _contractExpiredNotifiyDataManager;
        public ContractExpiredNotifyService(ContractFactory factroy, int advanceDays, int notifyMetaTypeId,
            int contractMetaTypeId, int contractNameAttrTypeId)
        {
            this._factroy = factroy;
            this._advanceDays = advanceDays;
            _contractExpiredNotifiyDataManager = new ContractExpiredNotifiyDataManager();
            this._notifyMetaTypeId = notifyMetaTypeId;
            this._contractMetaTypeId = contractMetaTypeId;
            this._contractNameAttrTypeId = contractNameAttrTypeId;
        }

        public void Start()
        {
            ContractManagerHelper.Logger.Info("启动合同过期通知服务");
            Thread t1 = new Thread(this.Thread1);
            t1.IsBackground = true;
            t1.Start();
        }

        private void Thread1()
        {
            Thread.Sleep(1000 * 3);
            while (true)
            {
                ContractManagerHelper.Logger.Info("开始通知");
                this.Notify();
                ContractManagerHelper.Logger.Info("停止通知");
#if DEBUG
                Thread.Sleep(1000 * 60);
#else
                Thread.Sleep(1000 * 60 * 60 );
#endif
            }
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

        private string GetEmails(string token, IEDoc2File file)
        {
            string emails = "";
            List<IEDoc2MetaObjType> metaObjTypeList;
            bool isInherit;
            file.ParentFolder.GetFolderMetaTypesAndValues(1, out metaObjTypeList, false, out isInherit);
            IEDoc2MetaObjType notifyMetadata = metaObjTypeList.Find(x => x.TypeId == this._notifyMetaTypeId);
            if (notifyMetadata != null)
            {
                foreach (IEDoc2MetaValue metavalue in notifyMetadata.EDoc2MetaValueList)
                {
                    ContractManagerHelper.Logger.Info("metavalue.AttrValue:" + metavalue.AttrValue);
                    if (metavalue.AttrValue.IndexOf(" ") > -1)
                    {
                        int userId;
                        if (int.TryParse(metavalue.AttrValue.Split(' ')[0], out userId) && userId > 0)
                        {
                            EDoc2UserInfo userInfo;
                            ApiManager.Api.OrgnizationManagement.GetUserById(token, userId, out userInfo);
                            if (userInfo != null && !string.IsNullOrEmpty(userInfo.UserEmail))
                            {
                                emails += userInfo.UserEmail + ";";
                            }
                        }
                    }
                }
            }
            else
            {
                ContractManagerHelper.Logger.Info("notifyMetadata null");
            }
            return emails.TrimEnd(';');
        }

        private string GetContractName(string token, IEDoc2File file)
        {
            return EDoc2MetadataHelper.GetMetaValue(file, this._contractMetaTypeId, this._contractNameAttrTypeId);
        }

        public void Notify()
        {
            string token = null;
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
                        && !this.Notified(contract.EDoc2FileId, contract.ExpireDate.Value))
                    {
                        string notifyMails = this.GetEmails(token, file);
                        if (!string.IsNullOrEmpty(notifyMails))
                        {
                            string contractName = this.GetContractName(token, file);
                            ContractManagerHelper.Logger.Info(string.Format("合同{0}将于{1}到期准备发送邮件给{2}", contract.EDoc2FileId, contract.ExpireDate, notifyMails));
                            MailSender.Send(notifyMails, contractName + "-合同到期提醒", "", "", true,
                                    string.Format("<html><body>合同{0}将于{1}过期, <a href='{2}?FileId={3}'>查看详细信息</a></body></html>", contractName, contract.ExpireDate.Value.ToString("yyyy-MM-dd"), edoc2PreviewAddress, contract.EDoc2FileId));
                            this.WriteLog(contract.EDoc2FileId, contract.ExpireDate.Value);
                        }
                        else
                        {
                            ContractManagerHelper.Logger.Info(string.Format("合同{0}将于{1}到期，无法获取通知邮件", contract.EDoc2FileId, contract.ExpireDate));
                        }
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
