using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.Core
{
    public class Contract
    {
        public Contract(ContractInfo contractInfo)
        {
            this.Id = contractInfo.Id;
            this.EDoc2FileId = contractInfo.EDoc2FileId;
            this.ExpireDate = contractInfo.ExpireDate;
            this.ValidMonth = contractInfo.ValidMonth;
        }

        public int Id { private set; get; }

        public int EDoc2FileId { private set; get; }

        public int? ValidMonth { set; get; }

        public DateTime? ExpireDate { private set; get; }

        private object _updateLock = new object();

        public void Update(DateTime? expireDate)
        {
            lock (_updateLock)
            {
                ContractInfo contractInfo = NHibernateHelper.CurrentSession.Get<ContractInfo>(this.Id);
                contractInfo.ExpireDate = expireDate;
                NHibernateHelper.CurrentSession.Update(contractInfo, contractInfo.Id);
                NHibernateHelper.CurrentSession.Flush();
                this.ExpireDate = expireDate;
            }
        }

        public void Update(int? validMonth)
        {
            lock (_updateLock)
            {
                ContractInfo contractInfo = NHibernateHelper.CurrentSession.Get<ContractInfo>(this.Id);
                contractInfo.ValidMonth = validMonth;
                NHibernateHelper.CurrentSession.Update(contractInfo, contractInfo.Id);
                NHibernateHelper.CurrentSession.Flush();
                this.ValidMonth = validMonth;
            }
        }
    }
}
