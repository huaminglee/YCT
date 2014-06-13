using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.Core
{
    public class ContractExpiredNotifiyDataManager
    {
        public void Insert(ContractExpiredNotifiyLogInfo info)
        {
            NHibernateHelper.CurrentSession.Save(info);
        }

        public bool Exist(int fileId, DateTime expireDate)
        {
            NHibernate.Criterion.ICriterion criterion = NHibernate.Criterion.Expression.Where<ContractExpiredNotifiyLogInfo>(x => x.EDoc2FileId == fileId && x.ExpireDate == expireDate);
            int count = NHibernateHelper.CurrentSession.QueryOver<ContractExpiredNotifiyLogInfo>().Where(criterion).RowCount();
            return count > 0;
        }
    }
}
