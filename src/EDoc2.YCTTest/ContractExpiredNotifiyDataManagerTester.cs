using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EDoc2.YCT.Core;

namespace EDoc2.YCTTest
{
    public class ContractExpiredNotifiyDataManagerTester
    {
        [Test]
        public void Test()
        {
            ContractExpiredNotifiyDataManager manager = new ContractExpiredNotifiyDataManager();
            manager.Insert(new ContractExpiredNotifiyLogInfo { EDoc2FileId = 2, ExpireDate = DateTime.Today });

            Assert.IsTrue(manager.Exist(2, DateTime.Today));
        }
    }
}
