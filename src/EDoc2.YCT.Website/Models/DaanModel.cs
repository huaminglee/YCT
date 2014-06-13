using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCT.Website.Models
{
    public class DaanModel
    {
        public DaanModel(Daan daan)
        {
            if (daan.FujianList != null)
            {
                this.fujian = daan.FujianList.Select(x => new FujianModel(x)).ToList();
            }
            this.id = daan.Id;
            this.neirong = daan.Neirong;
        }

        public int id;

        public string neirong;

        public List<FujianModel> fujian;
    }
}