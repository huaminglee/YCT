using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class QuanxianDataModel
    {
        public virtual int Id { set; get; }

        public virtual int Mulu { set; get; }

        public virtual int Zhi { set; get; }

        public virtual int ChengyuanId { set; get; }

        public virtual string ChengyuanMingcheng { set; get; }

        public virtual int ChengyuanLeixing { set; get; }
    }
}
