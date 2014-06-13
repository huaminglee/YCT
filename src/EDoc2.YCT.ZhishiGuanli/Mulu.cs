using System;
using System.Collections.Generic;
using System.Linq;
using EDoc2.YCT.ZhishiGuanli.Exceptions;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class Mulu
    {
        private readonly object _lock = new object();
        private List<DaanGuanliQuanxian> _daanGuanliQuanxianList;
        private List<Quanxian> _quanxianList;
        private List<Zhishi> _zhishiList;
        private List<Mulu> _ziMuluList;

        public Mulu(int id, string mingcheng, string chuangjianren, DateTime chuangjianShijian,
                    List<Quanxian> quanxianList,
                    List<Mulu> ziMuluList, List<Zhishi> zhishiList, List<DaanGuanliQuanxian> daanGuanliQuanxianList)
        {
            Id = id;
            Mingcheng = mingcheng;
            Chuangjianren = chuangjianren;
            ChuangjianShijian = chuangjianShijian;

            _quanxianList = quanxianList;
            if (_quanxianList == null)
            {
                _quanxianList = new List<Quanxian>();
            }
            _quanxianList.ForEach(x => x.ShanchuHou += Quanxian_ShanchuHou);

            InitializeDaanGuanliQuanxianList(daanGuanliQuanxianList);

            _ziMuluList = ziMuluList;
            if (_ziMuluList == null)
            {
                _ziMuluList = new List<Mulu>();
            }
            _ziMuluList.ForEach(x => x.ShanchuHou += ZiMulu_ShanchuHou);

            _zhishiList = zhishiList;
            if (_zhishiList == null)
            {
                _zhishiList = new List<Zhishi>();
            }
            _zhishiList.ForEach(x =>
                {
                    x.Mulu = this;
                    x.Shanchuhou += Zhishi_Shanchuhou;
                    x.Xiugaihou += Xiugaihou;
                });
            PaiXunZhishi();
        }

        public int Id { private set; get; }

        public string Mingcheng { private set; get; }

        public string Chuangjianren { private set; get; }

        public DateTime ChuangjianShijian { private set; get; }

        public Mulu FuMulu { internal set; get; }

        public List<Quanxian> GetQuanxianList()
        {
            return _quanxianList.ToList();
        }

        public List<Quanxian> GetJichengQuanxianList()
        {
            if (FuMulu != null)
            {
                List<Quanxian> quanxianList = FuMulu.GetAllQuanxianList();
                return
                    quanxianList.Where(x => !_quanxianList.Any(y => x.Chengyuan.ShiXiangtongChengyuan(y.Chengyuan)))
                                .ToList();
            }
            return new List<Quanxian>();
        }

        public List<Quanxian> GetAllQuanxianList()
        {
            List<Quanxian> quanxianList = GetQuanxianList();
            List<Quanxian> jichengQuanxianList = GetJichengQuanxianList();
            quanxianList.AddRange(jichengQuanxianList);
            return quanxianList;
        }

        public List<Quanxian> GetYijiDaanQuanxian()
        {
            return GetAllQuanxianList().Where(x => x.Zhi.HasFlag(QuanxianZhi.YijiDaan)).ToList();
        }

        public List<Quanxian> GetErjiDaanQuanxian()
        {
            return GetAllQuanxianList().Where(x => x.Zhi.HasFlag(QuanxianZhi.ErjiDaan)).ToList();
        }

        public List<Quanxian> GetSanjiDaanQuanxian()
        {
            return GetAllQuanxianList().Where(x => x.Zhi.HasFlag(QuanxianZhi.SanjiDaan)).ToList();
        }

        public List<Quanxian> GetSijiDaanQuanxian()
        {
            return GetAllQuanxianList().Where(x => x.Zhi.HasFlag(QuanxianZhi.SijiDaan)).ToList();
        }

        public List<Quanxian> GetWujiDaanQuanxian()
        {
            return GetAllQuanxianList().Where(x => x.Zhi.HasFlag(QuanxianZhi.WujiDaan)).ToList();
        }

        public List<Quanxian> GetGuanliQuanxian()
        {
            return GetAllQuanxianList().Where(x => x.Zhi.HasFlag(QuanxianZhi.Guanli)).ToList();
        }

        public bool YouYijiDaanQuanxian(string yonghu)
        {
            List<Quanxian> quanxianList = GetYijiDaanQuanxian();
            if (quanxianList.Any(x => x.Youquanxian(yonghu, x.Zhi)))
            {
                return true;
            }
            return false;
        }

        public bool YouErjiDaanQuanxian(string yonghu)
        {
            List<Quanxian> quanxianList = GetErjiDaanQuanxian();
            if (quanxianList.Any(x => x.Youquanxian(yonghu, x.Zhi)))
            {
                return true;
            }
            return false;
        }

        public bool YouSanjiDaanQuanxian(string yonghu)
        {
            List<Quanxian> quanxianList = GetSanjiDaanQuanxian();
            if (quanxianList.Any(x => x.Youquanxian(yonghu, x.Zhi)))
            {
                return true;
            }
            return false;
        }

        public bool YouSijiDaanQuanxian(string yonghu)
        {
            List<Quanxian> quanxianList = GetSijiDaanQuanxian();
            if (quanxianList.Any(x => x.Youquanxian(yonghu, x.Zhi)))
            {
                return true;
            }
            return false;
        }

        public bool YouWujiDaanQuanxian(string yonghu)
        {
            List<Quanxian> quanxianList = GetWujiDaanQuanxian();
            if (quanxianList.Any(x => x.Youquanxian(yonghu, x.Zhi)))
            {
                return true;
            }
            return false;
        }

        public bool YouGuanliQuanxian(string yonghu)
        {
            return true;
            if (Chuangjianren.Equals(yonghu, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            List<Quanxian> quanxianList = GetGuanliQuanxian();
            if (quanxianList.Any(x => x.Youquanxian(yonghu, x.Zhi)))
            {
                return true;
            }
            return false;
        }

        internal List<Mulu> GetZiMuluList()
        {
            return _ziMuluList.ToList();
        }

        public List<Mulu> GetKeyulanMuluList(string yonghu)
        {
            return _ziMuluList.Where(x => x.YouDaanChakanQuanxian(yonghu) || x.YouZiMuluYulanQuanxian(yonghu)).ToList();
        }

        protected bool YouZiMuluYulanQuanxian(string yonghu)
        {
            return _ziMuluList.Any(x => x.YouDaanChakanQuanxian(yonghu) || x.YouZiMuluYulanQuanxian(yonghu));
        }

        protected bool YouDaanChakanQuanxian(string yonghu)
        {
            return YouYijiDaanQuanxian(yonghu) ||
                   YouErjiDaanQuanxian(yonghu) ||
                   YouSanjiDaanQuanxian(yonghu) ||
                   YouSijiDaanQuanxian(yonghu) ||
                   YouWujiDaanQuanxian(yonghu);
        }

        private List<Mulu> GetGuanliMuluList(string yonghu)
        {
            return _ziMuluList.Where(x => x.YouGuanliQuanxian(yonghu)).ToList();
        }

        public List<Zhishi> GetZhishiList()
        {
            return _zhishiList.ToList();
        }

        private List<Zhishi> ChaxunGuanliZhishiList(string yonghu, string wenti, string daan, string fujian,
                                                    bool baohanZiMulu)
        {
            if (string.IsNullOrEmpty(daan))
            {
                daan = "";
            }
            if (string.IsNullOrEmpty(fujian))
            {
                fujian = "";
            }
            var list = new List<Zhishi>();

            list.AddRange(_zhishiList.Where(x =>
                {
                    if (!string.IsNullOrEmpty(wenti) &&
                        x.Wenti.IndexOf(wenti, StringComparison.InvariantCultureIgnoreCase) == -1)
                    {
                        return false;
                    }
                    if (!string.IsNullOrEmpty(daan))
                    {
                        string zhishiDaan = "";
                        zhishiDaan += x.YijiDaan.Neirong;
                        zhishiDaan += x.ErjiDaan.Neirong;
                        zhishiDaan += x.SanjiDaan.Neirong;
                        zhishiDaan += x.SijiDaan.Neirong;
                        zhishiDaan += x.WujiDaan.Neirong;
                        if (zhishiDaan.IndexOf(daan, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            return false;
                        }
                    }
                    if (!string.IsNullOrEmpty(fujian) && !x.YijiDaan.YouFujian(fujian) && !x.ErjiDaan.YouFujian(fujian) &&
                        !x.SanjiDaan.YouFujian(fujian)
                        && !x.SijiDaan.YouFujian(fujian) && !x.WujiDaan.YouFujian(fujian))
                    {
                        return false;
                    }
                    return true;
                }).ToList());

            if (baohanZiMulu)
            {
                foreach (Mulu ziMulu in _ziMuluList)
                {
                    list.AddRange(ziMulu.ChaxunGuanliZhishiList(yonghu, wenti, daan, fujian, baohanZiMulu));
                }
            }

            return list;
        }

        private bool YouGuanjianzi(string yonghu, Zhishi zhishi, string guanjianzi)
        {
            if (zhishi.Wenti.IndexOf(guanjianzi, StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                return true;
            }
            string zhishiDaan = "";
            bool fujianSousuo = false;
            if (YouYijiDaanQuanxian(yonghu))
            {
                zhishiDaan += zhishi.YijiDaan.Neirong;
                if (!fujianSousuo)
                {
                    fujianSousuo = zhishi.YijiDaan.YouFujian(guanjianzi);
                }
            }
            if (YouErjiDaanQuanxian(yonghu))
            {
                zhishiDaan += zhishi.ErjiDaan.Neirong;
                if (!fujianSousuo)
                {
                    fujianSousuo = zhishi.ErjiDaan.YouFujian(guanjianzi);
                }
            }
            if (YouSanjiDaanQuanxian(yonghu))
            {
                zhishiDaan += zhishi.SanjiDaan.Neirong;
                if (!fujianSousuo)
                {
                    fujianSousuo = zhishi.SanjiDaan.YouFujian(guanjianzi);
                }
            }
            if (YouSijiDaanQuanxian(yonghu))
            {
                zhishiDaan += zhishi.SijiDaan.Neirong;
                if (!fujianSousuo)
                {
                    fujianSousuo = zhishi.SijiDaan.YouFujian(guanjianzi);
                }
            }
            if (YouWujiDaanQuanxian(yonghu))
            {
                zhishiDaan += zhishi.WujiDaan.Neirong;
                if (!fujianSousuo)
                {
                    fujianSousuo = zhishi.WujiDaan.YouFujian(guanjianzi);
                }
            }
            if (zhishiDaan.IndexOf(guanjianzi, StringComparison.InvariantCultureIgnoreCase) > -1 || fujianSousuo)
            {
                return true;
            }
            return false;
        }

        public List<Zhishi> ChaxunZhishiList(string yonghu, List<string> guanjianziList, bool baohanZiMulu)
        {
            var list = new List<Zhishi>();

            if (YouDaanChakanQuanxian(yonghu))
            {
                list.AddRange(_zhishiList.Where(x =>
                    {
                        foreach (string guanjianzi in guanjianziList)
                        {
                            bool youGuanjianzi = YouGuanjianzi(yonghu, x, guanjianzi);
                            if (!youGuanjianzi)
                            {
                                return false;
                            }
                        }
                        return true;
                    }).ToList());
            }

            if (baohanZiMulu)
            {
                foreach (Mulu ziMulu in _ziMuluList)
                {
                    list.AddRange(ziMulu.ChaxunZhishiList(yonghu, guanjianziList, baohanZiMulu));
                }
            }

            return list;
        }

        public List<Zhishi> ChaxunZhishiList(string yonghu, string wenti, string daan, string fujian, bool baohanZiMulu)
        {
            if (string.IsNullOrEmpty(daan))
            {
                daan = "";
            }
            if (string.IsNullOrEmpty(fujian))
            {
                fujian = "";
            }
            var list = new List<Zhishi>();

            if (YouDaanChakanQuanxian(yonghu))
            {
                list.AddRange(_zhishiList.Where(x =>
                    {
                        if (!string.IsNullOrEmpty(wenti) &&
                            x.Wenti.IndexOf(wenti, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            return false;
                        }
                        if (!string.IsNullOrEmpty(daan))
                        {
                            string zhishiDaan = "";
                            if (YouYijiDaanQuanxian(yonghu))
                            {
                                zhishiDaan += x.YijiDaan.Neirong + ",";
                            }
                            if (YouErjiDaanQuanxian(yonghu))
                            {
                                zhishiDaan += x.ErjiDaan.Neirong + ",";
                            }
                            if (YouSanjiDaanQuanxian(yonghu))
                            {
                                zhishiDaan += x.SanjiDaan.Neirong + ",";
                            }
                            if (YouSijiDaanQuanxian(yonghu))
                            {
                                zhishiDaan += x.SijiDaan.Neirong + ",";
                            }
                            if (YouWujiDaanQuanxian(yonghu))
                            {
                                zhishiDaan += x.WujiDaan.Neirong;
                            }
                            if (zhishiDaan.IndexOf(daan, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                return false;
                            }
                        }
                        if (!string.IsNullOrEmpty(fujian))
                        {
                            bool yijiDaanFujianSousuo = false;
                            if (YouYijiDaanQuanxian(yonghu))
                            {
                                yijiDaanFujianSousuo = x.YijiDaan.YouFujian(fujian);
                            }
                            bool erjiDaanFujianSousuo = false;
                            if (YouErjiDaanQuanxian(yonghu))
                            {
                                erjiDaanFujianSousuo = x.ErjiDaan.YouFujian(fujian);
                            }
                            bool sanjiDaanFujianSousuo = false;
                            if (YouSanjiDaanQuanxian(yonghu))
                            {
                                sanjiDaanFujianSousuo = x.SanjiDaan.YouFujian(fujian);
                            }
                            bool sijiDaanFujianSousuo = false;
                            if (YouSijiDaanQuanxian(yonghu))
                            {
                                sijiDaanFujianSousuo = x.SijiDaan.YouFujian(fujian);
                            }
                            bool wujiDaanFujianSousuo = false;
                            if (YouWujiDaanQuanxian(yonghu))
                            {
                                wujiDaanFujianSousuo = x.WujiDaan.YouFujian(fujian);
                            }
                            if (!yijiDaanFujianSousuo && !erjiDaanFujianSousuo && !sanjiDaanFujianSousuo &&
                                !sijiDaanFujianSousuo && !wujiDaanFujianSousuo)
                            {
                                return false;
                            }
                        }
                        return true;
                    }).OrderByDescending(x => x.XiugaiShijian).ToList());
            }

            var listZhidingde = new List<Zhishi>();
            var newList = new List<Zhishi>();

            foreach (Zhishi zhishi in list)
            {
                if (zhishi.Zhidingde == null)
                {
                    newList.Add(zhishi);
                }
                else if (zhishi.Zhidingde.Value)
                {
                    listZhidingde.Add(zhishi);
                }
                else
                {
                    newList.Add(zhishi);
                }
            }
            list.Clear();

            foreach (Zhishi zhishi in listZhidingde)
            {
                list.Add(zhishi);
            }

            foreach (Zhishi zhishi in newList)
            {
                list.Add(zhishi);
            }

            if (baohanZiMulu)
            {
                foreach (Mulu ziMulu in _ziMuluList)
                {
                    list.AddRange(ziMulu.ChaxunZhishiList(yonghu, wenti, daan, fujian, baohanZiMulu));
                }
            }

            return list;
        }

        public event TEventHandler<Mulu, Mulu> ChuangjianZiMuluHou;

        public Mulu ChuangjianMulu(string mingcheng, string chuangjianren)
        {
            lock (_lock)
            {
                var model = new MuluDataModel();
                model.Chuangjianren = chuangjianren;
                model.ChuangjianShijian = DateTime.Now;
                model.FuMulu = Id;
                model.Mingcheng = mingcheng;
                var id = (int) NHibernateHelper.CurrentSession.Save(model);

                var mulu = new Mulu(id, mingcheng, chuangjianren, model.ChuangjianShijian, null, null, null, null);
                mulu.FuMulu = this;
                mulu.ShanchuHou += ZiMulu_ShanchuHou;
                List<Mulu> muluList = _ziMuluList.ToList();
                muluList.Add(mulu);
                _ziMuluList = muluList;

                if (ChuangjianZiMuluHou != null)
                {
                    ChuangjianZiMuluHou(this, mulu);
                }
                return mulu;
            }
        }

        public void Xiugai(string mingcheng)
        {
            var dataModel = NHibernateHelper.CurrentSession.Get<MuluDataModel>(Id);
            dataModel.Mingcheng = mingcheng;
            NHibernateHelper.CurrentSession.Update(dataModel);
            NHibernateHelper.CurrentSession.Flush();
            Mingcheng = mingcheng;
        }

        private void ZiMulu_ShanchuHou(Mulu args)
        {
            lock (_lock)
            {
                List<Mulu> muluList = _ziMuluList.ToList();
                muluList.Remove(args);
                _ziMuluList = muluList;
            }
        }

        public event TEventHandler<Mulu, Zhishi> ChuangjianZhishihou;

        public Zhishi ChuangjianZhishi(string wenti, int? shunxu, string chuangjianren, string yijiDaanNeirong,
                                       List<FujianXinxi> yijiDaanFujian,
                                       string erjiDaanNeirong, List<FujianXinxi> erjiDaanFujian, string sanjiDaanNeirong,
                                       List<FujianXinxi> sanjiDaanFujian,
                                       string sijiDaanNeirong, List<FujianXinxi> sijiDaanFujian, string wujiDaanNeirong,
                                       List<FujianXinxi> wujiDaanFujian, string yonghu)
        {
            lock (_lock)
            {
                wenti = wenti.Trim();
                if (_zhishiList.Any(x => x.Wenti.Equals(wenti, StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new Exception("创建失败，知识问题重复");
                }
                if (string.IsNullOrEmpty(yijiDaanNeirong) && string.IsNullOrEmpty(erjiDaanNeirong) &&
                    string.IsNullOrEmpty(sanjiDaanNeirong) && string.IsNullOrEmpty(sijiDaanNeirong) &&
                    string.IsNullOrEmpty(wujiDaanNeirong))
                {
                    throw new Exception("创建失败，至少填写一个答案");
                }

                Daan yijiDaan = null;
                Daan erjiDaan = null;
                Daan sanjiDaan = null;
                Daan sijiDaan = null;
                Daan wujiDaan = null;

                bool youYijiDaanGuanliQuanxian = YouDaanGuanliQuanxian(yonghu, QuanxianZhi.YijiDaan);
                bool youErjiDaanGuanliQuanxian = YouDaanGuanliQuanxian(yonghu, QuanxianZhi.ErjiDaan);
                bool youSanjiDaanGuanliQuanxian = YouDaanGuanliQuanxian(yonghu, QuanxianZhi.SanjiDaan);
                bool youSijiDaanGuanliQuanxian = YouDaanGuanliQuanxian(yonghu, QuanxianZhi.SijiDaan);
                bool youWujiDaanGuanliQuanxian = YouDaanGuanliQuanxian(yonghu, QuanxianZhi.WujiDaan);

                var model = new ZhishiDataModel();
                model.Shunxu = shunxu;
                model.Banben = 1;
                model.Chuangjianren = chuangjianren;
                model.ChuangjianShijian = DateTime.Now;
                model.Mulu = Id;
                model.Wenti = wenti;

                if (youYijiDaanGuanliQuanxian)
                {
                    yijiDaan = Zhishiku.ChuangjianDaan(yijiDaanNeirong, yijiDaanFujian);
                    model.YijiDaan = yijiDaan.Id;
                }
                if (youErjiDaanGuanliQuanxian)
                {
                    erjiDaan = Zhishiku.ChuangjianDaan(erjiDaanNeirong, erjiDaanFujian);
                    model.ErjiDaan = erjiDaan.Id;
                }
                if (youSanjiDaanGuanliQuanxian)
                {
                    sanjiDaan = Zhishiku.ChuangjianDaan(sanjiDaanNeirong, sanjiDaanFujian);
                    model.SanjiDaan = sanjiDaan.Id;
                }
                if (youSijiDaanGuanliQuanxian)
                {
                    sijiDaan = Zhishiku.ChuangjianDaan(sijiDaanNeirong, sijiDaanFujian);
                    model.SijiDaan = sijiDaan.Id;
                }
                if (youWujiDaanGuanliQuanxian)
                {
                    wujiDaan = Zhishiku.ChuangjianDaan(wujiDaanNeirong, wujiDaanFujian);
                    model.WujiDaan = wujiDaan.Id;
                }
                var id = (int) NHibernateHelper.CurrentSession.Save(model);

                var zhishi = new Zhishi(id, wenti, yijiDaan, erjiDaan, sanjiDaan, sijiDaan, wujiDaan, chuangjianren,
                                        model.ChuangjianShijian, model.Banben, model.Shunxu, null, null, null, false);
                zhishi.Mulu = this;
                List<Zhishi> zhishiList = _zhishiList;
                zhishiList.Add(zhishi);
                _zhishiList = zhishiList.OrderBy(x => x.Shunxu).ToList();
                zhishi.Shanchuhou += Zhishi_Shanchuhou;
                zhishi.Xiugaihou += Xiugaihou;
                DizhengZhishiShunxu(zhishi);
                PaiXunZhishi();
                if (ChuangjianZhishihou != null)
                {
                    ChuangjianZhishihou(this, zhishi);
                }
                return zhishi;
            }
        }

        private void Xiugaihou(Zhishi zhishi, XiugaihouEventArgs<ZhishiXinxi> args)
        {
            lock (_lock)
            {
                if (args.XiugaiqianXinxi.Shunxu != zhishi.Shunxu)
                {
                    DizhengZhishiShunxu(zhishi);
                }

                PaiXunZhishi();
            }
        }

        private void DizhengZhishiShunxu(Zhishi zhishi)
        {
            if (zhishi.Shunxu.HasValue && _zhishiList.Any(x => x != zhishi && x.Shunxu == zhishi.Shunxu))
            {
                _zhishiList.Where(x => x != zhishi && x.Shunxu.HasValue && x.Shunxu >= zhishi.Shunxu)
                           .ToList().ForEach(x => x.InternalXiugaiShunxu(x.Shunxu.Value + 1));
            }
        }

        internal void PaiXunZhishi()
        {
            List<Zhishi> zhishiList = _zhishiList;
            _zhishiList = new List<Zhishi>();
            _zhishiList.AddRange(zhishiList.Where(x => x.Shunxu.HasValue).OrderBy(x => x.Shunxu.Value));
            _zhishiList.AddRange(zhishiList.Where(x => !x.Shunxu.HasValue).OrderByDescending(x => x.XiugaiShijian));
        }

        private void Zhishi_Shanchuhou(Zhishi args)
        {
            lock (_lock)
            {
                List<Zhishi> zhishiList = _zhishiList.ToList();
                zhishiList.Remove(args);
                _zhishiList = zhishiList;
            }
        }

        public event TEventHandler<Mulu> ShanchuHou;

        public virtual void Shanchu()
        {
            foreach (Mulu mulu in _ziMuluList)
            {
                mulu.Shanchu();
            }
            foreach (Zhishi zhishi in _zhishiList)
            {
                zhishi.Shanchu();
            }
            var dataModel = NHibernateHelper.CurrentSession.Get<MuluDataModel>(Id);
            NHibernateHelper.CurrentSession.Delete(dataModel);
            NHibernateHelper.CurrentSession.Flush();
            if (ShanchuHou != null)
            {
                ShanchuHou(this);
            }
        }

        public void TianjiaZhishi(Zhishi zhishi)
        {
            if (_zhishiList.Contains(zhishi))
            {
                throw new Exception("移动的目录中已经包含该知识");
            }

            zhishi.Mulu._zhishiList.Remove(zhishi);
            _zhishiList.Add(zhishi);
            zhishi.XiugaiMulu(this);

            DizhengZhishiShunxu(zhishi);
            PaiXunZhishi();
        }

        public void TianjiaMulu(Mulu mulu)
        {
            if (mulu == this)
            {
                throw new Exception("不能移动自己目录中");
            }
            if (_ziMuluList.Contains(mulu))
            {
                throw new Exception("移动的目录中已经包含该目录");
            }
            if (ShiWodeShangji(mulu, true))
            {
                throw new Exception("不能移动到下级目录中");
            }

            mulu.FuMulu._ziMuluList.Remove(mulu);
            _ziMuluList.Add(mulu);
            mulu.XiugaiFuMulu(this);
        }

        internal void XiugaiFuMulu(Mulu fuMulu)
        {
            var dataModel = NHibernateHelper.CurrentSession.Get<MuluDataModel>(Id);
            dataModel.FuMulu = fuMulu.Id;
            NHibernateHelper.CurrentSession.Update(dataModel);
            NHibernateHelper.CurrentSession.Flush();
            FuMulu = fuMulu;
        }

        public bool ShiWodeShangji(Mulu mulu, bool digui)
        {
            if (FuMulu == null)
            {
                return false;
            }
            if (FuMulu == mulu)
            {
                return true;
            }
            if (digui)
            {
                return FuMulu.ShiWodeShangji(mulu, digui);
            }
            return false;
        }

        public void TianjiaQuanxian(int chengyuanId, string chengyuanMingcheng, ChengyuanLeixing chengyuanLeixing,
                                    QuanxianZhi quanxianZhi)
        {
            lock (_lock)
            {
                if (_quanxianList.Any(x => x.Chengyuan.Id == chengyuanId && x.Chengyuan.Leixing == chengyuanLeixing))
                {
                    throw new ChengyuanQuanxianChongfuException();
                }
                var model = new QuanxianDataModel();
                model.ChengyuanId = chengyuanId;
                model.ChengyuanLeixing = (int) chengyuanLeixing;
                model.ChengyuanMingcheng = chengyuanMingcheng;
                model.Mulu = Id;
                model.Zhi = (int) quanxianZhi;
                model.Id = (int) NHibernateHelper.CurrentSession.Save(model);
                var quanxian = new Quanxian(model.Id, quanxianZhi,
                                            ZuzhiChengyuanHelper.Chuangjian(chengyuanId, chengyuanMingcheng,
                                                                            chengyuanLeixing));
                quanxian.ShanchuHou += Quanxian_ShanchuHou;
                List<Quanxian> quanxianList = _quanxianList.ToList();
                quanxianList.Add(quanxian);
                _quanxianList = quanxianList;
            }
        }

        private void Quanxian_ShanchuHou(Quanxian quanxian)
        {
            lock (_lock)
            {
                List<Quanxian> quanxianList = _quanxianList.ToList();
                quanxianList.Remove(quanxian);
                _quanxianList = quanxianList;
            }
        }

        #region DaanGuanliQuanxian

        private void InitializeDaanGuanliQuanxianList(List<DaanGuanliQuanxian> daanGuanliQuanxianList)
        {
            _daanGuanliQuanxianList = daanGuanliQuanxianList;
            if (_daanGuanliQuanxianList == null)
            {
                _daanGuanliQuanxianList = new List<DaanGuanliQuanxian>();
            }
            _daanGuanliQuanxianList.ForEach(x => x.ShanchuHou += DaanGuanliQuanxian_ShanchuHou);
        }

        public List<DaanGuanliQuanxian> GetDaanGuanliQuanxianList()
        {
            return _daanGuanliQuanxianList.ToList();
        }

        public bool YouDaanGuanliQuanxian(string yonghu, QuanxianZhi quanxianZhi)
        {
            List<DaanGuanliQuanxian> daanGuanliQuanxianList =
                GetAllDaanGuanliQuanxian().Where(x => x.Zhi.HasFlag(quanxianZhi)).ToList();
            return daanGuanliQuanxianList.Any(x => x.Youquanxian(yonghu, x.Zhi));
        }

        public List<DaanGuanliQuanxian> GetAllDaanGuanliQuanxian()
        {
            List<DaanGuanliQuanxian> daanGuanliQuanxianList = GetDaanGuanliQuanxianList();
            List<DaanGuanliQuanxian> jichengQuanxianList = GetJichengDaanGuanliQuanxianList();
            daanGuanliQuanxianList.AddRange(jichengQuanxianList);
            return daanGuanliQuanxianList;
        }

        public List<DaanGuanliQuanxian> GetJichengDaanGuanliQuanxianList()
        {
            if (FuMulu != null)
            {
                List<DaanGuanliQuanxian> daanGuanliQuanxianList = FuMulu.GetAllDaanGuanliQuanxian();
                return
                    daanGuanliQuanxianList.Where(
                        x => !_daanGuanliQuanxianList.Any(y => x.Chengyuan.ShiXiangtongChengyuan(y.Chengyuan))).ToList();
            }
            return new List<DaanGuanliQuanxian>();
        }

        public void TianjiaDaanGuanliMuluQuanxian(int chengyuanId, string chengyuanMingcheng,
                                                  ChengyuanLeixing chengyuanLeixing,
                                                  QuanxianZhi quanxianZhi)
        {
            lock (_lock)
            {
                if (
                    _daanGuanliQuanxianList.Any(
                        x => x.Chengyuan.Id == chengyuanId && x.Chengyuan.Leixing == chengyuanLeixing))
                {
                    throw new ChengyuanQuanxianChongfuException();
                }
                var model = new DaanGuanliQuanxianDataModel();
                model.ChengyuanId = chengyuanId;
                model.ChengyuanLeixing = (int) chengyuanLeixing;
                model.ChengyuanMingcheng = chengyuanMingcheng;
                model.Mulu = Id;
                model.Zhi = (int) quanxianZhi;
                model.Id = (int) NHibernateHelper.CurrentSession.Save(model);
                var daanGuanliQuanxian = new DaanGuanliQuanxian(model.Id, quanxianZhi,
                                                                ZuzhiChengyuanHelper.Chuangjian(chengyuanId,
                                                                                                chengyuanMingcheng,
                                                                                                chengyuanLeixing));
                daanGuanliQuanxian.ShanchuHou += DaanGuanliQuanxian_ShanchuHou;
                List<DaanGuanliQuanxian> daanGuanliQuanxianList = _daanGuanliQuanxianList.ToList();
                daanGuanliQuanxianList.Add(daanGuanliQuanxian);
                _daanGuanliQuanxianList = daanGuanliQuanxianList;
            }
        }

        private void DaanGuanliQuanxian_ShanchuHou(DaanGuanliQuanxian daanGuanliQuanxian)
        {
            lock (_lock)
            {
                List<DaanGuanliQuanxian> daanGuanliQuanxianList = _daanGuanliQuanxianList.ToList();
                daanGuanliQuanxianList.Remove(daanGuanliQuanxian);
                _daanGuanliQuanxianList = daanGuanliQuanxianList;
            }
        }

        #endregion
    }
}