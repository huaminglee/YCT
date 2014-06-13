using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class DaanDataModel
    {
        public virtual int Id { set; get; }

        public virtual int ZhishiId { set; get; }

        public virtual string Neirong { set; get; }

        public virtual string FujianJson { set; get; }
    }
}
