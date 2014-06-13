using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class Zhishi
    {
        public Zhishi(int id, string wenti, Daan yijiDaan, Daan erjiDaan, Daan sanjiDaan, Daan sijiDaan, Daan wujiDaan,
            string chuangjianren, DateTime chuangjianShijian, int banben, int? shunxu, List<LishiZhishi> lishiBanben,
            string xiugairen, DateTime? xiugaiShijian, bool? zhidingde)
        {
            this.Id = id;
            this.YijiDaan = yijiDaan;
            this.ErjiDaan = erjiDaan;
            this.SanjiDaan = sanjiDaan;
            this.SijiDaan = sijiDaan;
            this.WujiDaan = wujiDaan;
            this.Wenti = wenti;
            this.Chuangjianren = chuangjianren;
            this.ChuangjianShijian = chuangjianShijian;
            this.Banben = banben;
            this.Shunxu = shunxu;
            this.Xiugairen = xiugairen;
            this.XiugaiShijian = xiugaiShijian;
            this._lishiBanben = lishiBanben;
            this.Zhidingde = zhidingde;
            if (this._lishiBanben == null)
            {
                this._lishiBanben = new List<LishiZhishi>();
            }
            this._lishiBanben.ForEach(x => x.Zhishi = this);
        }

        public Mulu Mulu{ internal set; get; }

        public int Id { private set; get; }

        public string Wenti { private set; get; }

        public Daan YijiDaan { private set; get; }

        public Daan ErjiDaan { private set; get; }

        public Daan SanjiDaan { private set; get; }

        public Daan SijiDaan { private set; get; }

        public Daan WujiDaan { private set; get; }

        public string Chuangjianren { private set; get; }

        public DateTime ChuangjianShijian { private set; get; }

        public int Banben { private set; get; }

        public int? Shunxu { private set; get; }

        public string Xiugairen { private set; get; }

        public DateTime? XiugaiShijian { private set; get; }

        public bool Bianjizhong { private set; get; }

        public bool? Zhidingde { private set; get; }

        public string Bianjiren { private set; get; }

        private List<LishiZhishi> _lishiBanben;

        public List<LishiZhishi> GetLishiBanben()
        {
            return _lishiBanben.ToList();
        }

        public event TEventHandler<Zhishi, XiugaihouEventArgs<ZhishiXinxi>> Xiugaihou;

        private LishiZhishi ChuangjianLishi(string chuangjianren)
        {
            LishiZhishiDataModel lsModel = new LishiZhishiDataModel(this);
            lsModel.Chuangjianren = chuangjianren;
            lsModel.ChuangjianShijian = DateTime.Now;
            lsModel.Id = (int) NHibernateHelper.CurrentSession.Save(lsModel);
            LishiZhishi zhishi = new LishiZhishi(lsModel.Id, lsModel.Wenti, this.YijiDaan, this.ErjiDaan, this.SanjiDaan, this.SijiDaan, this.WujiDaan, 
                lsModel.Chuangjianren, lsModel.ChuangjianShijian, lsModel.Banben);
            List<LishiZhishi> list = this._lishiBanben.ToList();
            zhishi.Zhishi = this;
            list.Add(zhishi);
            this._lishiBanben = list;
            return zhishi;
        }

        public void Xiugai(string wenti, int? shunxu, string yijiDaanNeirong, List<FujianXinxi> yijiDaanFujian,
            string erjiDaanNeirong, List<FujianXinxi> erjiDaanFujian, string sanjiDaanNeirong, List<FujianXinxi> sanjiDaanFujian,
            string sijiDaanNeirong, List<FujianXinxi> sijiDaanFujian, string wujiDaanNeirong, List<FujianXinxi> wujiDaanFujian, 
            bool shengjiBanben, string xiugairen)
        {
            wenti = wenti.Trim();
            if (this.Mulu.GetZhishiList().Any(x => x.Wenti.Equals(wenti, StringComparison.InvariantCultureIgnoreCase) && x != this))
            {
                throw new Exception("修改失败，知识问题重复");
            }
            if (string.IsNullOrEmpty(yijiDaanNeirong) && string.IsNullOrEmpty(erjiDaanNeirong) &&
                string.IsNullOrEmpty(sanjiDaanNeirong) && string.IsNullOrEmpty(sijiDaanNeirong) &&
                string.IsNullOrEmpty(wujiDaanNeirong))
            {
                throw new Exception("修改失败，至少填写一个答案");
            }
            ZhishiXinxi zhishiXinxi = new ZhishiXinxi(this);

            ZhishiDataModel model = NHibernateHelper.CurrentSession.Get<ZhishiDataModel>(this.Id);

            Mulu mulu = Mulu;
            bool youYijiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(xiugairen, QuanxianZhi.YijiDaan);
            bool youErjiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(xiugairen, QuanxianZhi.ErjiDaan);
            bool youSanjiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(xiugairen, QuanxianZhi.SanjiDaan);
            bool youSijiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(xiugairen, QuanxianZhi.SijiDaan);
            bool youWujiDaanGuanliQuanxian = mulu.YouDaanGuanliQuanxian(xiugairen, QuanxianZhi.WujiDaan);

            if (shengjiBanben)
            {
                this.ChuangjianLishi(xiugairen);
                Daan yijiDaan = youYijiDaanGuanliQuanxian
                                    ? Zhishiku.ChuangjianDaan(yijiDaanNeirong, yijiDaanFujian)
                                    : Zhishiku.ChuangjianDaan(YijiDaan.Neirong, YijiDaan.FujianList);
                Daan erjiDaan = youErjiDaanGuanliQuanxian
                                    ? Zhishiku.ChuangjianDaan(erjiDaanNeirong, erjiDaanFujian)
                                    : Zhishiku.ChuangjianDaan(ErjiDaan.Neirong, ErjiDaan.FujianList);
                Daan sanjiDaan = youSanjiDaanGuanliQuanxian
                                     ? Zhishiku.ChuangjianDaan(sanjiDaanNeirong, sanjiDaanFujian)
                                     : Zhishiku.ChuangjianDaan(SanjiDaan.Neirong, SanjiDaan.FujianList);
                Daan sijiDaan = youSijiDaanGuanliQuanxian
                                    ? Zhishiku.ChuangjianDaan(sijiDaanNeirong, sijiDaanFujian)
                                    : Zhishiku.ChuangjianDaan(SijiDaan.Neirong, SijiDaan.FujianList);
                Daan wujiDaan = youWujiDaanGuanliQuanxian
                                    ? Zhishiku.ChuangjianDaan(wujiDaanNeirong, wujiDaanFujian)
                                    : Zhishiku.ChuangjianDaan(WujiDaan.Neirong, WujiDaan.FujianList);

                YijiDaan = yijiDaan;
                ErjiDaan = erjiDaan;
                SanjiDaan = sanjiDaan;
                SijiDaan = sijiDaan;
                WujiDaan = wujiDaan;

                model.YijiDaan = yijiDaan.Id;
                model.ErjiDaan = erjiDaan.Id;
                model.SanjiDaan = sanjiDaan.Id;
                model.SijiDaan = sijiDaan.Id;
                model.WujiDaan = wujiDaan.Id;
                model.Banben++;
            }
            else
            {
                if (youYijiDaanGuanliQuanxian)
                {
                    this.YijiDaan.Xiugai(yijiDaanNeirong, yijiDaanFujian);
                }
                if (youErjiDaanGuanliQuanxian)
                {
                    this.ErjiDaan.Xiugai(erjiDaanNeirong, erjiDaanFujian);
                }
                if (youSanjiDaanGuanliQuanxian)
                {
                    this.SanjiDaan.Xiugai(sanjiDaanNeirong, sanjiDaanFujian);
                }
                if (youSijiDaanGuanliQuanxian)
                {
                    this.SijiDaan.Xiugai(sijiDaanNeirong, sijiDaanFujian);
                }
                if (youWujiDaanGuanliQuanxian)
                {
                    this.WujiDaan.Xiugai(wujiDaanNeirong, wujiDaanFujian);
                }
            }
            //model.Shunxu = shunxu;
            model.Wenti = wenti;
            model.Xiugairen = xiugairen;
            model.XiugaiShijian = DateTime.Now;

            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();

            this.Wenti = wenti;
            this.Banben = model.Banben;
            //this.Shunxu = shunxu;
            this.Xiugairen = xiugairen;
            this.XiugaiShijian = model.XiugaiShijian;

            if (this.Xiugaihou != null)
            {
                XiugaihouEventArgs<ZhishiXinxi> args = new XiugaihouEventArgs<ZhishiXinxi>();
                args.XiugaiqianXinxi = zhishiXinxi;
                this.Xiugaihou(this, args);
            }
        }

        public void Zhiding()
        {
            ZhishiXinxi zhishiXinxi = new ZhishiXinxi(this);

            ZhishiDataModel model = NHibernateHelper.CurrentSession.Get<ZhishiDataModel>(this.Id);
            
            model.Shunxu = 1;
            model.Zhidingde = true;

            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();

            this.Shunxu = 1;
            this.Zhidingde = true;

            if (this.Xiugaihou != null)
            {
                XiugaihouEventArgs<ZhishiXinxi> args = new XiugaihouEventArgs<ZhishiXinxi>();
                args.XiugaiqianXinxi = zhishiXinxi;
                this.Xiugaihou(this, args);
            }
        }

        public void QuxiaoZhiding()
        {
            ZhishiXinxi zhishiXinxi = new ZhishiXinxi(this);

            ZhishiDataModel model = NHibernateHelper.CurrentSession.Get<ZhishiDataModel>(this.Id);

            model.Shunxu = null;
            model.Zhidingde = false;

            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();

            this.Shunxu = null;
            this.Zhidingde = false;

            if (this.Xiugaihou != null)
            {
                XiugaihouEventArgs<ZhishiXinxi> args = new XiugaihouEventArgs<ZhishiXinxi>();
                args.XiugaiqianXinxi = zhishiXinxi;
                this.Xiugaihou(this, args);
            }
        }

        internal void XiugaiMulu(Mulu mulu)
        {
            ZhishiDataModel model = NHibernateHelper.CurrentSession.Get<ZhishiDataModel>(this.Id);
            model.Mulu = mulu.Id;

            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();

            this.Mulu = mulu;
        }

        internal void InternalXiugaiShunxu(int shunxu)
        {
            ZhishiDataModel model = NHibernateHelper.CurrentSession.Get<ZhishiDataModel>(this.Id);
            model.Shunxu = shunxu;

            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();

            this.Shunxu = shunxu;
        }

        public event TEventHandler<Zhishi> Shanchuhou;

        public void Shanchu()
        {
            this.YijiDaan.Shanchu();
            this.ErjiDaan.Shanchu();
            this.SanjiDaan.Shanchu();
            this.SijiDaan.Shanchu();
            this.WujiDaan.Shanchu();

            ZhishiDataModel model = NHibernateHelper.CurrentSession.Get<ZhishiDataModel>(this.Id);
            NHibernateHelper.CurrentSession.Delete(model);
            NHibernateHelper.CurrentSession.Flush();
            if (this.Shanchuhou != null)
            {
                this.Shanchuhou(this);
            }
        }

        public void Suoding(string yonghu)
        {
            if (this.Bianjizhong && this.Bianjiren != yonghu)
            {
                throw new Exception(string.Format("已经被{0}锁定", this.Bianjiren));
            }
            this.Bianjiren = yonghu;
            this.Bianjizhong = true;
        }

        public void Jiesuo()
        {
            this.Bianjiren = "";
            this.Bianjizhong = false;
        }

        internal void Jiazai()
        {
            IList<DaanDataModel> models = NHibernateHelper.CurrentSession.QueryOver<DaanDataModel>().Where(x => x.ZhishiId == this.Id).List();
            foreach (DaanDataModel model in models)
            {
                
            }
        }
    }
}
