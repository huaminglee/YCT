using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class Quanxian
    {
        public Quanxian(int id, QuanxianZhi zhi, ZuzhiChengyuan chengyuan)
        {
            this.Id = id;
            this.Zhi = zhi;
            this.Chengyuan = chengyuan;
        }

        public int Id { private set; get; }

        public QuanxianZhi Zhi { private set; get; }

        public ZuzhiChengyuan Chengyuan { private set; get; }

        public bool Youquanxian(string yonghu, QuanxianZhi zhi)
        {
            if (this.Chengyuan.Contians(yonghu) && this.Zhi.HasFlag(zhi))
            {
                return true;
            }
            return false;
        }

        public event TEventHandler<Quanxian> ShanchuHou;

        public void Shanchu()
        {
            QuanxianDataModel model = NHibernateHelper.CurrentSession.Get<QuanxianDataModel>(this.Id);
            NHibernateHelper.CurrentSession.Delete(model);
            NHibernateHelper.CurrentSession.Flush();
            if (this.ShanchuHou != null)
            {
                this.ShanchuHou(this);
            }
        }

        public void Xiugai(QuanxianZhi zhi)
        {
            QuanxianDataModel model = NHibernateHelper.CurrentSession.Get<QuanxianDataModel>(this.Id);
            model.Zhi = (int)zhi;
            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();
            this.Zhi = zhi;
        }
    }
}
