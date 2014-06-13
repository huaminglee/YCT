using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDoc2.YCT.ZhishiGuanli.Exceptions;

namespace EDoc2.YCT.ZhishiGuanli
{
    public class DingjiMulu : Mulu
    {
        public DingjiMulu(int id, string mingcheng, string chuangjianren, DateTime chuangjianShijian,
            List<Quanxian> quanxianList, List<Mulu> ziMuluList, List<Zhishi> zhishiList, List<DaanGuanliQuanxian> daanGuanliQuanxianList)
            : base(id, mingcheng, chuangjianren, chuangjianShijian, quanxianList, ziMuluList, zhishiList, daanGuanliQuanxianList)
        {
            
        }

        public override void Shanchu()
        {
            throw new DingjiMuluException();
        }
    }
}
