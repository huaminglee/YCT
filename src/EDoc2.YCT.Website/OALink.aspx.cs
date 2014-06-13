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
    public partial class OALink : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OALoginSettingInfo info = NHibernateHelper.CurrentSession.QueryOver<OALoginSettingInfo>()
                    .Where(x => x.UserId == WebsiteUtility.CurrentUser.UserId)
                    .List().FirstOrDefault();
            if (info != null)
            {
                this.Response.Write(string.Format("http://10.240.10.3/iOffice/prg/set/wss/ioLogin.aspx?loginid={0}&pwd={1}", info.OAUserName, info.OAUserPassword));
                this.Response.End();
            }
            else
            {
                this.Response.Write("http://10.240.10.3/ioffice/Login.aspx");
                this.Response.End();
            }
        }
    }
}