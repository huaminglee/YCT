using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli.Exceptions
{
    public class DingjiMuluException : ZhishikuException
    {
        public override string Message
        {
            get
            {
                return "顶级目录不能删除!";
            }
        }
    }
}
