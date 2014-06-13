using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDoc2.Document;
using EDoc2.Website;
using EDoc2.MetaData;
using EDoc2.Search;

namespace EDoc2.YCT.Website
{
    public partial class ExpiredContractMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strArgsXml = "<GetListArgs><PageNum>1</PageNum></GetListArgs>";
            if (strArgsXml == null)
            {
                strArgsXml = "";
            }
            int metaAttrId = this.GetExpiredDaysMetaAttrId();
            string searchXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Search><SearchCondition><SearchString><![CDATA[ ( +" + metaAttrId + ":{1 TO 61})]]></SearchString><SearchType>MetaFile</SearchType><SearchHistory>False</SearchHistory><SearchSort>default</SearchSort></SearchCondition><conditions><condition><id>" + metaAttrId + "</id><type>EDoc2.Meta.Number</type><compare>2</compare><relation>1</relation><value1><![CDATA[60]]></value1><value2><![CDATA[]]></value2></condition></conditions></Search>";

            int pageCount = 0;
            int actualPageSize = 0;
            int totalCount = 0;

            int nResult = 0;
            List<IEdoc2SearchResult> resultList;
            nResult = ApiManager.SearchApi.SearchServiceManagement.FileSearch(ApiManager.CurrentUserToken, 
                searchXml, 0, 10, out actualPageSize, out pageCount, out totalCount, out resultList);
            this.Response.Write(string.Format("有{0}份合同将到期", totalCount));
        }

        private int GetExpiredDaysMetaAttrId()
        {
            int typeId = this.GetMetaTypeId("合同基本信息");
            return this.GetMetaAttrId(typeId, "到期天数");
        }

        public int GetMetaTypeId(string typeName)
        {
            List<IEDoc2MetaType> metaTypeList;
            ApiManager.Api.MetaDataManagement.GetAllMetaType(ApiManager.CurrentUserToken, out metaTypeList);

            foreach (IEDoc2MetaType metaType in metaTypeList)
            {
                if (metaType.TypeName == typeName)
                {
                    return metaType.TypeId;
                }
            }
            return 0;
        }

        public int GetMetaAttrId(int typeId, string attrName)
        {
            List<IEDoc2MetaAttr> metaAttrList = null;
            ApiManager.Api.MetaDataManagement.GetMetaAttrByTypeId(ApiManager.CurrentUserToken,
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
    }
}