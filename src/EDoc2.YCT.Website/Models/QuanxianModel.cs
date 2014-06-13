using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCT.Website.Models
{
    public class QuanxianModel
    {
        public QuanxianModel(Quanxian quanxian, bool shiJichengDe)
        {
            this.id = quanxian.Id;
            this.chengyuanMingcheng = quanxian.Chengyuan.Mingcheng;
            this.youYijiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.YijiDaan) ? "有权限" : "无权限";
            this.youErjiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.ErjiDaan) ? "有权限" : "无权限";
            this.youSanjiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.SanjiDaan) ? "有权限" : "无权限";
            this.youSijiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.SijiDaan) ? "有权限" : "无权限";
            this.youWujiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.WujiDaan) ? "有权限" : "无权限";
            this.strShiJichengDe = shiJichengDe ? "是" : "否";
            this.shiJichengDe = shiJichengDe;
            this.zhi = (int)quanxian.Zhi;
        }

        public QuanxianModel(DaanGuanliQuanxian quanxian, bool shiJichengDe)
        {
            this.id = quanxian.Id;
            this.chengyuanMingcheng = quanxian.Chengyuan.Mingcheng;
            this.youYijiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.YijiDaan) ? "有权限" : "无权限";
            this.youErjiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.ErjiDaan) ? "有权限" : "无权限";
            this.youSanjiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.SanjiDaan) ? "有权限" : "无权限";
            this.youSijiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.SijiDaan) ? "有权限" : "无权限";
            this.youWujiDaanQuanxian = quanxian.Zhi.HasFlag(QuanxianZhi.WujiDaan) ? "有权限" : "无权限";
            this.strShiJichengDe = shiJichengDe ? "是" : "否";
            this.shiJichengDe = shiJichengDe;
            this.zhi = (int)quanxian.Zhi;
        }

        public int id;
        public string chengyuanMingcheng;
        public string youYijiDaanQuanxian;
        public string youErjiDaanQuanxian;
        public string youSanjiDaanQuanxian;
        public string youSijiDaanQuanxian;
        public string youWujiDaanQuanxian;
        public string strShiJichengDe;
        public bool shiJichengDe;
        public int zhi;
    }
}