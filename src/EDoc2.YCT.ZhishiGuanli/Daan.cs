using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class Daan
    {
        public Daan(int id, string neirong, List<FujianXinxi> fujianList)
        {
            this.Id = id;
            this.Neirong = neirong;
            this._fujianList = fujianList;
            if (this._fujianList == null)
            {
                this._fujianList = new List<FujianXinxi>();
            }
        }

        public int Id { private set; get; }

        public string Neirong { private set; get; }

        List<FujianXinxi> _fujianList;
        public List<FujianXinxi> FujianList
        {
            get
            {
                return _fujianList.ToList();
            }
        }

        public event TEventHandler<Daan> Xiugaihou;

        public void Xiugai(string neirong, List<FujianXinxi> fujian)
        {
            DaanDataModel dataModel = NHibernateHelper.CurrentSession.Get<DaanDataModel>(this.Id);
            dataModel.Neirong = neirong;
            if (fujian == null)
            {
                dataModel.FujianJson = null;
            }
            else
            {
                dataModel.FujianJson = Newtonsoft.Json.JsonConvert.SerializeObject(fujian);
            }

            NHibernateHelper.CurrentSession.Update(dataModel);
            NHibernateHelper.CurrentSession.Flush();

            this.Neirong = neirong;
            this._fujianList = fujian;

            if (this.Xiugaihou != null)
            {
                this.Xiugaihou(this);
            }
        }

        public bool YouFujian(string mingcheng)
        {
            foreach (FujianXinxi fujian in _fujianList)
            {
                if (fujian.Mingcheng.IndexOf(mingcheng, StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    return true;
                }
            }
            return false;
        }

        internal void Shanchu()
        {
            DaanDataModel dataModel = NHibernateHelper.CurrentSession.Get<DaanDataModel>(this.Id);
            NHibernateHelper.CurrentSession.Delete(dataModel);
            NHibernateHelper.CurrentSession.Flush();
        }
    }
}
