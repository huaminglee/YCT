using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace EDoc2.YCT.Zhishiku
{
    public class Mulu
    {
        public int Id { private set; get; }

        public string Mingcheng { private set; get; }

        public string Chuangjianren { private set; get; }

        public DateTime ChuangjianShijian { private set; get; }

        public Mulu FuMulu { private set; get; }

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

        public List<Mulu> GetZimuluList()
        {
            return null;
        }

        public List<Zhishi> GetZhishiList()
        {
            return null;
        }


    }
}
