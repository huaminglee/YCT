using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class ZhishiDataModel
    {
        public virtual int Id { set; get; }

        public virtual int Mulu { set; get; }

        public virtual string Wenti { set; get; }

        public virtual int YijiDaan { set; get; }

        public virtual int ErjiDaan { set; get; }

        public virtual int SanjiDaan { set; get; }

        public virtual int SijiDaan { set; get; }

        public virtual int WujiDaan { set; get; }

        public virtual string Chuangjianren { set; get; }

        public virtual DateTime ChuangjianShijian { set; get; }

        public virtual string Xiugairen { set; get; }

        public virtual DateTime? XiugaiShijian { set; get; }

        public virtual int Banben { set; get; }

        public virtual int? Shunxu { set; get; }

        public virtual bool? Zhidingde { set; get; }
    }
}
