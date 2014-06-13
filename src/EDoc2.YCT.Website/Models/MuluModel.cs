using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCT.Website.Models
{
    public class MuluModel
    {
        public MuluModel(Mulu mulu)
        {
            this.chuangjianren = WebHelper.GetYonghuXingming(mulu.Chuangjianren);
            this.chuangjianShijian = mulu.ChuangjianShijian.ToString("yyyy-MM-dd");
            this.id = mulu.Id;
            this.mingcheng = mulu.Mingcheng;
            if (mulu.FuMulu != null)
            {
                this.fuMuluId = mulu.FuMulu.Id;
            }
        }

        public int id;

        public string mingcheng;

        public string chuangjianren;

        public string chuangjianShijian;

        public int fuMuluId;
    }
}