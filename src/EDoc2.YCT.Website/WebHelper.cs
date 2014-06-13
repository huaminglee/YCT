using System;
using System.Collections.Generic;
using System.Web;
using log4net;
using System.Configuration;
using EDoc2.Website;
using EDoc2.Organization;
using System.IO;
using EDoc2.YCT.ZhishiGuanli;
using EDoc2.YCT.Website.Models;

namespace EDoc2.YCT.Website
{
    public class WebHelper
    {
        static WebHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger = log4net.LogManager.GetLogger("yctLogger");
            Zhishiku = new Zhishiku();
        }

        public static ILog Logger { private set; get; }

        public static int ZhishikuWenjianjiaId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["zhishikuWenjianjiaId"]);
            }
        }

        public static Zhishiku Zhishiku { private set; get; }

        public static string DangqianYonghuZhanghao
        {
            get
            {
#if DEBUG
                return "ceshi1";
#else
                return WebsiteUtility.CurrentUser.UserLoginName;
#endif
            }
        }

        public static string GetYonghuXingming(string zhanghao)
        {
#if DEBUG
            return zhanghao;
#else
            EDoc2UserInfo userInfo;
            ApiManager.Api.OrgnizationManagement.GetUserByLoginName(ApiManager.CurrentUserToken, zhanghao, out userInfo);
            if (userInfo != null)
            {
                return userInfo.UserRealName;
            }
            else
            {
                return zhanghao;
            }
#endif
        }
    }
}