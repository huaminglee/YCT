using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class MuluDataModel
    {
        public virtual int Id { set; get; }

        public virtual string Mingcheng { set; get; }

        public virtual string Chuangjianren { set; get; }

        public virtual DateTime ChuangjianShijian { set; get; }

        public virtual int FuMulu { set; get; }
    }
}
