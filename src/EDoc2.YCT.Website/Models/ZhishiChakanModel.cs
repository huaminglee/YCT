using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCT.Website.Models
{
    public class ZhishiChakanModel
    {
        public ZhishiChakanModel(Zhishi zhishi)
        {
            this.banbenhao = zhishi.Banben.ToString() + ".0";
            this.chuangjianren = WebHelper.GetYonghuXingming(zhishi.Chuangjianren);
            this.chuangjianShijian = zhishi.ChuangjianShijian.ToString("yyyy-MM-dd");
            string yonghu = WebHelper.DangqianYonghuZhanghao;
            if (zhishi.Mulu.YouYijiDaanQuanxian(yonghu) && zhishi.YijiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.YijiDaan);
            }
            if (zhishi.Mulu.YouErjiDaanQuanxian(yonghu) && zhishi.ErjiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.ErjiDaan);
            }
            if (zhishi.Mulu.YouSanjiDaanQuanxian(yonghu) && zhishi.SanjiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.SanjiDaan);
            }
            if (zhishi.Mulu.YouSijiDaanQuanxian(yonghu) && zhishi.SijiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.SijiDaan);
            }
            if (zhishi.Mulu.YouWujiDaanQuanxian(yonghu) && zhishi.WujiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.WujiDaan);
            }
            this.id = zhishi.Id;
            this.shunxu = zhishi.Shunxu;
            this.wenti = zhishi.Wenti;
        }
        public ZhishiChakanModel(LishiZhishi zhishi)
        {
            this.banbenhao = zhishi.Banben.ToString() + ".0";
            this.chuangjianren = WebHelper.GetYonghuXingming(zhishi.Chuangjianren);
            this.chuangjianShijian = zhishi.ChuangjianShijian.ToString("yyyy-MM-dd");
            string yonghu = WebHelper.DangqianYonghuZhanghao;
            this.daanHtml = "";
            if (zhishi.ErjiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.ErjiDaan);
            }
            this.id = zhishi.Id;
            if (zhishi.SanjiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.SanjiDaan);
            }
            if (zhishi.SijiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.SijiDaan);
            }
            this.wenti = zhishi.Wenti;
            if (zhishi.WujiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.WujiDaan);
            }
            if (zhishi.YijiDaan != null)
            {
                this.daanHtml += this.ShengchengHtml(zhishi.YijiDaan);
            }
        }

        private string ShengchengHtml(Daan daan)
        {
            string daanNeirong = daan.Neirong;
            if (daanNeirong != null)
            {
                daanNeirong = daanNeirong.Replace("\r\n", "<br />");
            }
            string daanNeirongHtml = "";
            if (!string.IsNullOrEmpty(daanNeirong))
            {
                daanNeirongHtml = string.Format("<h4 >答案：</h4><p>{0}</p>", daanNeirong);
            }
            string fujianHtml = this.ShengchengHtml(daan.FujianList);
            if (string.IsNullOrEmpty(daanNeirongHtml) && string.IsNullOrEmpty(fujianHtml))
            {
                return "";
            }
            return string.Format("<div>{0}<div>{1}</div><hr /></div>", daanNeirongHtml, fujianHtml);
        }

        private string ShengchengHtml(List<FujianXinxi> fujianList)
        {
            string html = "";
            if (fujianList.Count > 0)
            {
                html += "<h5>附件:</h5>";
            }
            foreach (FujianXinxi fujian in fujianList)
            {
                html += "<div>";
                html += string.Format("<a href='/preview.aspx?fileId={0}&key=yctZhishiku' target='_blank'>{1}&nbsp;</a>", fujian.EDoc2Id, fujian.Mingcheng);
                html += string.Format("<a href='/Document/File_Download.aspx?file_Id={0}&key=yctZhishiku' target='_blank' style='color: #256f95;'>下载</a>", fujian.EDoc2Id, fujian.Mingcheng);
                html += "</div>";
            }
            return html;
        }

        public int id;

        public string wenti;

        public string daanHtml;

        public string chuangjianren;

        public string chuangjianShijian;

        public string banbenhao;

        public int? shunxu;
    }
}