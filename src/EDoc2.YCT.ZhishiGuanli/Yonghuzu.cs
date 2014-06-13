using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDoc2.Organization;
using EDoc2.Website;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class Yonghuzu : ZuzhiChengyuan
    {
        public Yonghuzu(int id, string mingcheng)
            :base(id, mingcheng)
        {
            
        }

        public override bool Contians(string yonghu)
        {
            
#if DEBUG
            if (yonghu == "ceshi")
            {
                return true;
            }
#else
            List<EDoc2UserInfo> userList;
            ApiManager.Api.OrgnizationManagement.GetChildUsersInUserGroup(ApiManager.CurrentUserToken, this.Id, out userList);
            if (userList != null && userList.Any(x => x.UserLoginName.Equals(yonghu, StringComparison.InvariantCultureIgnoreCase)))
            {
                return true;
            }
#endif
            return false;
        }

        public override ChengyuanLeixing Leixing
        {
            get { return ChengyuanLeixing.Yonghuzu; }
        }
    }
}
