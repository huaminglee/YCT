using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class ZhishiXinxi
    {
        public ZhishiXinxi(Zhishi zhishi)
        {
            this.Id = zhishi.Id;
            this.Wenti = zhishi.Wenti;
            this.Chuangjianren = zhishi.Chuangjianren;
            this.ChuangjianShijian = zhishi.ChuangjianShijian;
            this.Banben = zhishi.Banben;
            this.Shunxu = zhishi.Shunxu;
        }

        public int Id { private set; get; }

        public string Wenti { private set; get; }

        public string Chuangjianren { private set; get; }

        public DateTime ChuangjianShijian { private set; get; }

        public int Banben { private set; get; }

        public int? Shunxu { private set; get; }
    }
}
