using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCT.Website.Models
{
    public class FujianModel
    {
        public FujianModel()
        {

        }

        public FujianModel(FujianXinxi fujianXinxi)
        {
            this.edoc2Id = fujianXinxi.EDoc2Id;
            this.mingcheng = fujianXinxi.Mingcheng;
        }

        public int edoc2Id;
        public string mingcheng;

        public FujianXinxi Map()
        {
            return new FujianXinxi { EDoc2Id = this.edoc2Id, Mingcheng = this.mingcheng };
        }
    }
}