using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCT.Website.Models
{
    public class LishiZhishiModel
    {

        public LishiZhishiModel(LishiZhishi lishiZhishi)
        {
            this.zhishiId = lishiZhishi.Zhishi.Id;
            this.banbenhao = lishiZhishi.Banben.ToString() + ".0";
            this.chuangjianren = WebHelper.GetYonghuXingming(lishiZhishi.Chuangjianren);
            this.chuangjianShijian = lishiZhishi.ChuangjianShijian.ToString("yyyy-MM-dd");
            string yonghu = WebHelper.DangqianYonghuZhanghao;
            if (lishiZhishi.ErjiDaan != null)
            {
                this.erjiDaan = lishiZhishi.ErjiDaan.Neirong;
                this.erjiDaanFujian = lishiZhishi.ErjiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            this.id = lishiZhishi.Id;
            if (lishiZhishi.SanjiDaan != null)
            {
                this.sanjiDaan = lishiZhishi.SanjiDaan.Neirong;
                this.sanjiDaanFujian = lishiZhishi.SanjiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            if (lishiZhishi.SijiDaan != null)
            {
                this.sijiDaan = lishiZhishi.SijiDaan.Neirong;
                this.sijiDaanFujian = lishiZhishi.SijiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            this.wenti = lishiZhishi.Wenti;
            if (lishiZhishi.WujiDaan != null)
            {
                this.wujiDaan = lishiZhishi.WujiDaan.Neirong;
                this.wujiDaanFujian = lishiZhishi.WujiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            if (lishiZhishi.YijiDaan != null)
            {
                this.yijiDaan = lishiZhishi.YijiDaan.Neirong;
                this.yijiDaanFujian = lishiZhishi.YijiDaan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
        }

        public int id;

        public int zhishiId;

        public string wenti;
        public string yijiDaan;
        public List<FujianModel> yijiDaanFujian;
        public string erjiDaan;
        public List<FujianModel> erjiDaanFujian;
        public string sanjiDaan;
        public List<FujianModel> sanjiDaanFujian;
        public string sijiDaan;
        public List<FujianModel> sijiDaanFujian;
        public string wujiDaan;
        public List<FujianModel> wujiDaanFujian;

        public string chuangjianren;

        public string chuangjianShijian;

        public string banbenhao;
    }
}