using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.Core
{
    public class ContractExpiredNotifiyLogInfo
    {
        public virtual int Id { set; get; }

        public virtual int EDoc2FileId { set; get; }

        public virtual DateTime ExpireDate { set; get; }

    }
}
