using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using EDoc2.Website;

namespace EDoc2.YCT.Website
{
    public class YCTLogin : EDoc2.Website.Login
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sAction = PageHelper.GetQueryStringValue("action", "");
            if (sAction.ToLower() == "login")
            {
                string sUserName = PageHelper.GetFormValue("username", "");
                string sPassword = PageHelper.GetFormValue("password", "");

                if ((!string.IsNullOrEmpty(sUserName))
                    && (!string.IsNullOrEmpty(sPassword))
                    && (WebsiteUtility.Login(sUserName, sPassword) == EDoc2ApiConst.ERROR_SUCCEEDED))
                {
                    string sDefaultPage = WebsiteConfig.EDoc2DefaultPageUrl;
                    Response.Redirect(sDefaultPage);
                    Response.End();
                }
            }

            EDoc2.LicenseInfo licenseInfo = EDoc2.Website.WebControls.SystemManager.LicenseInfo;
            if (!licenseInfo.VerifiedOk
                || (licenseInfo.ExpiredTime != DateTime.MinValue && licenseInfo.ExpiredTime < DateTime.Today))
            {
                this.clientResMng.RegisterScriptBlock("var RedirectUrl = 'Activate.aspx';" +
                        "JueKit.UI.MessageBox.showMessage({" +
                                "text:'" + SR.GetString("inValidLicenseOrExpiredTime") + "'," +
                                "icon:'warning'," +
                                "onClose : {" +
                                    "handler : function()" +
                                    "{" +
                                        "window.location.href = RedirectUrl;" +
                                    "}," +
                                    "scope : null" +
                                "}" +
                           " });");
            }
            else//防止进行多次判断以及降低性能
            {
                //List<EDoc2UserInfo> listUser;
                //int nResult = ApiManager.Api.OrgnizationManagement.GetAllUserInfo(out listUser);
                //if (listUser != null)//当用户过多时跳转到激活License页面
                //{
                //    if (licenseInfo.MaxUsers < listUser.Count)
                //    {
                //        this.clientResMng.RegisterScriptBlock("var RedirectUrl = 'Activate.aspx';" +
                //            "JueKit.UI.MessageBox.showMessage({" +
                //                    "text:'" + SR.GetString("License_MoreUsers") + "'," +
                //                    "icon:'warning'," +
                //                    "onClose : {" +
                //                        "handler : function()" +
                //                        "{" +
                //                            "window.location.href = RedirectUrl;" +
                //                        "}," +
                //                        "scope : null" +
                //                    "}" +
                //               " });");
                //    }
                //}
            }


            if (!JueKit.Web.WebUtility.IsPostBack())
            {
                WebsiteUtility.Login();
                rcpMain.SetClientProperty("edoc2DefaultPageUrl", ResolveClientUrl(WebsiteConfig.EDoc2DefaultPageUrl));
                string strEDoc2StyleLibPath = WebsiteConfig.EDoc2CurrentUserStyleLibPath;
                string strEDoc2ScriptLibPath = WebsiteConfig.EDoc2ScriptLibPath;
                string strCurLng = WebsiteUtility.CurUserLanguage;

#if DEBUG
                this.clientResMng.RegisterCssLink("~/Styles/Default/Login.css");
                this.clientResMng.RegisterCssLink("~/Styles/Default/Login_{1}.css");
                this.clientResMng.RegisterScriptFile("~/Js/EDoc2.js");
                this.clientResMng.RegisterScriptFile("~/Js/SR/EDoc2_LoginSR_{0}.js");
                this.clientResMng.RegisterScriptFile("~/Js/Pages/Login.js");
                this.clientResMng.RegisterScriptFile("~/Js/Pages/ChangePasswordDlg.js");
#else
                this.clientResMng.RegisterCssLink(strEDoc2StyleLibPath + "Login.css");
                if (strCurLng == "en" || strCurLng == "ja")
                {
                    this.clientResMng.RegisterCssLink(strEDoc2StyleLibPath + "Login_{1}.css");
                }
                this.clientResMng.RegisterScriptFile(strEDoc2ScriptLibPath + "EDoc2.js");
                this.clientResMng.RegisterScriptFile(strEDoc2ScriptLibPath + "SR/EDoc2LoginSR_{0}.js");
                this.clientResMng.RegisterScriptFile(strEDoc2ScriptLibPath + "EDoc2_Page_Login.js");
#endif
                this.Title = SR.GetString("login_title") + " - " + SR.GetString("productName");
                this.rcpMain.SetClientProperty("retUrl", Request.QueryString["retUrl"]);
            }
        }
    }
}