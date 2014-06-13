using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDoc2.YCT.Website.Models
{
    public class ChuangjianZhishiModel
    {
        public int muluId;
        public int? shunxu;
        public string wenti;
        public string yijiDaan;
        public List<FujianModel> yijiDaanFujian;
        public string erjiDaan;
        public List<FujianModel> erjiDaanFujian;
        public string sanjiDaan;
        public List<FujianModel> sanjiDaanFujian;
        public string sijiDaan;
        public List<FujianModel> sijiDaanFujian;
        public string wujiDaan;
        public List<FujianModel> wujiDaanFujian;
    }
}