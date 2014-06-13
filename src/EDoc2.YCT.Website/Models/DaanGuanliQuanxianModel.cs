using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCT.Website.Models
{
    public class DaanGuanliQuanxianModel
    {
        public bool youErjiDaanGuanliQuanxian;
        public bool youSanjiDaanGuanliQuanxian;
        public bool youSijiDaanGuanliQuanxian;
        public bool youWujiDaanGuanliQuanxian;
        public bool youYijiDaanGuanliQuanxian;

        public DaanGuanliQuanxianModel(Mulu mulu)
        {
            string yonghu = WebHelper.DangqianYonghuZhanghao;
            youYijiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.YijiDaan);
            youErjiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.ErjiDaan); ;
            youSanjiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.SanjiDaan); ;
            youSijiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.SijiDaan); ;
            youWujiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.WujiDaan); ;
        }
    }
}