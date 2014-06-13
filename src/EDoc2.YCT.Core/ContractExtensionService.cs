using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDoc2.Message;
using EDoc2.Document;
using EDoc2.MetaData;
using System.Configuration;
using System.Threading;

namespace EDoc2.YCT.Core
{
    public class ContractExtensionService : EDoc2.ExtensionServices.EDoc2ExtensionService
    {
        bool _initSucceed;
        ContractExpireDaysCalculativeService _expireDaysCalculativeService;
        ContractExpiredNotifyService _contractExpiredNotifyService;
        ContractFactory _contractFactory;
        int _metaTypeId;
        int _expireDateMetaAttrId;
        int _validMonthMetaAttrId;
        internal static List<int> _jisuanzhongWenjianIdList;
        public ContractExtensionService()
        {
            _jisuanzhongWenjianIdList = new List<int>();
        }

        protected override void OnInitService()
        {
            this._serviceName = "ContractExtensionService";
            this.LogDirectory = AppDomain.CurrentDomain.BaseDirectory + "Logs\\YCT\\";

            // TODO: 编写初始化消息服务的代码。
            this._errorLog.Info(string.Format("Init Extension Service: {0}", this._serviceName));
            ContractManagerHelper.Logger = this._errorLog;
            try
            {
                string token;
                ApiManager.Api.OrgnizationManagement.ImpersonateByLoginName("admin", "127.0.0.1", out token);
                int notifyMetaTypeId = EDoc2MetadataHelper.GetMetaTypeId(token, EDoc2MetadataHelper.CONTRACT_META_NOTIFY_NAME);
                this._metaTypeId = EDoc2MetadataHelper.GetMetaTypeId(token, EDoc2MetadataHelper.CONTRACT_META_NAME);
                EDoc2MetadataHelper.ContactMetaTypeId = this._metaTypeId;
                int contractNameMetaAttrId = EDoc2MetadataHelper.GetMetaAttrId(token, this._metaTypeId, EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_CONTRACT_NAME);
                this._expireDateMetaAttrId = EDoc2MetadataHelper.GetMetaAttrId(token, this._metaTypeId, EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_EXPIRE_DATE);
                EDoc2MetadataHelper.ExpireDateMetaAttrId = this._expireDateMetaAttrId;
                this._validMonthMetaAttrId = EDoc2MetadataHelper.GetMetaAttrId(token, this._metaTypeId, EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_VALID_MONTH);
                EDoc2MetadataHelper.ValidMonthMetaAttrId = this._validMonthMetaAttrId;
                if (this._metaTypeId == 0)
                {
                    throw new Exception("找不到元数据" + EDoc2MetadataHelper.CONTRACT_META_NAME);
                }
                if (this._expireDateMetaAttrId == 0)
                {
                    throw new Exception("找不到元数据属性" + EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_EXPIRE_DATE);
                }
                if (this._validMonthMetaAttrId == 0)
                {
                    throw new Exception("找不到元数据属性" + EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_VALID_MONTH);
                }
                if (notifyMetaTypeId == 0)
                {
                    throw new Exception("找不到元数据属性" + EDoc2MetadataHelper.CONTRACT_META_NOTIFY_NAME);
                }
                if (contractNameMetaAttrId == 0)
                {
                    throw new Exception("找不到元数据属性" + EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_CONTRACT_NAME);
                }

                this._contractFactory = new ContractFactory();
                this._expireDaysCalculativeService = new ContractExpireDaysCalculativeService(_contractFactory);
                this._expireDaysCalculativeService.Start(this._errorLog);
                this._errorLog.Info("Started ContractExpireDaysCalculativeService");
                Thread.Sleep(1000 * 3);

                this._contractExpiredNotifyService = new ContractExpiredNotifyService(_contractFactory, 60, notifyMetaTypeId, this._metaTypeId, contractNameMetaAttrId);
                this._contractExpiredNotifyService.Start();
                this._errorLog.Info("Started ContractExpiredNotifyService");

                ContractShujuXiuhuService shujuXiuhuService = new ContractShujuXiuhuService(_contractFactory);
                shujuXiuhuService.Start();
                this._errorLog.Info("Started ContractShujuXiuhuService");
                _initSucceed = true;
                
            }
            catch(Exception ex)
            {
                _initSucceed = false;
                this._errorLog.Info(ex.ToString());
                throw;
            }
        }
         	
