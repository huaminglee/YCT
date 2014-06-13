using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDoc2.MetaData;
using EDoc2.Document;
using EDoc2.Common.Log;

namespace EDoc2.YCT.Core
{
    public class EDoc2MetadataHelper
    {
        public const string CONTRACT_META_NOTIFY_NAME = "合同提醒对象";
        public const string CONTRACT_META_NAME = "合同基本信息";
        public const string CONTRACT_META_ATTR_NAME_CONTRACT_NAME = "合同名称";
        public const string CONTRACT_META_ATTR_NAME_EXPIRE_DATE = "到期时间";
        public const string CONTRACT_META_ATTR_NAME_VALID_MONTH = "有效期(月)";
        public const string CONTRACT_META_ATTR_NAME_EXPIRE_DAYS = "到期天数";
        public const string CONTRACT_META_ATTR_NAME_SIGN_DATE = "签订日期";

        public static int ContactMetaTypeId;
        public static int ExpireDateMetaAttrId;
        public static int ValidMonthMetaAttrId;

        public static int GetMetaTypeId(IEDoc2Instance instance, string token, string typeName)
        {
            List<IEDoc2MetaType> metaTypeList;
            instance.MetaDataManagement.GetAllMetaType(token, out metaTypeList);

            foreach (IEDoc2MetaType metaType in metaTypeList)
            {
                if (metaType.TypeName == typeName)
                {
                    return metaType.TypeId;
                }
            }
            return 0;
        }

        public static int GetMetaAttrId(IEDoc2Instance instance, string token, int typeId, string attrName)
        {
            List<IEDoc2MetaAttr> metaAttrList = null;
            instance.MetaDataManagement.GetMetaAttrByTypeId(token,
                typeId, out metaAttrList);

            foreach (IEDoc2MetaAttr metaAttr in metaAttrList)
            {
                if (metaAttr.AttrName == attrName)
                {
                    return metaAttr.AttrId;
                }
            }
            return 0;
        }

        public static int GetMetaTypeId(string token, string typeName)
        {
            List<IEDoc2MetaType> metaTypeList;
            ApiManager.Api.MetaDataManagement.GetAllMetaType(token, out metaTypeList);

            foreach (IEDoc2MetaType metaType in metaTypeList)
            {
                if (metaType.TypeName == typeName)
                {
                    return metaType.TypeId;
                }
            }
            return 0;
        }

        public static int GetMetaAttrId(string token, int typeId, string attrName)
        {
            List<IEDoc2MetaAttr> metaAttrList = null;
            ApiManager.Api.MetaDataManagement.GetMetaAttrByTypeId(token,
                typeId, out metaAttrList);

            foreach (IEDoc2MetaAttr metaAttr in metaAttrList)
            {
                if (metaAttr.AttrName == attrName)
                {
                    return metaAttr.AttrId;
                }
            }
            return 0;
        }

        private static List<IEDoc2MetaObjType> GetMetaObjTypeByStrMeta(List<IEDoc2MetaObjType> metaObjTypeList)
        {
            var edoc2MetaObjTypeList = new List<IEDoc2MetaObjType>();
            foreach (IEDoc2MetaObjType oldMetaObjType in metaObjTypeList)
            {
                
                var metaValueList = new List<IEDoc2MetaValue>();

                foreach (IEDoc2MetaValue oldMetaValue in oldMetaObjType.EDoc2MetaValueList)
                {
                    IEDoc2MetaValue metaValue = new EDoc2MetaValue();
                    metaValue.AttrId = oldMetaValue.AttrId;
                    metaValue.AttrPath = oldMetaValue.AttrPath;
                    metaValue.AttrValue = oldMetaValue.AttrValue;

                    metaValueList.Add(metaValue);
                }
                var metaObjType = new EDoc2MetaObjType(oldMetaObjType.ObjData, oldMetaObjType.ObjId, oldMetaObjType.ObjType, oldMetaObjType.TypeId, metaValueList);
                IEDoc2MetaObjType IMetaObjType = metaObjType;
                edoc2MetaObjTypeList.Add(IMetaObjType);
            }
            return edoc2MetaObjTypeList;
        }

        public static int? GetValidMonth(IEDoc2File file)
        {
            int? validMonth = null;
            string metavalue = EDoc2MetadataHelper.GetMetaValue(file, ContactMetaTypeId, ValidMonthMetaAttrId);
            int intOutput;
            if (int.TryParse(metavalue, out intOutput))
            {
                validMonth = intOutput;
            }
            return validMonth;
        }

        public static DateTime? GetExpireDate(IEDoc2File file)
        {
            DateTime? expireDate = null;
            string metavalue = EDoc2MetadataHelper.GetMetaValue(file, ContactMetaTypeId, ExpireDateMetaAttrId);
            DateTime expireDateOutput;
            if (DateTime.TryParse(metavalue, out expireDateOutput))
            {
                expireDate = expireDateOutput;
            }
            return expireDate;
        }

        public static void CalculateExpireInfo(int fileId)
        {
            CalculateExpireInfo(fileId, null);
        }

