using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    internal class ZuzhiChengyuanHelper
    {
        public static ZuzhiChengyuan Chuangjian(int chengyuanId, string chengyuanMingcheng, ChengyuanLeixing chengyuanLeixing)
        {
            ZuzhiChengyuan chengyuan = null;
            if (chengyuanLeixing == ChengyuanLeixing.Yonghuzu)
            {
                chengyuan = new Yonghuzu(chengyuanId, chengyuanMingcheng);
            }
            else if (chengyuanLeixing == ChengyuanLeixing.Bumen)
            {
                chengyuan = new Bumen(chengyuanId, chengyuanMingcheng);
            }
            else
            {
                throw new ArgumentException("找不到成员类型:" + (int)chengyuanLeixing);
            }
            return chengyuan;
        }
    }
}