        protected override void ProcessMessageData(EDoc2InstanceMessageData messageData)
        {
            if (!_initSucceed)
            {
                return;
            }
            UpdateOperationMessage updateOperationMessage = messageData.MessageData as UpdateOperationMessage;
            this._errorLog.Info("entry ProcessMessageData:MessageType" + messageData.MessageType);
            if (messageData.MessageType == EDoc2ApiConst.OP_FILE_PROPERTY_CHANGE)
            {
                this._errorLog.Info("this._jisuanzhongWenjianIdList.count" + _jisuanzhongWenjianIdList.Count);
                if (_jisuanzhongWenjianIdList.Contains(updateOperationMessage.sourceId))
                {
                    _jisuanzhongWenjianIdList.Remove(updateOperationMessage.sourceId);
                    return;
                }
                this._errorLog.Info("fileId:" + updateOperationMessage.sourceId);
                IEDoc2File file;
                messageData.Instance.DocumentManagement.GetFileById(updateOperationMessage.sourceId, out file);
                if (!EDoc2MetadataHelper.HasContactMeta(file))
                {
                    this._errorLog.Info("不是合同文件退出");
                    return;
                }

                int? validMonth = this.GetValidMonth(messageData.Instance, updateOperationMessage.sourceId);
                if (!this._contractFactory.Exists(updateOperationMessage.sourceId))
                {
                    string token;
                    messageData.Instance.OrgnizationManagement.ImpersonateByLoginName("admin", "127.0.0.1", out token);
                    this._errorLog.Info("CalculateExpireDate");
                    EDoc2MetadataHelper.CalculateExpireInfo(updateOperationMessage.sourceId);
                    DateTime? expireDate = this.GetExpireDate(messageData.Instance, updateOperationMessage.sourceId);
                    Contract contract = this._contractFactory.Create(updateOperationMessage.sourceId, expireDate, validMonth);
                    try
                    {
                        contract.Update(validMonth);
                    }
                    catch (Exception ex)
                    {
                        this._errorLog.Error("CalculateExpireDate错误:" + ex.ToString());
                    }
                }
                else
                {
                    this._errorLog.Info("CalculateExpireDate");
                    EDoc2MetadataHelper.CalculateExpireInfo(updateOperationMessage.sourceId);
                    DateTime? expireDate = this.GetExpireDate(messageData.Instance, updateOperationMessage.sourceId);

                    Contract contract = this._contractFactory.GetContractByFileId(updateOperationMessage.sourceId);
                    this._errorLog.Info(string.Format("contract old ExpireDate{0}, contract new ExpireDate{1}", contract.ExpireDate, expireDate));
                    if (contract.ExpireDate != expireDate)
                    {
                        contract.Update(expireDate);
                    }

                    try
                    {
                        this._errorLog.Info(string.Format("contract old ValidMonth{0}, contract new validMonth{1}", contract.ValidMonth, validMonth));
                        if (contract.ValidMonth != validMonth)
                        {
                            contract.Update(validMonth);
                        }
                    }
                    catch (Exception ex)
                    {
                        this._errorLog.Error("CalculateExpireDate错误:" + ex.ToString());
                    }
                }
                
            }
            this._errorLog.Info("out ProcessMessageData");
        }

        private int? GetValidMonth(IEDoc2Instance instance, int fileId)
        {
            int? validMonth = null;
            string metavalue = EDoc2MetadataHelper.GetMetaValue(instance, fileId, this._metaTypeId, this._validMonthMetaAttrId);
            int intOutput;
            if (int.TryParse(metavalue, out intOutput))
            {
                validMonth = intOutput;
            }
            return validMonth;
        }

        private DateTime? GetExpireDate(IEDoc2Instance instance, int fileId)
        {
            DateTime? expireDate = null;
            string metavalue = EDoc2MetadataHelper.GetMetaValue(instance, fileId, this._metaTypeId, this._expireDateMetaAttrId);
            DateTime expireDateOutput;
            if (DateTime.TryParse(metavalue, out expireDateOutput))
            {
                expireDate = expireDateOutput;
            }
            return expireDate;
        }
    }
}
