using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCT.Website.Models
{
    public class ZhishiModel
    {
        public string banbenhao;
        public string bianjiren;
        public string chuangjianShijian;
        public string chuangjianren;
        public string erjiDaan;
        public List<FujianModel> erjiDaanFujian;
        public int id;
        public bool kebianji;
        public string sanjiDaan;
        public List<FujianModel> sanjiDaanFujian;
        public int? shunxu;
        public string sijiDaan;
        public List<FujianModel> sijiDaanFujian;
        public string wenti;
        public string wentiBiaoti;
        public string wujiDaan;
        public List<FujianModel> wujiDaanFujian;

        public string xiugaiShijian;
        public string xiugairen;
        public string yijiDaan;
        public List<FujianModel> yijiDaanFujian;
        public bool youErjiDaanGuanliQuanxian;
        public bool youSanjiDaanGuanliQuanxian;
        public bool youSijiDaanGuanliQuanxian;
        public bool youWujiDaanGuanliQuanxian;
        public bool youYijiDaanGuanliQuanxian;

        public ZhishiModel(Zhishi zhishi)
        {
            string yonghu = WebHelper.DangqianYonghuZhanghao;
            InitializeDaanGuanliQuanxian(zhishi, yonghu);

            banbenhao = zhishi.Banben.ToString(CultureInfo.InvariantCulture) + ".0";
            chuangjianren = WebHelper.GetYonghuXingming(zhishi.Chuangjianren);
            chuangjianShijian = zhishi.ChuangjianShijian.ToString("yyyy-MM-dd");
            //string yonghu = WebHelper.DangqianYonghuZhanghao;
            if (zhishi.YijiDaan != null && youYijiDaanGuanliQuanxian)
            {
                yijiDaan = zhishi.YijiDaan.Neirong;
                yijiDaanFujian = zhishi.YijiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            if (zhishi.ErjiDaan != null && youErjiDaanGuanliQuanxian)
            {
                erjiDaan = zhishi.ErjiDaan.Neirong;
                erjiDaanFujian = zhishi.ErjiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            id = zhishi.Id;
            if (zhishi.SanjiDaan != null && youSanjiDaanGuanliQuanxian)
            {
                sanjiDaan = zhishi.SanjiDaan.Neirong;
                sanjiDaanFujian = zhishi.SanjiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            if (zhishi.SijiDaan != null && youSijiDaanGuanliQuanxian)
            {
                sijiDaan = zhishi.SijiDaan.Neirong;
                sijiDaanFujian = zhishi.SijiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            if (zhishi.WujiDaan != null && youWujiDaanGuanliQuanxian)
            {
                wujiDaan = zhishi.WujiDaan.Neirong;
                wujiDaanFujian = zhishi.WujiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            wentiBiaoti = zhishi.Wenti;
            if (zhishi.Zhidingde.HasValue && zhishi.Zhidingde.Value)
            {
                wentiBiaoti = "<font color='red'>【置顶】</font>" + wentiBiaoti;
            }
            wenti = zhishi.Wenti;
            bianjiren = WebHelper.GetYonghuXingming(zhishi.Bianjiren);
            if (zhishi.Bianjizhong)
            {
                kebianji = zhishi.Bianjiren == WebHelper.DangqianYonghuZhanghao;
            }
            else
            {
                kebianji = true;
            }
            shunxu = zhishi.Shunxu;
            xiugairen = zhishi.Xiugairen;
            if (zhishi.XiugaiShijian.HasValue)
            {
                xiugaiShijian = zhishi.XiugaiShijian.Value.ToString("yyyy-MM-dd");
            }
        }

        private void InitializeDaanGuanliQuanxian(Zhishi zhishi,string yonghu)
        {
            Mulu mulu = zhishi.Mulu;
            youYijiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.YijiDaan);
            youErjiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.ErjiDaan);
            youSanjiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.SanjiDaan);
            youSijiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.SijiDaan);
            youWujiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(yonghu, QuanxianZhi.WujiDaan);
        }
    }
}