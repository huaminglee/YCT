using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EDoc2.MetaData;
using EDoc2.Document;
using EDoc2.YCT.Core;

namespace EDoc2.YCTTest
{
    public class MetadataTester
    {
        [Test]
        public void Test()
        {

            string token;
            ApiManager.Api.OrgnizationManagement.ImpersonateByLoginName("admin", "192.168.1.168", out token);
            ApiManager.CurrentUserToken = token;

            string xmlArgs = "<GetListArgs><PageNum>1</PageNum></GetListArgs>";
            DocListInfo listInfo;
            ApiManager.Api.DocumentManagement.GetFolderChildren(token, 761, 0, xmlArgs, out listInfo);

            IEDoc2File file;
            ApiManager.Api.DocumentManagement.GetFileById(84, out file);
            List<IEDoc2MetaObjType> edoc2MetaObjTypeList;
            file.GetFileMetaData(file.FileLastVerId, out edoc2MetaObjTypeList);
            edoc2MetaObjTypeList[0].EDoc2MetaValueList.Find(x => x.AttrId == 102).AttrValue = "9";

            int lastVerId;
            file.UpdateFileMeta(token, edoc2MetaObjTypeList, out lastVerId);
            //IEDoc2MetaValue metaValue;
            //List<IEDoc2MetaValue> valueList;
            //int result = ApiManager.Api.MetaDataManagement.GetMetaValue(ApiManager.CurrentUserToken, 84, EDoc2ApiConst.OBJECT_FILE, 3, out valueList);
            //Assert.AreEqual(0, result);
            //Assert.IsNotNull(valueList);
            //Assert.IsNotNull(valueList.Find(x => x.AttrId == 9));

            //List<IEDoc2MetaValue> valueList;
            //int result = ApiManager.Api.MetaDataManagement.GetMetaValue(ApiManager.CurrentUserToken, file.FileLastVerId, EDoc2ApiConst.OBJECT_FILE_VERSION, 3, out valueList);
            //Assert.AreEqual(0, result);
            //Assert.IsNotNull(valueList);
            //Assert.Greater(valueList.Count10);
            //Assert.IsNotNull(valueList[4].AttrValue);

            //metaValue = null;
            //result = ApiManager.Api.EDoc2MetaDataManagement.GetMetaValue(ApiManager.CurrentUserToken, EDoc2ApiConst.OBJECT_FILE, 56, 9, out metaValue, MetaRange.DOC);
            //Assert.AreEqual(0, result);
            //Assert.IsNull(metaValue);
        }

        [Test]
        public void Test1()
        {
            ApiManager.ServerIp = "192.168.1.168";

            EDoc2MetadataHelper.CalculateExpireInfo(86);
        }
    }
}
