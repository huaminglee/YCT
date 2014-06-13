using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EDoc2.YCT.ZhishiGuanli;

namespace EDoc2.YCTTest
{
    public class ZhishikuTester
    {
        [Test]
        public void Test1()
        {
            int? int1 = 2;
            int? int2 = 1;

            Assert.IsTrue(int1 > int2);

            Zhishiku zhishiku = new Zhishiku();

            zhishiku.DingjiMulu.ChuangjianMulu("ceshi1", "ceshi_yonghu");
        }
    }
}
