using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.Zhishiku
{
    public class Zhishi
    {
        public int Id { private set; get; }

        public string Wenti { private set; get; }

        public Daan YijiDaan { private set; get; }

        public Daan ErjiDaan { private set; get; }

        public Daan SanjiDaan { private set; get; }

        public Daan SijiDaan { private set; get; }

        public Daan WujiDaan { private set; get; }

        public string Chuangjianren { private set; get; }

        public DateTime ChuangjianShijian { private set; get; }

        public string Banbenhao { private set; get; }

        public int Shunxu { private set; get; }

        public List<Quanxian> GetYijiDaanQuanxian()
        {
            return null;
        }

        public List<Quanxian> GetErjiDaanQuanxian()
        {
            return null;
        }

        public List<Quanxian> GetSanjiDaanQuanxian()
        {
            return null;
        }

        public List<Quanxian> GetSijiDaanQuanxian()
        {
            return null;
        }

        public List<Quanxian> GetWujiDaanQuanxian()
        {
            return null;
        }

        public List<Zhishi> GetLishiBanben()
        {
            return null;
        }
    }
}
