using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDoc2.YCT.Core;
using EDoc2.Website;


namespace EDoc2.YCT.Website
{
    public partial class OALoginSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (WebsiteUtility.CurrentUser == null)
            {
                lblMessage.Text = "请重新登录！";
                return;
            }
            try
            {
                OALoginSettingInfo info = NHibernateHelper.CurrentSession.QueryOver<OALoginSettingInfo>()
                    .Where(x => x.UserId == WebsiteUtility.CurrentUser.UserId)
                    .List().FirstOrDefault();
                if (info == null)
                {
                    info = new OALoginSettingInfo();
                    info.OAUserName = this.txtUserName.Text;
                    info.OAUserPassword = this.txtPassword.Text;
                    info.UserId = WebsiteUtility.CurrentUser.UserId;
                    NHibernateHelper.CurrentSession.Save(info);
                }
                else
                {
                    info.OAUserName = this.txtUserName.Text;
                    info.OAUserPassword = this.txtPassword.Text;
                    NHibernateHelper.CurrentSession.Update(info, info.Id);
                    NHibernateHelper.CurrentSession.Flush();
                }
                lblMessage.Text = "保存成功,重新登录后生效！";
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}