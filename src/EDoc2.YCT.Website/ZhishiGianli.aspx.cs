using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDoc2.YCT.Website
{
    public partial class ZhishiGianli : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);

#if DEBUG
            writer.Write("<script language='javascript' type='text/javascript'>jQuery.extend({debug: true});</script>");
#endif
        }
    }
}