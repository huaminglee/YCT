using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class LishiZhishiDataModel
    {
        public LishiZhishiDataModel()
        {
        }

        public LishiZhishiDataModel(Zhishi zhishi)
        {
            this.Wenti = zhishi.Wenti;
            this.Banben = zhishi.Banben;
            this.Chuangjianren = zhishi.Chuangjianren;
            this.YijiDaan = zhishi.YijiDaan != null ? zhishi.YijiDaan.Id : 0;
            this.ErjiDaan = zhishi.ErjiDaan != null ? zhishi.ErjiDaan.Id : 0;
            this.SanjiDaan = zhishi.SanjiDaan != null ? zhishi.SanjiDaan.Id : 0;
            this.SijiDaan = zhishi.SijiDaan != null ? zhishi.SijiDaan.Id : 0;
            this.WujiDaan = zhishi.WujiDaan != null ? zhishi.WujiDaan.Id : 0;
            this.ZhishiId = zhishi.Id;
        }

        public virtual int Id { set; get; }

        public virtual int ZhishiId { set; get; }

        public virtual string Wenti { set; get; }

        public virtual int YijiDaan { set; get; }

        public virtual int ErjiDaan { set; get; }

        public virtual int SanjiDaan { set; get; }

        public virtual int SijiDaan { set; get; }

        public virtual int WujiDaan { set; get; }

        public virtual string Chuangjianren { set; get; }

        public virtual DateTime ChuangjianShijian { set; get; }

        public virtual int Banben { set; get; }
    }
}