        public static bool HasContactMeta(IEDoc2File file)
        {
            List<IEDoc2MetaObjType> metaObjTypeList = null;
            file.GetFileMetaData(file.FileLastVerId, out metaObjTypeList);
            if (metaObjTypeList != null)
            {
                return metaObjTypeList.Any(x => x.TypeId == ContactMetaTypeId);
            }
            return false;
                
        }

        public static void CalculateExpireInfo(int fileId, ILog log)
        {
            string token;
            ApiManager.Api.OrgnizationManagement.ImpersonateByLoginName("admin", "127.0.0.1", out token);
            ApiManager.CurrentUserToken = token;
            IEDoc2File file;
            ApiManager.Api.DocumentManagement.GetFileById(fileId, out file);
            List<IEDoc2MetaObjType> metaObjTypeList = null;
            if (file != null)
            {
                int expireDateMetaAttrId = GetMetaAttrId(token, ContactMetaTypeId, EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_EXPIRE_DATE);
                int expireDaysMetaAttrId = GetMetaAttrId(token, ContactMetaTypeId, EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_EXPIRE_DAYS);


                int validMonthMetaAttrId = GetMetaAttrId(token, ContactMetaTypeId, EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_VALID_MONTH);
                int signDateMetaAttrId = GetMetaAttrId(token, ContactMetaTypeId, EDoc2MetadataHelper.CONTRACT_META_ATTR_NAME_SIGN_DATE);

                file.GetFileMetaData(file.FileLastVerId, out metaObjTypeList);
                if (metaObjTypeList != null)
                {
                    IEDoc2MetaObjType metaObjType = metaObjTypeList.Find(x => x.TypeId == ContactMetaTypeId);
                    if (metaObjType.EDoc2MetaValueList != null)
                    {
                        IEDoc2MetaValue expireDateMetaValue = metaObjType.EDoc2MetaValueList.Find(x => x.AttrId == expireDateMetaAttrId);
                        IEDoc2MetaValue validMonthMetaValue = metaObjType.EDoc2MetaValueList.Find(x => x.AttrId == validMonthMetaAttrId);
                        IEDoc2MetaValue signDateMetaValue = metaObjType.EDoc2MetaValueList.Find(x => x.AttrId == signDateMetaAttrId);
                        IEDoc2MetaValue expireDaysMetaValue = metaObjType.EDoc2MetaValueList.Find(x => x.AttrId == expireDaysMetaAttrId);
                        DateTime signDate;
                        int validMonth;
                        if (signDateMetaValue != null && DateTime.TryParse(signDateMetaValue.AttrValue, out signDate)
                            && validMonthMetaValue != null && int.TryParse(validMonthMetaValue.AttrValue, out validMonth))
                        {
                            expireDateMetaValue.AttrValue = signDate.AddMonths(validMonth).AddDays(-1).ToString("yyyy-MM-dd");
                        }
                        DateTime expireDate;
                        if (expireDateMetaValue != null && DateTime.TryParse(expireDateMetaValue.AttrValue, out expireDate))
                        {
                            double expireDays = (expireDate - DateTime.Today).TotalDays;
                            if(expireDays < 0)
                            {
                                expireDays = 0;
                            }
                            expireDaysMetaValue.AttrValue = expireDays.ToString();
                            int fileVerId;
                            int fileLastVerId = file.FileLastVerId;
                            ContractExtensionService._jisuanzhongWenjianIdList.Add(fileId);
                            file.UpdateFileMeta(token, metaObjTypeList, out fileVerId);
                            if (log != null)
                            {
                                log.Info("UpdatedFileMeta");
                            }
                            if (fileVerId != fileLastVerId)
                            {
                                file.OverlayPrevVersion(token);
                                if (log != null)
                                {
                                    log.Info("OverlayPrevVersion");
                                }
                            }
                            file.CheckInFileVersion(token, "");
                        }
                    }
                }
            }
        }

        public static string GetMetaValue(IEDoc2File file, int metaTypeId, int metaAttrId)
        {
            int result = 0;
            if (file == null)
            {
                throw new Exception("file null");
            }
            List<IEDoc2MetaObjType> metaObjTypeList;
            result = file.GetFileMetaData(file.FileLastVerId, out metaObjTypeList);
            if (result != 0)
            {
                throw new Exception("GetFileMetaData result" + result);
            }
            if (metaObjTypeList == null)
            {
                throw new Exception("metaObjTypeList null");
            }
            IEDoc2MetaObjType objType = metaObjTypeList.Find(x => x.TypeId == metaTypeId);
            if (objType == null)
            {
                throw new Exception("objType null,this._metaTypeId:" + metaTypeId);
            }
            IEDoc2MetaValue validMonthMetaValue = objType.EDoc2MetaValueList.Find(x => x.AttrId == metaAttrId);
            if (validMonthMetaValue == null)
            {
                throw new Exception("validMonthMetaValue null, metaAttrId:" + metaAttrId);
            }
            else
            {
                return validMonthMetaValue.AttrValue;
            }
        }

        public static string GetMetaValue(IEDoc2Instance instance, int fileId, int metaTypeId, int metaAttrId)
        {
            IEDoc2File file;
            int result = 0;
            result = instance.DocumentManagement.GetFileById(fileId, out file);
            if (result != 0)
            {
                throw new Exception("GetFileById result" + result);
            }
            return GetMetaValue(file, metaTypeId, metaAttrId);
        }
    }
}
