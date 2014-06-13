using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class LishiZhishi
    {
        public LishiZhishi(int id, string wenti, Daan yijiDaan, Daan erjiDaan, Daan sanjiDaan, Daan sijiDaan, Daan wujiDaan,
            string chuangjianren, DateTime chuangjianShijian, int banben)
        {
            this.Id = id;
            this.Wenti = wenti;
            this.YijiDaan = yijiDaan;
            this.ErjiDaan = erjiDaan;
            this.SanjiDaan = sanjiDaan;
            this.SijiDaan = sijiDaan;
            this.WujiDaan = wujiDaan;
            this.Chuangjianren = chuangjianren;
            this.ChuangjianShijian = chuangjianShijian;
            this.Banben = banben;
        }

        public int Id { private set; get; }

        public Zhishi Zhishi { internal set; get; }

        public string Wenti { private set; get; }

        public Daan YijiDaan { private set; get; }

        public Daan ErjiDaan { private set; get; }

        public Daan SanjiDaan { private set; get; }

        public Daan SijiDaan { private set; get; }

        public Daan WujiDaan { private set; get; }

        public string Chuangjianren { private set; get; }

        public DateTime ChuangjianShijian { private set; get; }

        public int Banben { private set; get; }
    }
}
