using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public abstract class ZuzhiChengyuan
    {
        protected ZuzhiChengyuan(int id, string mingcheng)
        {
            this.Id = id;
            this.Mingcheng = mingcheng;
        }

        public int Id { private set; get; }

        public string Mingcheng { private set; get; }

        public abstract ChengyuanLeixing Leixing { get; }

        public abstract bool Contians(string yonghu);

        public bool ShiXiangtongChengyuan(ZuzhiChengyuan chengyuan)
        {
            if (chengyuan.Leixing == this.Leixing && chengyuan.Id == this.Id)
            {
                return true;
            }
            return false;
        }
    }
}
