using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    [Flags]
    public enum QuanxianZhi
    {
        YijiDaan = 1,
        ErjiDaan = 2,
        SanjiDaan = 4,
        SijiDaan = 8,
        WujiDaan = 16,
        Guanli = 32,
    }
}
