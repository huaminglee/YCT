using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EDoc2.YCT.Core;
using System.Threading;

namespace EDoc2.YCTTest
{
    public class ContractTester
    {
        [Test]
        public void Test()
        {
            ContractFactory factroy = new ContractFactory();
            Contract contract = factroy.Create(1, DateTime.Now, null);
            contract.Update(DateTime.Now.AddDays(10));

            //ContractExpireDaysCalculativeService expireDaysCalculativeService = new ContractExpireDaysCalculativeService(factroy);
            //string contractExpiredNodifyEmails = "";
            //int advanceDays;
            //advanceDays = 10;
            //ContractExpiredNotifyService contractExpiredNotifyService = new ContractExpiredNotifyService(factroy, advanceDays, contractExpiredNodifyEmails);

            //expireDaysCalculativeService.Start(new Common.Log.ConsoleLog());
            //contractExpiredNotifyService.Start(new Common.Log.ConsoleLog());

            //Thread.Sleep(1000*5);
        }
    }
}
