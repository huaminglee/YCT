using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDoc2.Organization;
using EDoc2.Website;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class Bumen : ZuzhiChengyuan
    {
        public Bumen(int id, string mingcheng)
            :base(id, mingcheng)
        {
            
        }

        public override bool Contians(string yonghu)
        {

#if DEBUG
            if (yonghu == "ceshi1")
            {
                return true;
            }
#else
            List<EDoc2UserInfo> userList = this.DiguiHuoquBumenYonghu(this.Id);
            if (userList.Any(x => x.UserLoginName.Equals(yonghu, StringComparison.InvariantCultureIgnoreCase)))
            {
                return true;
            }
#endif
            return false;
        }

        private List<EDoc2UserInfo> DiguiHuoquBumenYonghu(int bumen)
        {
            List<EDoc2UserInfo> userList;
            ApiManager.Api.OrgnizationManagement.GetChildUsersInDepartment(ApiManager.CurrentUserToken, bumen, out userList);
            if (userList == null)
            {
                userList = new List<EDoc2UserInfo>();
            }

            List<EDoc2DepartmentInfo> departmentList;
            ApiManager.Api.OrgnizationManagement.GetChildDepartments(ApiManager.CurrentUserToken, bumen, out departmentList);
            foreach (EDoc2DepartmentInfo departmentInfo in departmentList)
            {
                userList.AddRange(this.DiguiHuoquBumenYonghu(departmentInfo.DeptId));
            }

            return userList;
        }

        public override ChengyuanLeixing Leixing
        {
            get { return ChengyuanLeixing.Bumen; }
        }
    }
}
