using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDoc2.Website;

namespace EDoc2.YCT.Website
{
    public partial class _Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int result = WebsiteUtility.Login(Request["userName"], Request["password"]);
            if (result == 0)
            {
                WebsiteUtility.Redirect("~/default.aspx");
            }
            else
            {
                WebsiteUtility.Redirect("~/Login.aspx");
            }
        }
    }
}