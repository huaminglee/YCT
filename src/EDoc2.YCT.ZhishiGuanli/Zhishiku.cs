using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class Zhishiku
    {
        private List<Mulu> _muluList;
        private List<Zhishi> _zhishiList;

        public Zhishiku()
        {
            this._muluList = new List<Mulu>();
            this._zhishiList = new List<Zhishi>();
            this.Jiazai();
            if (this.DingjiMulu == null)
            {
                this.ChuangjianDingjiMulu("知识库", "system");
            }
        }

        public Mulu DingjiMulu { private set; get; }

        public Mulu GetMulu(int muluId)
        {
            return this._muluList.Find(x => x.Id == muluId);
        }

        public Zhishi GetZhishi(int zhishiId)
        {
            return this._zhishiList.Find(x => x.Id == zhishiId);
        }

        private object _lock = new object();

        private void ChuangjianDingjiMulu(string mingcheng, string chuangjianren)
        {
            lock (_lock)
            {
                MuluDataModel model = new MuluDataModel();
                model.Chuangjianren = chuangjianren;
                model.ChuangjianShijian = DateTime.Now;
                model.Mingcheng = mingcheng;
                int id = (int)NHibernateHelper.CurrentSession.Save(model);

                Mulu mulu = new DingjiMulu(id, mingcheng, chuangjianren, model.ChuangjianShijian, null, null, null,null);
                List<Mulu> muluList = this._muluList.ToList();
                muluList.Add(mulu);
                this._muluList = muluList;
                this.DingjiMulu = mulu;
                this.BangdingMuluShijian(mulu);
            }
        }

        private void BangdingMuluShijian(Mulu mulu)
        {
            mulu.ChuangjianZiMuluHou += new TEventHandler<Mulu, Mulu>(ChuangjianMuluHou);
            mulu.ShanchuHou += new TEventHandler<Mulu>(Mulu_ShanchuHou);
            mulu.ChuangjianZhishihou += new TEventHandler<Mulu, Zhishi>(ChuangjianZhishihou);
        }

        private void BangdingZhishiShijian(Zhishi zhishi)
        {
            zhishi.Shanchuhou += new TEventHandler<Zhishi>(Zhishi_Shanchuhou);
        }

        void Zhishi_Shanchuhou(Zhishi args)
        {
            lock (_lock)
            {
                List<Zhishi> zhishiList = this._zhishiList.ToList();
                zhishiList.Remove(args);
                this._zhishiList = zhishiList;
            }
        }

        void ChuangjianZhishihou(Mulu sender, Zhishi args)
        {
            lock (_lock)
            {
                List<Zhishi> zhishiList = this._zhishiList.ToList();
                zhishiList.Add(args);
                this._zhishiList = zhishiList;
                this.BangdingZhishiShijian(args);
            }
        }

        void ChuangjianMuluHou(Mulu sender, Mulu args)
        {
            lock (_lock)
            {
                this.BangdingMuluShijian(args);
                List<Mulu> muluList = this._muluList.ToList();
                muluList.Add(args);
                this._muluList = muluList;
            }
        }

        void Mulu_ShanchuHou(Mulu args)
        {
            lock (_lock)
            {
                List<Mulu> muluList = this._muluList.ToList();
                muluList.Remove(args);
                this._muluList = muluList;
            }
        }

        private void Jiazai()
        {
            MuluDataModel dingJimuluDataModel  = NHibernateHelper.CurrentSession.QueryOver<MuluDataModel>().Where(x => x.FuMulu == 0).SingleOrDefault();
            if (dingJimuluDataModel != null)
            {
                this.DingjiMulu = this.JiazaiMulu(dingJimuluDataModel);

                this.ShezhiFuMulu(this.DingjiMulu);
            }
        }

        private void ShezhiFuMulu(Mulu mulu)
        {
            List<Mulu> ziMuluList = mulu.GetZiMuluList();
            foreach (Mulu zimulu in ziMuluList)
            {
                zimulu.FuMulu = mulu;
                this.ShezhiFuMulu(zimulu);
            }
        }

        private Mulu JiazaiMulu(MuluDataModel model)
        {
            Mulu mulu = this.GetMulu(model.Id);
            if (mulu == null)
            {
                List<Mulu> ziMuluList = this.JiazaiZiMulu(model.Id);
                List<Quanxian> quanxianList = this.JiazaiQuanxian(model.Id);
                List<DaanGuanliQuanxian> daanGuanliQuanxianList = this.JiazaiDaanGuanliQuanxian(model.Id);
                List<Zhishi> zhishiList = this.JiazaiZhishi(model.Id);
                if (model.FuMulu == 0)
                {
                    mulu = new DingjiMulu(model.Id, model.Mingcheng, model.Chuangjianren, model.ChuangjianShijian, quanxianList, ziMuluList, zhishiList, daanGuanliQuanxianList);
                }
                else
                {
                    mulu = new Mulu(model.Id, model.Mingcheng, model.Chuangjianren, model.ChuangjianShijian, quanxianList, ziMuluList, zhishiList, daanGuanliQuanxianList);
                }
                this._muluList.Add(mulu);
                this.BangdingMuluShijian(mulu);
            }
            return mulu;
        }

        private List<Mulu> JiazaiZiMulu(int fuMuluId)
        {
            List<Mulu> muluList = new List<Mulu>();

            List<MuluDataModel> ziMuluDataModels = NHibernateHelper.CurrentSession.QueryOver<MuluDataModel>().Where(x => x.FuMulu == fuMuluId).List().ToList();
            foreach (MuluDataModel model in ziMuluDataModels)
            {
                muluList.Add(this.JiazaiMulu(model));
            }

            return muluList;
        }

        private List<Quanxian> JiazaiQuanxian(int muluId)
        {
            List<Quanxian> list = new List<Quanxian>();

            List<QuanxianDataModel> dataModels = NHibernateHelper.CurrentSession.QueryOver<QuanxianDataModel>().Where(x => x.Mulu == muluId).List().ToList();
            foreach (QuanxianDataModel model in dataModels)
            {
                List<string> yonghuList = new List<string>();
                ZuzhiChengyuan zuzhiChengyuan = ZuzhiChengyuanHelper.Chuangjian(model.ChengyuanId, model.ChengyuanMingcheng, (ChengyuanLeixing)model.ChengyuanLeixing);
                Quanxian quanxian = new Quanxian(model.Id, (QuanxianZhi)model.Zhi, zuzhiChengyuan);
                list.Add(quanxian);
            }

            return list;
        }

        private List<Zhishi> JiazaiZhishi(int muluId)
        {
            List<Zhishi> zhishiList = new List<Zhishi>();
            List<ZhishiDataModel> dataModels = NHibernateHelper.CurrentSession.QueryOver<ZhishiDataModel>().Where(x => x.Mulu == muluId).List().ToList();
            foreach (ZhishiDataModel model in dataModels)
            {
                List<LishiZhishi> lishiZhishi = this.JiazaiLishiZhishi(model.Id);
                Zhishi zhishi = new Zhishi(model.Id, model.Wenti, this.JiazaiDaan(model.YijiDaan), this.JiazaiDaan(model.ErjiDaan),
                    this.JiazaiDaan(model.SanjiDaan), this.JiazaiDaan(model.SijiDaan), this.JiazaiDaan(model.WujiDaan), model.Chuangjianren,
                    model.ChuangjianShijian, model.Banben, model.Shunxu, lishiZhishi, model.Xiugairen, model.XiugaiShijian, model.Zhidingde);
                zhishiList.Add(zhishi);
                this._zhishiList.Add(zhishi);
                this.BangdingZhishiShijian(zhishi);
            }
            return zhishiList;
        }

        private List<LishiZhishi> JiazaiLishiZhishi(int zhishiId)
        {
            List<LishiZhishi> lishiZhishiList = new List<LishiZhishi>();
            List<LishiZhishiDataModel> dataModels = NHibernateHelper.CurrentSession.QueryOver<LishiZhishiDataModel>().Where(x => x.ZhishiId == zhishiId).List().ToList();
            foreach (LishiZhishiDataModel model in dataModels)
            {
                LishiZhishi zhishi = new LishiZhishi(model.Id, model.Wenti, this.JiazaiDaan(model.YijiDaan), this.JiazaiDaan(model.ErjiDaan),
                    this.JiazaiDaan(model.SanjiDaan), this.JiazaiDaan(model.SijiDaan), this.JiazaiDaan(model.WujiDaan), model.Chuangjianren,
                    model.ChuangjianShijian, model.Banben);
                lishiZhishiList.Add(zhishi);
            }
            return lishiZhishiList;
        }

        private Daan JiazaiDaan(int daanId)
        {
            DaanDataModel model = NHibernateHelper.CurrentSession.Get<DaanDataModel>(daanId);
            NHibernateHelper.CurrentSession.Flush();
            if (model != null)
            {
                List<FujianXinxi> fujian = null;
                if (!string.IsNullOrEmpty(model.FujianJson))
                {
                    fujian = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FujianXinxi>>(model.FujianJson);
                }
                Daan daan = new Daan(model.Id, model.Neirong, fujian);
                return daan;
            }
            return null;
        }

        internal static Daan ChuangjianDaan(string neirong, List<FujianXinxi> fujianList)
        {
            DaanDataModel model = new DaanDataModel();
            model.Neirong = neirong;
            if (fujianList != null)
            {
                model.FujianJson = Newtonsoft.Json.JsonConvert.SerializeObject(fujianList);
            }
            model.Id = (int)NHibernateHelper.CurrentSession.Save(model);

            Daan daan = ChuangjianDaan(model);
            return daan;
        }

        internal static Daan ChuangjianDaan(DaanDataModel model)
        {
            List<FujianXinxi> fujianList = null;
            if (!string.IsNullOrEmpty(model.FujianJson))
            {
                fujianList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FujianXinxi>>(model.FujianJson);
            }
            Daan daan = new Daan(model.Id, model.Neirong, fujianList);
            return daan;
        }

        #region DaanGuanliQuanxian

        private List<DaanGuanliQuanxian> JiazaiDaanGuanliQuanxian(int muluId)
        {
            var daanGuanliQuanxianList = new List<DaanGuanliQuanxian>();
            List<DaanGuanliQuanxianDataModel> dataModels =
                NHibernateHelper.CurrentSession.QueryOver<DaanGuanliQuanxianDataModel>()
                                .Where(x => x.Mulu == muluId)
                                .List()
                                .ToList();
            foreach (DaanGuanliQuanxianDataModel model in dataModels)
            {
                var yonghuList = new List<string>();
                ZuzhiChengyuan zuzhiChengyuan = ZuzhiChengyuanHelper.Chuangjian(model.ChengyuanId,
                                                                                model.ChengyuanMingcheng,
                                                                                (ChengyuanLeixing)
                                                                                model.ChengyuanLeixing);
                var daanGuanliQuanxian = new DaanGuanliQuanxian(model.Id, (QuanxianZhi) model.Zhi, zuzhiChengyuan);
                daanGuanliQuanxianList.Add(daanGuanliQuanxian);
            }

            return daanGuanliQuanxianList;
        }

        #endregion
    }
}
