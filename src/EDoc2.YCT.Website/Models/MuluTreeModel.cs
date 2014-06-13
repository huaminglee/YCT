using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCT.Website.Models
{
    public class MuluTreeModel
    {
        public MuluTreeModel(Mulu mulu)
        {
            this.id = mulu.Id;
            this.text = mulu.Mingcheng;
            this.iconClass = "icon-folder";
        }

        public int id;

        public string text;

        public string iconClass;
    }
}