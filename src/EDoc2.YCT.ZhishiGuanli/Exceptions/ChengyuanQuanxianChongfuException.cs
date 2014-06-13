using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli.Exceptions
{
    public class ChengyuanQuanxianChongfuException : ZhishikuException
    {
        public override string Message
        {
            get
            {
                return "成员权限已经存在！";
            }
        }
    }
}
