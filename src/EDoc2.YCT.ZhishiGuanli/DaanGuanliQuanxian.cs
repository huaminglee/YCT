using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class DaanGuanliQuanxian
    {
        public DaanGuanliQuanxian(int id, QuanxianZhi zhi, ZuzhiChengyuan chengyuan)
        {
            Id = id;
            Zhi = zhi;
            Chengyuan = chengyuan;
        }

        public int Id { private set; get; }

        public QuanxianZhi Zhi { private set; get; }

        public ZuzhiChengyuan Chengyuan { private set; get; }

        public bool Youquanxian(string yonghu, QuanxianZhi zhi)
        {
            if (Chengyuan.Contians(yonghu) && Zhi.HasFlag(zhi))
            {
                return true;
            }
            return false;
        }

        public event TEventHandler<DaanGuanliQuanxian> ShanchuHou;

        public void Shanchu()
        {
            var model = NHibernateHelper.CurrentSession.Get<DaanGuanliQuanxianDataModel>(Id);
            NHibernateHelper.CurrentSession.Delete(model);
            NHibernateHelper.CurrentSession.Flush();
            if (ShanchuHou != null)
            {
                ShanchuHou(this);
            }
        }

        public void Xiugai(QuanxianZhi zhi)
        {
            var model = NHibernateHelper.CurrentSession.Get<DaanGuanliQuanxianDataModel>(Id);
            model.Zhi = (int) zhi;
            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();
            Zhi = zhi;
        }
    }
}